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

                case var expression when (number_of_flood_risk_areas <= 2):
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

        private double get_illegal_activity_points(List<String> crimes_list)
        {
            double number_of_crimes = crimes_list.Count();
            double score_of_all_crimes = 0;
            double multiplier = 1;
            int count = 0;

            foreach (String crime in crimes_list)
            {
                switch (crime)
                {
                    case "anti-social-behaviour":
                        score_of_all_crimes += 0.3;
                        break;
                    case "burglary":
                        score_of_all_crimes += 0.9;
                        break;
                    case "criminal-damage-arson":
                        score_of_all_crimes += 0.8;
                        break;
                    case "drugs":
                        score_of_all_crimes += 0.4;
                        break;
                    case "other-theft":
                        score_of_all_crimes += 0.6;
                        break;
                    case "public-order":
                        score_of_all_crimes += 0.1;
                        break;
                    case "vehicle-crime":
                        score_of_all_crimes += 0.5;
                        break;
                    case "violent-crime":
                        score_of_all_crimes += 1;
                        break;
                    case "other-crime":
                        score_of_all_crimes += 0.2;
                        break;
                    case "robbery":
                        score_of_all_crimes += 0.8;
                        break;
                    case "theft-from-the-person":
                        score_of_all_crimes += 1;
                        break;
                    case "possession-of-weapons":
                        score_of_all_crimes += 0.7;
                        break;
                    case "shoplifting":
                        score_of_all_crimes += 0.2;
                        break;
                    case "bicycle-theft":
                        score_of_all_crimes += 0.1;
                        break;
                    default:
                        score_of_all_crimes += 0.1;
                        break;
                }

                score_of_all_crimes += 0.05 * multiplier;

                if (count >= (multiplier * 40))
                {
                    if (multiplier < 5)
                    {
                        multiplier++;
                    }
                    else if (multiplier < 10)
                    {
                        multiplier += 0.5;
                    }
                    else
                    {
                        multiplier += 0.3;
                    }
                    count = 0;
                }
                count++;
            }


            double average_score_of_crimes = Math.Round(score_of_all_crimes, 1) / number_of_crimes;

            return Math.Round(average_score_of_crimes, 2);
        }


        //pairs with the get_illegal_activity_points() function
        public double get_illegal_activity_score(double average_score_of_crimes)
        {
            switch (average_score_of_crimes)
            {
                case var expression when (average_score_of_crimes < 0.65):
                    return 1.0;

                case var expression when (average_score_of_crimes >= 0.65 && average_score_of_crimes < 0.69):
                    return 0.9;

                case var expression when (average_score_of_crimes >= 0.69 && average_score_of_crimes < 0.72):
                    return 0.8;

                case var expression when (average_score_of_crimes >= 0.72 && average_score_of_crimes < 0.75):
                    return 0.7;

                case var expression when (average_score_of_crimes >= 0.75 && average_score_of_crimes < 0.79):
                    return 0.6;

                case var expression when (average_score_of_crimes >= 0.79 && average_score_of_crimes < 0.83):
                    return 0.5;

                case var expression when (average_score_of_crimes >= 0.83 && average_score_of_crimes < 0.86):
                    return 0.4;

                case var expression when (average_score_of_crimes >= 0.86 && average_score_of_crimes < 0.90):
                    return 0.3;

                case var expression when (average_score_of_crimes >= 0.90 && average_score_of_crimes < 0.95):
                    return 0.2;

                case var expression when (average_score_of_crimes >= 0.95 && average_score_of_crimes < 1):
                    return 0.1;

                default:
                    return 0.0;
            }
        }

        //pairs with the get_car_distance_between_two_points_via_driving() function
        public double get_distance_from_work_score(double distance)
        {
            switch (distance)
            {
                case var expression when (distance < 1.6):
                    return 1.0;

                case var expression when (distance >= 1.6 && distance < 3.2):
                    return 0.8;

                case var expression when (distance >= 3.2 && distance < 8):
                    return 0.6;

                case var expression when (distance >= 8 && distance < 16.1):
                    return 0.4;

                case var expression when (distance >= 16.1 && distance < 40.2):
                    return 0.2;

                default:
                    return 0.0;
            }
        }



        //pairs with the get_distance_to_nearest_places() function with the time unit specified as seconds
        public double get_distance_from_a_movie_theater_score(int distance_time)
        {
            switch (distance_time)
            {
                case var expression when (distance_time < 180):
                    return 1.0;

                case var expression when (distance_time >= 180 && distance_time < 300):
                    return 0.8;

                case var expression when (distance_time >= 300 && distance_time < 480):
                    return 0.6;

                case var expression when (distance_time >= 480 && distance_time < 660):
                    return 0.4;

                case var expression when (distance_time >= 660 && distance_time < 900):
                    return 0.2;

                default:
                    return 0.0;
            }
        }



        //pairs with the get_distance_to_nearest_places() function with the time unit specified as minutes
        public double get_distance_from_a_hospital_score(int distance_time)
        {
            switch (distance_time)
            {
                case var expression when (distance_time < 20):
                    return 1.0;

                //33 being urban areas average
                case var expression when (distance_time >= 20 && distance_time < 33):
                    return 0.8;

                case var expression when (distance_time >= 33 && distance_time < 43):
                    return 0.6;

                //57 being rural areas average
                case var expression when (distance_time >= 43 && distance_time < 57):
                    return 0.4;

                case var expression when (distance_time >= 57 && distance_time < 80):
                    return 0.2;

                default:
                    return 0.0;
            }
        }



        //pairs with the get_distance_to_nearest_places() function with the time unit specified as minutes
        public double get_distance_from_a_shopping_center_score(int distance_time)
        {
            switch (distance_time)
            {
                case var expression when (distance_time < 5):
                    return 1.0;

                case var expression when (distance_time >= 5 && distance_time < 10):
                    return 0.8;

                case var expression when (distance_time >= 10 && distance_time < 15):
                    return 0.6;

                case var expression when (distance_time >= 15 && distance_time < 20):
                    return 0.4;

                case var expression when (distance_time >= 20 && distance_time < 25):
                    return 0.2;

                default:
                    return 0.0;
            }
        }



        //pairs with the get_distance_to_nearest_places() function with the time unit specified as minutes
        public double get_number_of_grocery_stores_score(int number)
        {
            switch (number)
            {
                case var expression when (number > 3):
                    return 1.0;

                case var expression when (number <= 3 && number > 1):
                    return 0.7;

                case var expression when (number == 1):
                    return 0.3;

                default:
                    return 0.0;
            }
        }


        public double get_number_of_restaurants_score(int number)
        {
            switch (number)
            {
                case var expression when (number > 3):
                    return 1.0;

                case var expression when (number <= 3 && number > 1):
                    return 0.7;

                case var expression when (number == 1):
                    return 0.3;

                default:
                    return 0.0;
            }
        }

        public double get_number_of_parks_score(int number)
        {
            switch (number)
            {
                case var expression when (number > 3):
                    return 1.0;

                case var expression when (number <= 3 && number > 1):
                    return 0.7;

                case var expression when (number == 1):
                    return 0.3;

                default:
                    return 0.0;
            }
        }



        public double get_number_of_takeaways_score(int number)
        {
            switch (number)
            {
                case var expression when (number > 6):
                    return 1.0;

                case var expression when (number <= 6 && number > 3):
                    return 0.7;

                case var expression when (number >= 3):
                    return 0.3;

                default:
                    return 0.0;
            }
        }



        public double get_distance_from_bar_score(int distance_time, double ideal_minimum)
        {
            switch (distance_time)
            {
                case var expression when (distance_time > ideal_minimum):
                    return 1.0;

                case var expression when (distance_time <= ideal_minimum && distance_time > (ideal_minimum * 0.9)):
                    return 0.9;

                case var expression when (distance_time <= (ideal_minimum * 0.9) && distance_time > (ideal_minimum * 0.8)):
                    return 0.8;

                case var expression when (distance_time <= (ideal_minimum * 0.8) && distance_time > (ideal_minimum * 0.7)):
                    return 0.7;

                case var expression when (distance_time <= (ideal_minimum * 0.7) && distance_time > (ideal_minimum * 0.6)):
                    return 0.6;

                case var expression when (distance_time <= (ideal_minimum * 0.6) && distance_time > (ideal_minimum * 0.5)):
                    return 0.5;

                case var expression when (distance_time <= (ideal_minimum * 0.5) && distance_time > (ideal_minimum * 0.4)):
                    return 0.4;

                case var expression when (distance_time <= (ideal_minimum * 0.4) && distance_time > (ideal_minimum * 0.3)):
                    return 0.3;

                case var expression when (distance_time <= (ideal_minimum * 0.3) && distance_time > (ideal_minimum * 0.2)):
                    return 0.2;

                case var expression when (distance_time <= (ideal_minimum * 0.2) && distance_time > (ideal_minimum * 0.1)):
                    return 0.1;

                default:
                    return 0.0;
            }
        }



        public double get_number_of_museums_score(int number)
        {
            switch (number)
            {
                case var expression when (number > 4):
                    return 1.0;

                case var expression when (number <= 4 && number > 3):
                    return 0.75;

                case var expression when (number <= 3 && number > 2):
                    return 0.5;

                case var expression when (number >= 1):
                    return 0.25;

                default:
                    return 0.0;
            }
        }



        public double get_max_weather_temp_score(int temp)
        {
            switch (temp)
            {
                case var expression when (temp >= 30):
                    return 1.0;

                case var expression when (temp == 29):
                    return 0.9;

                case var expression when (temp == 28 ):
                    return 0.8;

                case var expression when (temp <= 27 && temp > 25):
                    return 0.7;

                case var expression when (temp <= 25 && temp > 23):
                    return 0.6;

                case var expression when (temp <= 23 && temp > 21):
                    return 0.5;

                case var expression when (temp <= 21 && temp > 19):
                    return 0.4;

                case var expression when (temp <= 19 && temp > 17):
                    return 0.3;

                case var expression when (temp <= 17 && temp > 14):
                    return 0.2;

                case var expression when (temp >= 14):
                    return 0.1;

                default:
                    return 0.0;
            }
        }



        public double get_min_weather_temp_score(int temp)
        {
            switch (temp)
            {
                case var expression when (temp > 15):
                    return 1.0;

                case var expression when (temp <= 15 && temp > 13):
                    return 0.9;

                case var expression when (temp <= 13 && temp > 11):
                    return 0.8;

                case var expression when (temp <= 11 && temp > 9):
                    return 0.7;

                case var expression when (temp <= 9 && temp > 5):
                    return 0.6;

                case var expression when (temp <= 5 && temp > 2):
                    return 0.5;

                case var expression when (temp <= 2 && temp > -1):
                    return 0.4;

                case var expression when (temp <= -1 && temp > -3):
                    return 0.3;

                case var expression when (temp <= -3 && temp > -6):
                    return 0.2;

                case var expression when (temp >= -6):
                    return 0.1;

                default:
                    return 0.0;
            }
        }
    }
}
