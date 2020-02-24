using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FLS;
using FLS.Rules;

namespace AI_house_location_scorer
{
    class get_fuzzy_score_class
    {
        //fuzzification of variables 
        //max of 8
        public double get_weather_score(int number_of_flood_risk_areas)
        {


            var flood_risk = new LinguisticVariable("flood_risk");
            var flood_very_common = flood_risk.MembershipFunctions.AddTrapezoid("very_common", 0, 0, 20, 40);
            var flood_common = flood_risk.MembershipFunctions.AddTriangle("common", 30, 50, 70);
            var flood_frequent = flood_risk.MembershipFunctions.AddTriangle("frequent", 30, 50, 70);
            var flood_occasional = flood_risk.MembershipFunctions.AddTriangle("occasional", 30, 50, 70);
            var flood_rare = flood_risk.MembershipFunctions.AddTriangle("rare", 30, 50, 70);
            var flood_never_seen = flood_risk.MembershipFunctions.AddTriangle("never_seen", 30, 50, 70);

            var min_temp = new LinguisticVariable("min_temp");
            var min_temp_terrible = min_temp.MembershipFunctions.AddTrapezoid("very_likely", 0, 0, 20, 40);
            var min_temp_bad = min_temp.MembershipFunctions.AddTriangle("likely", 30, 50, 70);
            var min_temp_poor = min_temp.MembershipFunctions.AddTriangle("moderate", 30, 50, 70);
            var min_temp_okay = min_temp.MembershipFunctions.AddTriangle("", 30, 50, 70);
            var min_temp_above_average = min_temp.MembershipFunctions.AddTriangle("", 30, 50, 70);
            var min_temp_good = min_temp.MembershipFunctions.AddTriangle("good", 30, 50, 70);
            var min_temp_excellent = min_temp.MembershipFunctions.AddTrapezoid("excellent", 50, 80, 100, 100);
            
            var max_temp = new LinguisticVariable("max_temp");
            var max_temp_terrible = max_temp.MembershipFunctions.AddTrapezoid("terrible", 0, 0, 20, 40);
            var max_temp_bad = max_temp.MembershipFunctions.AddTriangle("bad", 30, 50, 70);
            var max_temp_poor = max_temp.MembershipFunctions.AddTriangle("poor", 30, 50, 70);
            var max_temp_okay = max_temp.MembershipFunctions.AddTriangle("okay", 30, 50, 70);
            var max_temp_above_average = max_temp.MembershipFunctions.AddTriangle("above_average", 30, 50, 70);
            var max_temp_good = max_temp.MembershipFunctions.AddTriangle("good", 30, 50, 70);
            var max_temp_excellent = max_temp.MembershipFunctions.AddTrapezoid("excellent", 50, 80, 100, 100);

            var weather_scorer = new LinguisticVariable("weather_scorer");
            var weather_scorer_terrible = weather_scorer.MembershipFunctions.AddTrapezoid("terrible", 0, 0, 20, 40);
            var weather_scorer_bad = weather_scorer.MembershipFunctions.AddTriangle("bad", 30, 50, 70);
            var weather_scorer_poor = weather_scorer.MembershipFunctions.AddTriangle("poor", 30, 50, 70);
            var weather_scorer_okay = weather_scorer.MembershipFunctions.AddTriangle("okay", 30, 50, 70);
            var weather_scorer_above_average = weather_scorer.MembershipFunctions.AddTriangle("above_average", 30, 50, 70);
            var weather_scorer_good = weather_scorer.MembershipFunctions.AddTriangle("good", 30, 50, 70);
            var weather_scorer_excellent = weather_scorer.MembershipFunctions.AddTrapezoid("excellent", 50, 80, 100, 100);

            IFuzzyEngine fuzzyEngine = new FuzzyEngineFactory().Default();

            var rule1 = Rule.If(flood_risk.Is(cold).Or(water.Is(warm))).Then(power.Is(high));
            var rule2 = Rule.If(water.Is(hot)).Then(power.Is(low));
            fuzzyEngine.Rules.Add(rule1, rule2);

            var result = fuzzyEngine.Defuzzify(new { water = 60 });

            return result;
            //double fuzzy_value;

            //double terrible = 0.7;
            //double bad = 0.5;
            //double poor = 0.4;
            //double okay = 0.3;
            //double above_average = 0.2; 
            //double good = 0.1;
            //double excellent = 0.0;

            //int number_of_possibilities = 0;
            //double postion_of_triangle = 0.0;

            ////terrible;
            //if (number_of_flood_risk_areas > 7)
            //{
            //    number_of_possibilities = 2;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    terrible = terrible + (1 * postion_of_triangle);
            //}

            ////bad
            //if (number_of_flood_risk_areas >= 6 && number_of_flood_risk_areas <= 8)
            //{
            //    number_of_possibilities = 3;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    bad = bad + (1 * postion_of_triangle);
            //}

            ////poor
            //if (number_of_flood_risk_areas >= 4 && number_of_flood_risk_areas <= 7)
            //{
            //    number_of_possibilities = 4;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    poor = poor + (1 * postion_of_triangle);
            //}

            ////okay
            //if (number_of_flood_risk_areas >= 3 && number_of_flood_risk_areas <= 5)
            //{
            //    number_of_possibilities = 3;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    okay = okay + (1 * postion_of_triangle);
            //}

            ////above_average
            //if (number_of_flood_risk_areas >= 2 && number_of_flood_risk_areas <= 4)
            //{
            //    number_of_possibilities = 3;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    above_average = above_average + (1 * postion_of_triangle);
            //}

            ////good
            //if (number_of_flood_risk_areas >= 1 && number_of_flood_risk_areas <= 3)
            //{
            //    number_of_possibilities = 3;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    good = good + (1 * postion_of_triangle);
            //}

            ////excellent
            //if (number_of_flood_risk_areas >= 0 && number_of_flood_risk_areas <= 2)
            //{
            //    number_of_possibilities = 3;
            //    postion_of_triangle = 1 / number_of_possibilities;
            //    excellent = excellent + (1 * postion_of_triangle);
            //}

            //fuzzy_value = terrible + bad + poor + okay + above_average + good + excellent;

            //return fuzzy_value;
        }

    }
}
