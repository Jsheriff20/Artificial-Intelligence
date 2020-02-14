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




        int get_time_to_nearest_places(string place_type, string postcode, string time_unit)
        {
            int distance_to_nearest = 0;
            int number_of_places = 0;

            //get longitude and latitude from postcode
            string[] long_and_lat = get_postcode_long_and_lat(postcode);
            string longitude = long_and_lat[0];
            string latitude = long_and_lat[1];
            string key = "c4KS0Km05Xs8ondgP5Zc~lkdQr73GTTjDT_MbverZjQ~AnONN35zZHkE8U9WpqwmS_ESLNYL-drcWCibvMfmynWAi0Urwbip4pYO0jDRi7wH";
           
            
            //dynamic variables
            int how_close_in_sec_via_driving = 100;
            bool found_closest = false;
            int thirty_count = 0;

            //find the distance to the closest specified place
            while (!found_closest)
            {
                //build url
                string url = "LocalInsights?waypoint=" + latitude + "," + longitude + "&maxTime=" + how_close_in_sec_via_driving.ToString() + "&timeUnit=" + time_unit + "&type=" + place_type + "&key=" + key;

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

                if(number_of_places == 1)
                {
                    found_closest = true;
                    distance_to_nearest = how_close_in_sec_via_driving;
                }
                else if(number_of_places > 1)
                {
                    //this if statement will prevent an endless loop
                    if(thirty_count >= 3)
                    {
                        found_closest = true;
                        distance_to_nearest = how_close_in_sec_via_driving;
                    }
                    how_close_in_sec_via_driving -= 30;
                    thirty_count++;
                }
                else
                {
                    how_close_in_sec_via_driving += 100;
                }
            }

            return distance_to_nearest;
        }




        //returns distance between two points via roads in KM (requires long and lat of both start and end points) 
        double get_car_distance_between_two_points_via_driving(string[] start_point, string[] end_point)
        {
            string start_point_lat = start_point[0].ToString();
            string start_point_long = start_point[1].ToString();
            string end_point_lat = end_point[0].ToString();
            string end_point_long = end_point[1].ToString();
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





        public calculate_results()
        {
            InitializeComponent();
        }

        private void calculate_results_Load(object sender, EventArgs e)
        {

            //Console.WriteLine(get_average_weather_temps("ne389ex")[0]);
            //Console.WriteLine(get_average_weather_temps("ne389ex")[1]);

            //List<String> crimes_list = get_recorded_crimes_in_area("CM1 4TS");
            //get_score_class get_scores = new get_score_class();
            //Console.WriteLine(get_scores.get_illegal_activity_score(crimes_list));

            //Console.WriteLine(get_distance_to_nearest_places("Parks", "Ne389ex"));

            //string[] start_point = {"47.6044", "-122.3345"};
            //string[] end_point = {"45.5347", "-122.6231"};
            //Console.WriteLine(get_car_distance_between_two_points(start_point, end_point));

            //Console.WriteLine(number_of_nearby_places("Parks", "ne389ex", "999"));

            //Console.WriteLine(get_flood_risk_score(get_number_of_flood_risk_areas("dd14dh")));
        }
    }
}
