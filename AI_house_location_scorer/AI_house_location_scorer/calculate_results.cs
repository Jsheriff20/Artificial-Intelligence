using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using PoliceUk;
using PoliceUk.Entities.StreetLevel;
using RestSharp;
using RestSharp.Authenticators;
using RestSharp.Serialization.Json;
using Newtonsoft.Json.Linq;
using distance_between_two_points_namespace;
using number_of_nearby_places_namespace;
using flood_risk_analysis_namespace;
using average_weather_temp_namespace;
using Newtonsoft.Json;
using BingMapsRESTToolkit;

namespace AI_house_location_scorer
{
    public partial class calculate_results : Form
    {
        // get api results - functions

        string[] get_postcode_long_and_lat(string postcode)
        {
            string[] long_and_lat = new string[2];

            //request api json data
            var client = new RestClient("http://api.postcodes.io/");
            var request = new RestRequest("postcodes/{postcode}", Method.GET);
            request.AddUrlSegment("postcode", postcode);

            //get response and decode it
            var response = client.Execute(request);
            JsonDeserializer deserialise = new JsonDeserializer();
            var json_response = deserialise.Deserialize<Dictionary<string, string>>(response);


            //encode result into json array then decode the inner array
            IRestResponse rest_response = new RestResponse { Content = json_response["result"] };
            var decoded_response = deserialise.Deserialize<Dictionary<string, string>>(rest_response);


            //return longitude & latitude array
            long_and_lat[0] = decoded_response["longitude"];
            long_and_lat[1] = decoded_response["latitude"];
            return long_and_lat;
        }




        string[] get_average_weather_temps(string postcode)
        {
            string[] highest_and_lowest = new string[2];

            DateTime start_date = DateTime.Now.AddMonths(-36);
            string start_date_str = start_date.ToString("yyyy-MM-dd");
            DateTime end_date = DateTime.Now.AddMonths(-36).AddDays(59);
            string end_date_str = end_date.ToString("yyyy-MM-dd");

            int total_highest_weather_temp = 0;
            int total_lowest_weather_temp = 0;

            //get current iterations start and end dates
            DateTime current_date = start_date;
            DateTime current_end_date = start_date.AddMonths(12);

            for (int i = 0; i < 3; i++)
            {
                //reset current record temps
                int current_highest_temp = 0;
                int current_lowest_temp = 0;

                while (current_date < current_end_date)
                {
                    //build url
                    string url = "historical?access_key=bd0825bb618b97237f5a30ede05f5acc&query=" + postcode + "&historical_date_start=" + start_date_str + "&historical_date_end=" + end_date_str;

                    //request api json data
                    var client = new RestClient("https://api.weatherstack.com/");
                    var request = new RestRequest(url, Method.GET);


                    //get response and deserialise it into a string
                    var response = client.Execute(request);
                    JsonDeserializer deserialise = new JsonDeserializer();
                    string json_response = deserialise.Deserialize<string>(response);


                    for (int j = 0; j < 59; j++)
                    {
                        //if the date that is currently being viewed is younger than 7 days exit the loop (this allows for bank holidays mondays and days where data may not be recorded)
                        if (current_date >= DateTime.Now.AddDays(-7))
                        {
                            break;
                        }


                        //get json and get the value we need (number of places nearby)
                        var json = AverageWeatherTemp.FromJson(json_response);
                        try
                        {
                            //get temp data for a specific date from a request 
                            string max_temp = json.Historical[current_date.ToString("yyyy-MM-dd")].Maxtemp.ToString();
                            string min_temp = json.Historical[current_date.ToString("yyyy-MM-dd")].Mintemp.ToString();

                            //update current years record temps
                            if (Convert.ToInt32(min_temp) < current_lowest_temp) current_lowest_temp = Convert.ToInt32(min_temp);
                            if (Convert.ToInt32(max_temp) > current_highest_temp) current_highest_temp = Convert.ToInt32(max_temp);
                        }
                        catch (Exception exp)
                        {
                            Console.WriteLine(exp);
                        }
                        current_date = current_date.AddDays(1);
                    }
                    //if the date that is currently being viewed is younger than 7 days exit the loop (this allows for bank holidays mondays and days where data may not be recorded)
                    if (current_date >= DateTime.Now.AddDays(-7) || end_date >= DateTime.Now.AddDays(-7))
                    {
                        break;
                    }


                    //adjust start and end dates for next iteration
                    start_date = start_date.AddDays(59);
                    end_date = end_date.AddDays(59);
                    start_date_str = start_date.ToString("yyyy-MM-dd");
                    end_date_str = end_date.ToString("yyyy-MM-dd");
                }
                //add temps together to get total
                total_highest_weather_temp += current_highest_temp;
                total_lowest_weather_temp += current_lowest_temp;
                Console.WriteLine("highest " + total_highest_weather_temp);
                Console.WriteLine("lowest " + total_lowest_weather_temp);
                current_end_date = current_end_date.AddMonths(12);
            }

            //work out average temps
            highest_and_lowest[0] = (total_highest_weather_temp / 3).ToString();
            highest_and_lowest[1] = (total_lowest_weather_temp / 3).ToString();
            return highest_and_lowest;
        }





