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
    public partial class enter_details : Form
    {
        public enter_details()
        {
            InitializeComponent();
        }

        private void btn_next_Click(object sender, EventArgs e)
        {
            dynamic data = new JObject();
            data.house_postcode = txt_house_postcode.Text;
            data.work_postcode = txt_works_postcode.Text;
            data.distance_from_bar = txt_min_distance_to_bar.Text;

            aspects_to_monitor next_form = new aspects_to_monitor(data);
            next_form.Show();
            this.Hide();
        }

        private void enter_details_Load(object sender, EventArgs e)
        {

        }
    }
}
