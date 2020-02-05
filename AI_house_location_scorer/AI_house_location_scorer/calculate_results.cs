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
            IRestResponse rest_response = new RestResponse {Content = json_response["result"]};
            var decoded_response = deserialise.Deserialize<Dictionary<string, string>>(rest_response);



            //return longitude & latitude array
            long_and_lat[0] = decoded_response["longitude"];
            long_and_lat[1] = decoded_response["latitude"];
            return long_and_lat;
        }



        //returns distance between two points via roads in KM (requires long and lat of both start and end points) 
        string get_car_distance_between_two_points(string[] start_point, string[] end_point)
        {
            string start_point_lat = start_point[0].ToString();
            string start_point_long = start_point[1].ToString();
            string end_point_lat = end_point[0].ToString();
            string end_point_long = end_point[1].ToString();
            string key = "c4KS0Km05Xs8ondgP5Zc~lkdQr73GTTjDT_MbverZjQ~AnONN35zZHkE8U9WpqwmS_ESLNYL-drcWCibvMfmynWAi0Urwbip4pYO0jDRi7wH";

            string uri = "DistanceMatrix?origins=" + start_point_lat + "," + start_point_long + "&destinations=" + end_point_lat + "," + end_point_long + "&travelMode=driving&key=" + key;
            //request api json data
            var client = new RestClient("https://dev.virtualearth.net/REST/v1/Routes/");
            var request = new RestRequest(uri, Method.GET);

            //get response and decode it
            var response = client.Execute(request);
            JsonDeserializer deserialise = new JsonDeserializer();
            var json_response = deserialise.Deserialize<Dictionary<string, string>>(response);


            //encode result into json array then decode the inner array
            IRestResponse rest_response = new RestResponse {Content = json_response["resourceSets"]};
            var decoded_response = deserialise.Deserialize<Dictionary<string, string>>(rest_response); //the rest_response value ends up having content with square brackets (works fine when these first pair of square brackets are removed)



            //return longitude & latitude array
            string travel_distance = decoded_response["travelDistance"];
            return travel_distance;
        }





        //gets a list of recorded crimes in a area when given longitude and latitude
        List<String> get_recorded_crimes_in_area(string postcode)
        {
            //get longitude and latitude from postcode
            string[] long_and_lat = new string[2];
            long_and_lat = get_postcode_long_and_lat(postcode);
            double longitude = Convert.ToDouble(long_and_lat[0]);
            double latitude = Convert.ToDouble(long_and_lat[1]);


            //create new instance of object
            var policeClient = new PoliceUkClient();

            //enter long and lat from postcode to get police crime details on the area
            var place_to_scan = new Geoposition(latitude, longitude);
            StreetLevelCrimeResults results = policeClient.StreetLevelCrimes(place_to_scan);


            //create a list of crimes in that area
            List<String> crimes_list = new List<String>();
            foreach (Crime crime in results.Crimes)
            {
                crimes_list.Add(crime.Category);
            }

            return crimes_list;
        }






        public calculate_results()
        {
            InitializeComponent();
        }

        private void calculate_results_Load(object sender, EventArgs e)
        {

            //List<String> crimes_list = get_recorded_crimes_in_area("ne38 9ex");
            //foreach (String crime in crimes_list)
            //{
            //    Console.WriteLine(crime);
            //}

            string[] start_point = {"47.6044", "-122.3345"};
            string[] end_point = {"45.5347", "-122.6231"};
            Console.WriteLine(get_car_distance_between_two_points(start_point, end_point));
        }
    }
}