        //will get the number of flood risk areas surrounding a post code
        int get_number_of_flood_risk_areas(string postcode)
        {
            int number_of_flood_risk_areas = 0;

            //get longitude and latitude from postcode
            string[] long_and_lat = get_postcode_long_and_lat(postcode);
            string longitude = long_and_lat[0];
            string latitude = long_and_lat[1];


            //build url
            string url = "floodAreas?lat=" + latitude + "&long=" + longitude + "&dist=1";

            //request api json data
            var client = new RestClient("https://environment.data.gov.uk/flood-monitoring/id/");
            var request = new RestRequest(url, Method.GET);

            //get response and deserialise it into a string
            var response = client.Execute(request);
            JsonDeserializer deserialise = new JsonDeserializer();
            string json_response = deserialise.Deserialize<string>(response);

            //get json and get the value we need (how mant flood areas nearby)
            var json = FloodRiskAssesment.FromJson(json_response);
            number_of_flood_risk_areas = json.Items.Count;

            return number_of_flood_risk_areas;
        }




        int get_number_of_nearby_places(string place_type, string postcode, string how_close_via_driving, string time_unit)
        {
            int number_of_places = 0;

            //get longitude and latitude from postcode
            string[] long_and_lat = get_postcode_long_and_lat(postcode);
            string longitude = long_and_lat[0];
            string latitude = long_and_lat[1];
            string key = "c4KS0Km05Xs8ondgP5Zc~lkdQr73GTTjDT_MbverZjQ~AnONN35zZHkE8U9WpqwmS_ESLNYL-drcWCibvMfmynWAi0Urwbip4pYO0jDRi7wH";

            //build url
            string url = "LocalInsights?waypoint=" + latitude + "," + longitude + "&maxTime=" + how_close_via_driving + "&timeUnit=" + time_unit + "&type=" + place_type + "&key=" + key;

            //request api json data
            var client = new RestClient("https://dev.virtualearth.net/REST/v1/Routes/");
            var request = new RestRequest(url, Method.GET);

            //get response and deserialise it into a string
            var response = client.Execute(request);
            JsonDeserializer deserialise = new JsonDeserializer();
            string json_response = deserialise.Deserialize<string>(response);

            //get json and get the value we need (number of places nearby)
            var json = number_of_nearby_places_var.FromJson(json_response);
            number_of_places = json.ResourceSets[0].Resources[0].CategoryTypeResults[0].Entities.Count;

            return number_of_places;
        }




