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

        public aspects_to_monitor()
        {
            InitializeComponent();
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
            calculate_results next_form = new calculate_results();
            next_form.Show();
            this.Hide();
        }
    }
}
