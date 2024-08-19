using Zhankui_Wang_Prob_Asst_3_Part_1.Models;

namespace Zhankui_Wang_Prob_Asst_3_Part_1.Utilities
{
    public static class Utility
    {
        public static DateTime FirstMondayOfSecondWeek(int year, int month)
        {
            // Get the first day of the month
            DateTime firstDayOfMonth = new DateTime(year, month, 1);

            // Calculate the first Monday of the month
            int daysToFirstMonday = ((int)DayOfWeek.Monday - (int)firstDayOfMonth.DayOfWeek + 7) % 7;
            DateTime firstMonday = firstDayOfMonth.AddDays(daysToFirstMonday);

            // The first Monday of the second week is 7 days after the first Monday
            DateTime firstMondayOfSecondWeek = firstMonday.AddDays(7);

            return firstMondayOfSecondWeek;
        }

        public static string PostalCode(string inputPostalCode)
        {

            return inputPostalCode.ToUpper().Replace(" ", "").Replace("-","").Trim();

        }

        public static string ProvinceOfCity(string cityName)
        {
            using (var context = new CollegeDbContext())
            {
                var provinceName = (from city in context.Cities
                                    join province in context.Provinces
                                    on city.ProvinceId equals province.Id
                                    where city.Name == cityName
                                    select province.Name).FirstOrDefault();

                return provinceName ?? "Province not found"; // Return a message if the city doesn't exist
            }
        }

    }
}