        int get_time_to_nearest_place(string place_type, string postcode, string time_unit)
        {
            int distance_to_nearest = 0;
            int number_of_places = 0;

            //get longitude and latitude from postcode
            string[] long_and_lat = get_postcode_long_and_lat(postcode);
            string longitude = long_and_lat[0];
            string latitude = long_and_lat[1];
            string key = "c4KS0Km05Xs8ondgP5Zc~lkdQr73GTTjDT_MbverZjQ~AnONN35zZHkE8U9WpqwmS_ESLNYL-drcWCibvMfmynWAi0Urwbip4pYO0jDRi7wH";


            //dynamic variables
            int how_close_via_driving;
            if (time_unit == "second")
            {
                how_close_via_driving = 100;
            }
            else
            {
                how_close_via_driving = 10;
            }

            bool found_closest = false;
            int count = 0;

            //find the distance to the closest specified place
            while (!found_closest)
            {
                //build url
                string url = "LocalInsights?waypoint=" + latitude + "," + longitude + "&maxTime=" + how_close_via_driving.ToString() + "&timeUnit=" + time_unit + "&type=" + place_type + "&key=" + key;

                //request api json data
                var client = new RestClient("https://dev.virtualearth.net/REST/v1/Routes/");
                var request = new RestRequest(url, Method.GET);

                //get response and deserialise it into a string
                var response = client.Execute(request);
                JsonDeserializer deserialise = new JsonDeserializer();
                string json_response = deserialise.Deserialize<string>(response);

                //get json and get the value we need (number of places nearby)
                var json = number_of_nearby_places_var.FromJson(json_response);
                number_of_places = json.ResourceSets[0].Resources[0].CategoryTypeResults[0].Entities.Count();

                if (time_unit == "second")
                {
                    if (number_of_places == 1)
                    {
                        found_closest = true;
                        distance_to_nearest = how_close_via_driving;
                    }
                    else if (number_of_places > 1)
                    {
                        //this if statement will prevent an endless loop
                        if (count >= 3)
                        {
                            found_closest = true;
                            distance_to_nearest = how_close_via_driving;
                        }
                        how_close_via_driving -= 30;
                        count++;
                    }
                    else
                    {
                        if (how_close_via_driving == 900)
                        {
                            how_close_via_driving += 99;
                        }
                        else if (how_close_via_driving == 999)
                        {
                            found_closest = true;
                        }
                        else
                        {
                            how_close_via_driving += 100;
                        }
                    }
                }
                else
                {
                    if (number_of_places == 1)
                    {
                        found_closest = true;
                        distance_to_nearest = how_close_via_driving;
                    }
                    else if (number_of_places > 1)
                    {
                        //this if statement will prevent an endless loop
                        if (count >= 3)
                        {
                            found_closest = true;
                            distance_to_nearest = how_close_via_driving;
                        }
                        how_close_via_driving -= 1;
                        count++;
                    }
                    else
                    {
                        if (how_close_via_driving == 60)
                        {
                            found_closest = true;
                        }
                        else
                        {
                            how_close_via_driving += 10;
                        }
                    }
                }
            }

            return distance_to_nearest;
        }




        //returns distance between two points via roads in KM (requires long and lat of both start and end points) 
        double get_car_distance_between_two_points_via_driving(string start_postcode, string end_postcode)
        {
            //get longitude and latitude from postcode
            string[] start_long_and_lat = get_postcode_long_and_lat(start_postcode);
            double start_point_long = Convert.ToDouble(start_long_and_lat[0]);
            double start_point_lat = Convert.ToDouble(start_long_and_lat[1]);


            string[] end_long_and_lat = get_postcode_long_and_lat(end_postcode);
            double end_point_long = Convert.ToDouble(end_long_and_lat[0]);
            double end_point_lat = Convert.ToDouble(end_long_and_lat[1]);

            string key = "c4KS0Km05Xs8ondgP5Zc~lkdQr73GTTjDT_MbverZjQ~AnONN35zZHkE8U9WpqwmS_ESLNYL-drcWCibvMfmynWAi0Urwbip4pYO0jDRi7wH";

            //build url
            string url = "DistanceMatrix?origins=" + start_point_lat + "," + start_point_long + "&destinations=" + end_point_lat + "," + end_point_long + "&travelMode=driving&key=" + key;

            //request api json data
            var client = new RestClient("https://dev.virtualearth.net/REST/v1/Routes/");
            var request = new RestRequest(url, Method.GET);

            //get response and deserialise it into a string
            var response = client.Execute(request);
            JsonDeserializer deserialise = new JsonDeserializer();
            string json_response = deserialise.Deserialize<string>(response);

            //get json and get the value we need (TavelDistance)
            var json = distance_between_two_points.FromJson(json_response);
            double travel_distance = json.ResourceSets[0].Resources[0].Results[0].TravelDistance;

            //return value
            return travel_distance;
        }





