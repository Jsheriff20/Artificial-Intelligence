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
            aspects_to_monitor next_form = new aspects_to_monitor();
            next_form.Show();
            this.Hide();
        }
    }
}
