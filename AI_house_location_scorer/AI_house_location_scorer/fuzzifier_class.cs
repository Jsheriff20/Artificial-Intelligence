using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_house_location_scorer
{
    class fuzzifier_class
    {
        //fuzzification of variables 
        //max of 8
        public double[] fuzzify_flood_risk(int number_of_flood_risk_areas)
        {
            double[] fuzzy_values = new double[7];

            double terrible = 0.0;
            double bad = 0.0;
            double poor = 0.0;
            double okay = 0.0;
            double above_average = 0.0;
            double good = 0.0;
            double excellent = 0.0;

            int number_of_possibilities = 0;
            double postion_of_triangle = 0.0;
            int min_value = 0;

            //terrible;
            if (number_of_flood_risk_areas > 7)
            {

                min_value = 8;
                number_of_possibilities = 2;

                if (number_of_flood_risk_areas > 10)
                {
                    terrible = (10 - min_value) / number_of_possibilities;
                }
                else
                {
                    terrible = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                }

                fuzzy_values[0] = terrible;
            }

            //bad
            if (number_of_flood_risk_areas >= 6 && number_of_flood_risk_areas <= 8)
            {

                min_value = 6;
                number_of_possibilities = 2;
                bad = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[1] = bad;
            }

            //poor
            if (number_of_flood_risk_areas >= 4 && number_of_flood_risk_areas <= 7)
            {
                min_value = 4;
                number_of_possibilities = 3;
                poor = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[2] = poor;
            }

            //okay
            if (number_of_flood_risk_areas >= 3 && number_of_flood_risk_areas <= 5)
            {
                min_value = 3;
                number_of_possibilities = 2;
                okay = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[3] = okay;
            }

            //above_average
            if (number_of_flood_risk_areas >= 2 && number_of_flood_risk_areas <= 4)
            {
                min_value = 3;
                number_of_possibilities = 2;
                above_average = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[4] = above_average;
            }

            //good
            if (number_of_flood_risk_areas >= 1 && number_of_flood_risk_areas <= 3)
            {
                min_value = 1;
                number_of_possibilities = 2;
                good = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[5] = good;
            }

            //excellent
            if (number_of_flood_risk_areas >= 0 && number_of_flood_risk_areas <= 2)
            {

                min_value = 0;
                number_of_possibilities = 2;
                excellent = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[6] = excellent;
            }

            return fuzzy_values;
        }




        // fuzzify min weather values
        public double[] fuzzy_min_weather_temp(int temp)
        {
            double[] fuzzy_values = new double[5];

            double freezing = 0.0;
            double cold = 0.0;
            double medium = 0.0;
            double warm = 0.0;
            double hot = 0.0;

            int number_of_possibilities = 0;
            double postion_of_triangle = 0.0;
            int min_value = 0;

            //freezing;
            if (temp <  -6)
            {
                min_value = -11;
                number_of_possibilities = 4;

                int local_number_of_flood_risk_areas = 0;
                if (temp < -11)
                {
                    local_number_of_flood_risk_areas = -11;
                }

                //-1 are there to convert the number to a postive to help with the maths
                freezing = (((temp * -1) + min_value) / number_of_possibilities) * -1;
                fuzzy_values[0] = freezing;
            }

            //cold
            if (temp <= -8 && temp >= -4)
            {
                min_value = -8;
                number_of_possibilities = 4;
                cold = (((temp * -1) + min_value) / number_of_possibilities) * -1;
                fuzzy_values[1] = cold;
            }

            //medium
            if (temp <= -6 && temp >= -1)
            {
                min_value = -6;
                number_of_possibilities = 5;
                medium = (((temp * -1) + min_value) / number_of_possibilities) * -1;
                fuzzy_values[2] = medium;
            }

            //warm
            if (temp <= -4 && temp >= 1)
            {
                min_value = -4;
                number_of_possibilities = 5;
                if(temp < 0)
                {
                    temp = temp * -1;
                }
                warm = ((temp + min_value) / number_of_possibilities) * -1;
                fuzzy_values[3] = okay;
            }

            //hot
            if (number_of_flood_risk_areas >= 2 && number_of_flood_risk_areas <= 4)
            {
                min_value = 3;
                number_of_possibilities = 3;
                above_average = (number_of_flood_risk_areas - min_value) / number_of_possibilities;
                fuzzy_values[4] = above_average;
            }

            return fuzzy_values;
        }
    }
}