        //gets a list of recorded crimes in a area when given longitude and latitude
        List<String> get_recorded_crimes_in_area(string postcode)
        {
            //get longitude and latitude from postcode
            string[] long_and_lat = get_postcode_long_and_lat(postcode);
            double longitude = Convert.ToDouble(long_and_lat[0]);
            double latitude = Convert.ToDouble(long_and_lat[1]);


            //create a list of crimes in that area
            List<String> crimes_list = new List<String>();
            //create new instance of object
            var policeClient = new PoliceUkClient();
            //enter long and lat from postcode to get police crime details on the area
            var place_to_scan = new Geoposition(latitude, longitude);
            //get 1 year ago date and then work out all of the crimes commited in that area
            DateTime target_date = DateTime.Now.AddMonths(-12).AddDays(2);


            StreetLevelCrimeResults results;
            while (target_date < DateTime.Now)
            {
                results = policeClient.StreetLevelCrimes(place_to_scan, target_date);

                //add all the crime for that month into a list
                foreach (Crime crime in results.Crimes)
                {
                    crimes_list.Add(crime.Category);
                }

                Console.WriteLine(target_date);
                target_date = target_date.AddMonths(1);
            }

            

            return crimes_list;
        }




        dynamic current_data;
        public calculate_results(dynamic data)
        {
            InitializeComponent();
            current_data = data;
        }


        //converts the inputed weights into a decimal 
        double get_weighting_as_decimal(string weighting)
        {
            int weighting_int = Convert.ToInt32(weighting);

            if(weighting_int < 10)
            {
                string building_weight = "0.0" + weighting_int;
                return Convert.ToDouble(building_weight);
            }
            else if(weighting_int == 100)
            {
                return 1.0; 
            }
            else
            {
                string building_weight = "0." + weighting_int;
                return Convert.ToDouble(building_weight);
            }
        }




