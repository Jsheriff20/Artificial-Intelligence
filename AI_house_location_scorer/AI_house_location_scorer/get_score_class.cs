using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AI_house_location_scorer
{
    class get_score_class
    {
        //functions to get a decimal score of what an element has been scored

        public double get_flood_risk_score(int number_of_flood_risk_areas)
        {
            switch (number_of_flood_risk_areas)
            {
                case 0:
                    return 1.0;

                case var expression when (number_of_flood_risk_areas > 0 && number_of_flood_risk_areas <= 2):
                    return 0.8;

                case var expression when (number_of_flood_risk_areas > 2 && number_of_flood_risk_areas <= 4):
                    return 0.6;

                case var expression when (number_of_flood_risk_areas > 0 && number_of_flood_risk_areas <= 6):
                    return 0.4;

                case var expression when (number_of_flood_risk_areas > 0 && number_of_flood_risk_areas <= 5):
                    return 0.2;

                default:
                    return 0.0;
            }
        }

        public double get_illegal_activity(List<String> crimes_list)
        {
            int number_of_crimes = crimes_list.Count();
            double number_of_crimes_penalty = (number_of_crimes / 10)/2.5;
            double score_of_all_crimes = number_of_crimes_penalty;
            score_of_all_crimes = Math.Round(score_of_all_crimes, 2);
            double average_score_of_crimes = score_of_all_crimes /number_of_crimes;

            foreach (String crime in crimes_list)
            {
                switch (crime)
                {
                    case "anti-social-behaviour":
                        score_of_all_crimes = score_of_all_crimes + 0.1;
                        break;
                    case "burglary":
                        score_of_all_crimes = score_of_all_crimes + 0.6;
                        break;
                    case "criminal-damage-arson":
                        score_of_all_crimes = score_of_all_crimes + 0.5;
                        break;
                    case "drugs":
                        score_of_all_crimes = score_of_all_crimes + 0.2;
                        break;
                    case "other-theft":
                        score_of_all_crimes = score_of_all_crimes + 0.3;
                        break;
                    case "public-order":
                        score_of_all_crimes = score_of_all_crimes + 0.1;
                        break;
                    case "vehicle-crime":
                        score_of_all_crimes = score_of_all_crimes + 0.3;
                        break;
                    case "violent-crime":
                        score_of_all_crimes = score_of_all_crimes + 0.9;
                        break;
                    case "other-crime":
                        score_of_all_crimes = score_of_all_crimes + 0.2;
                        break;
                    case "robbery":
                        score_of_all_crimes = score_of_all_crimes + 0.5;
                        break;
                    case "theft-from-the-person":
                        score_of_all_crimes = score_of_all_crimes + 0.6;
                        break;
                    case "possession-of-weapons":
                        score_of_all_crimes = score_of_all_crimes + 0.3;
                        break;
                    case "shoplifting":
                        score_of_all_crimes = score_of_all_crimes + 0.2;
                        break;
                    case "bicycle-theft":
                        score_of_all_crimes = score_of_all_crimes + 0.1;
                        break;
                    default:
                        score_of_all_crimes = score_of_all_crimes + 0.1;
                        break;
                }
            }

            return average_score_of_crimes;
        }
    }
}
