using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using RestSharp;
using RestSharp.Authenticators;


namespace AI_house_location_scorer
{
    public partial class calculate_results : Form
    {
        // get api results - functions



        int[] get_postcode_long_and_lat(string postcode)
        {
            int[] long_and_lat = new int[2];

            var client = new RestClient("api.postcodes.io/postcodes/" + postcode);

            string url = string.Format("api.postcodes.io/postcodes/" + postcode );
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            HttpWebResponse response = null;
            response = (HttpWebResponse)request.GetResponse();

            string result = "";
            using (Stream stream = response.GetResponseStream())
            {
                StreamReader stream_reader = new StreamReader(stream);
                result = stream_reader.ReadToEnd();
                stream_reader.Close();
            }

        }


        public calculate_results()
        {
            InitializeComponent();
        }

        private void calculate_results_Load(object sender, EventArgs e)
        {

        }
    }
}