        private void calculate_results_Load(object sender, EventArgs e)
        {
            get_fuzzy_score_class fuzzy_score = new get_fuzzy_score_class();
            Console.WriteLine(fuzzy_score.get_weather_score(1));
            double final_score = 0.0;


            //user inputs
            string house_postcode = current_data.house_postcode;
            string work_postcode = current_data.work_postcode;
            double distance_from_bar_ideally = Convert.ToDouble(current_data.distance_from_bar);


            //get data from the apis
            List<string> crimes_list = get_recorded_crimes_in_area(house_postcode);
            double distance_to_work = get_car_distance_between_two_points_via_driving(house_postcode, work_postcode);
            int time_to_nearest_bar = get_time_to_nearest_place("Bars", house_postcode, "minute");
            int time_to_nearest_movie_theater = get_time_to_nearest_place("MovieTheaters", house_postcode, "minute");
            int time_to_nearest_hospital = get_time_to_nearest_place("Hospitals", house_postcode, "minute");
            int time_to_nearest_shopping_center = get_time_to_nearest_place("MallsAndShoppingCenters", house_postcode, "minute");
            int number_of_flood_risk_areas = get_number_of_flood_risk_areas(house_postcode);
            int number_of_restaurants = get_number_of_nearby_places("Restaurants", house_postcode, "7", "minute");
            int number_of_parks = get_number_of_nearby_places("Parks", house_postcode, "10", "minute");
            int number_of_takeaways = get_number_of_nearby_places("TakeAway", house_postcode, "10", "minute");
            int number_of_local_grocery_stores = get_number_of_nearby_places("Grocers", house_postcode, "4", "minute");
            int number_of_museums = get_number_of_nearby_places("Museums", house_postcode, "15", "minute");
            string[] weather_temp = get_average_weather_temps(house_postcode);
            int weather_temp_max = Convert.ToInt32(weather_temp[0]);
            int weather_temp_min = Convert.ToInt32(weather_temp[1]);


            //get scores
            get_score_class get_scores = new get_score_class();
            double crime_score = get_scores.get_illegal_activity_score(crimes_list);
            double distance_to_work_score = get_scores.get_distance_to_work_score(distance_to_work);
            double time_to_nearest_bar_score = get_scores.get_distance_from_bar_score(time_to_nearest_bar, distance_from_bar_ideally);
            double time_to_nearest_movie_theater_score = get_scores.get_distance_from_a_movie_theater_score(time_to_nearest_movie_theater);
            double time_to_nearest_hospital_score = get_scores.get_distance_from_a_hospital_score(time_to_nearest_hospital);
            double time_to_nearest_shopping_center_score = get_scores.get_distance_from_a_shopping_center_score(time_to_nearest_shopping_center);
            double number_of_flood_risk_areas_score = get_scores.get_flood_risk_score(number_of_flood_risk_areas);
            double number_of_restaurants_score = get_scores.get_number_of_restaurants_score(number_of_restaurants);
            double number_of_parks_score = get_scores.get_number_of_parks_score(number_of_parks);
            double number_of_takeaways_score = get_scores.get_number_of_takeaways_score(number_of_takeaways);
            double number_of_local_grocery_stores_score = get_scores.get_number_of_grocery_stores_score(number_of_local_grocery_stores);
            double number_of_local_museums_score = get_scores.get_number_of_museums_score(number_of_museums);
            double weather_temp_max_score = get_scores.get_max_weather_temp_score(weather_temp_max);
            double weather_temp_min_score = get_scores.get_min_weather_temp_score(weather_temp_min);
            double weather_temp_score = (weather_temp_max_score + weather_temp_min_score) / 2;


            Console.WriteLine("scores");
            Console.WriteLine(crime_score);
            Console.WriteLine(distance_to_work_score);
            Console.WriteLine(time_to_nearest_bar_score);
            Console.WriteLine(time_to_nearest_movie_theater_score);
            Console.WriteLine(time_to_nearest_hospital_score);
            Console.WriteLine(time_to_nearest_shopping_center_score);
            Console.WriteLine(number_of_flood_risk_areas_score);
            Console.WriteLine(number_of_restaurants_score);
            Console.WriteLine(number_of_parks_score);
            Console.WriteLine(number_of_takeaways_score);
            Console.WriteLine(number_of_local_grocery_stores_score);
            Console.WriteLine(number_of_local_museums_score);
            Console.WriteLine(weather_temp_score);


            //get weights 
            double weighting_crime = get_weighting_as_decimal((current_data.weighting_crime).ToString());
            double weighting_work_distance = get_weighting_as_decimal((current_data.weighting_work_distance).ToString());
            double weighting_bar_distance = get_weighting_as_decimal((current_data.weighting_bar_distance).ToString());
            double weighting_movie_theater_distance = get_weighting_as_decimal((current_data.weighting_movie_theater_distance).ToString());
            double weighting_hospital_distance = get_weighting_as_decimal((current_data.weighting_hospital_distance).ToString());
            double weighting_shopping_center_distance = get_weighting_as_decimal((current_data.weighting_shopping_center_distance).ToString());
            double weighting_flood_areas_number = get_weighting_as_decimal((current_data.weighting_flood_areas_number).ToString());
            double weighting_resturants_number = get_weighting_as_decimal((current_data.weighting_restaurants_number).ToString());
            double weighting_parks_number = get_weighting_as_decimal((current_data.weighting_parks_number).ToString());
            double weighting_takeaways_number = get_weighting_as_decimal((current_data.weighting_takeaways_number).ToString());
            double weighting_local_grocery_stores_number = get_weighting_as_decimal((current_data.weighting_local_grocery_stores_number).ToString());
            double weighting_museums_number = get_weighting_as_decimal((current_data.weighting_museums_number).ToString());
            double weighting_weather_temps = get_weighting_as_decimal((current_data.weighting_weather_temps).ToString());


            //measure scores against weightings
            double crime_result = crime_score * weighting_crime;
            double distance_to_work_result = distance_to_work_score * weighting_work_distance;
            double time_to_nearest_bar_result = time_to_nearest_bar_score * weighting_bar_distance;
            double time_to_nearest_movie_theater_result = time_to_nearest_movie_theater_score * weighting_movie_theater_distance;
            double time_to_nearest_hospital_result = time_to_nearest_hospital_score * weighting_hospital_distance;
            double time_to_nearest_shopping_center_result = time_to_nearest_shopping_center_score * weighting_shopping_center_distance;
            double number_of_flood_risk_areas_result = number_of_flood_risk_areas_score * weighting_flood_areas_number;
            double number_of_restaurants_result = number_of_restaurants_score * weighting_resturants_number;
            double number_of_parks_result = number_of_parks_score * weighting_parks_number;
            double number_of_takeaways_result = number_of_takeaways_score * weighting_takeaways_number;
            double number_of_local_grocery_stores_result = number_of_local_grocery_stores_score * weighting_local_grocery_stores_number;
            double number_of_museums_result = number_of_local_museums_score * weighting_museums_number;
            double weather_temp_result = weather_temp_score * weighting_weather_temps;


            //working out final score and display a breakdown of the score to the user
            final_score =
                    crime_result + distance_to_work_result + time_to_nearest_bar_result + time_to_nearest_movie_theater_result + time_to_nearest_hospital_result +
                    time_to_nearest_shopping_center_result + number_of_flood_risk_areas_result + number_of_restaurants_result + number_of_parks_result +
                    number_of_takeaways_result + number_of_local_grocery_stores_result + number_of_museums_result + weather_temp_result;

            txt_results.Text = "This location scored:   " + Math.Round(final_score, 2, MidpointRounding.AwayFromZero).ToString() + "   Out of 1 " +
                "\r\n\r\nScores:" +  
                    "\r\n \t Crime in the area scored:   " + crime_score.ToString() + "   and contributed   " + (current_data.weighting_crime).ToString() + "%   to the final score" + 
                    "\r\n\t Distance to work scored:   " + distance_to_work_score.ToString() + "   and contributed   " + (current_data.weighting_work_distance).ToString() + "%   to the final score" +
                    "\r\n\t Distance from the nearest bar scored:   " + time_to_nearest_bar_score.ToString() + "   and contributed   " + (current_data.weighting_bar_distance).ToString() + "%   to the final score" +
                    "\r\n\t Distance to the nearest movie theater scored:   " + time_to_nearest_movie_theater_score.ToString() + "   and contributed   " + (current_data.weighting_movie_theater_distance).ToString() + "%   to the final score" +
                    "\r\n\t Distance to the nearest hospital scored:   " + time_to_nearest_hospital_score.ToString() + "   and contributed   " + (current_data.weighting_hospital_distance).ToString() + "%   to the final score" +
                    "\r\n\t Distance to the nearest shopping center scored:   " + time_to_nearest_shopping_center_score.ToString() + "   and contributed   " + (current_data.weighting_shopping_center_distance).ToString() + "%   to the final score" +
                    "\r\n\t Number of flood risk areas in the area scored:   " + number_of_flood_risk_areas_score.ToString() + "   and contributed   " + (current_data.weighting_flood_areas_number).ToString() + "%   to the final score" +
                    "\r\n\t Number of restaurants in the area scored:   " + number_of_restaurants_score.ToString() + "   and contributed   " + (current_data.weighting_restaurants_number).ToString() + "%   to the final score" +
                    "\r\n\t Number of parks in the area scored:   " + number_of_parks_score.ToString() + "   and contributed   " + (current_data.weighting_parks_number).ToString() + "%   to the final score" +
                    "\r\n\t Number of takeaways in the area scored:   " + number_of_takeaways_score.ToString() + "   and contributed   " + (current_data.weighting_takeaways_number).ToString() + "%   to the final score" +
                    "\r\n\t Number of grocery stores in the area scored:   " + number_of_local_grocery_stores_score.ToString() + "   and contributed   " + (current_data.weighting_local_grocery_stores_number).ToString() + "%   to the final score" +
                    "\r\n\t Number of museums in the area scored:   " + number_of_local_museums_score.ToString() + "   and contributed   " + (current_data.weighting_museums_number).ToString() + "%   to the final score" +
                    "\r\n\t The weather temprature of the area scored:   " + weather_temp_score.ToString() + "   and contributed   " + (current_data.weighting_weather_temps).ToString() + "%   to the final score";
        }
    }
}
