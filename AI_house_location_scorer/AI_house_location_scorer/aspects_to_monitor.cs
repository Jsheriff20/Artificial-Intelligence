using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AI_house_location_scorer
{
    public partial class aspects_to_monitor : Form
    {
        // functions

        private void calculate_total_percentage(params int[] percentages)
        {
            int total = 0;

            for (int i = 0; i < percentages.Length; i++)
            {
                total += percentages[i];
            }


            txt_total_percentage.Text = (total.ToString() + "%");
        }
        dynamic current_data;

        public aspects_to_monitor(dynamic data)
        {
            InitializeComponent();
            current_data = data;
        }

        private void btn_back_Click(object sender, EventArgs e)
        {
            enter_details back_form = new enter_details();
            back_form.Show();
            this.Hide();
        }

        private void nud_weighting_ValueChanged(object sender, EventArgs e)
        {
            //calcuates the toal percentage across all numericUpDown inputs
            calculate_total_percentage(
                Decimal.ToInt16(nud_distance_from_work.Value),
                Decimal.ToInt16(nud_illegal_activity.Value));

            //does not let the numericUpDown add up higher than 100% & will then reset once total is below 100%
            if (txt_total_percentage.Text == "100%")
            {
                nud_distance_from_work.Maximum = nud_distance_from_work.Value;
                nud_illegal_activity.Maximum = nud_illegal_activity.Value;
            }
            else
            {
                nud_distance_from_work.Maximum = 100;
                nud_illegal_activity.Maximum = 100;
            }
        }

        private void aspects_to_monitor_Load(object sender, EventArgs e)
        {

        }

        private void btn_get_score_Click(object sender, EventArgs e)
        {
            dynamic data = current_data;
            data.weighting_crime = nud_illegal_activity.Value.ToString();
            data.weighting_work_distance = nud_distance_from_work.Value.ToString();
            data.weighting_bar_distance = nud_distance_from_a_bar.Value.ToString();
            data.weighting_movie_theater_distance = nud_distance_from_a_movie_theater.Value.ToString();
            data.weighting_hospital_distance = nud_distance_from_a_hospital.Value.ToString();
            data.weighting_shopping_center_distance = nud_distance_from_a_shopping_center.Value.ToString();
            data.weighting_flood_areas_number = nud_number_of_flood_areas.Value.ToString();
            data.weighting_restaurants_number = nud_number_of_restaurants.Value.ToString();
            data.weighting_parks_number = nud_number_of_parks.Value.ToString();
            data.weighting_takeaways_number = nud_number_of_takeaways.Value.ToString();
            data.weighting_local_grocery_stores_number = nud_number_of_local_grocery_stores.Value.ToString();
            data.weighting_museums_number = nud_number_of_museums.Value.ToString();
            data.weighting_weather_temps = nud_weather_temp.Value.ToString();

            calculate_results next_form = new calculate_results(data);
            next_form.Show();
            this.Hide();
        }
    }
}
