using WebPageAge.Models;

namespace WebPageAge
{
    public class AgeCalculator
    {
        int birthDate, currentDate,birthMonth,currentMonth,birthYear,currentYear;
       public void age(DateTime dob)
        {
            DateTime cur= DateTime.Now;
            int[] month = { 31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31 };
            
            birthDate = dob.Day;
            currentDate = cur.Day;
            birthMonth = dob.Month;
            currentMonth = cur.Month;
            birthYear = dob.Year;
            currentYear = cur.Year;
            if (birthDate >currentDate)
            {
                 currentDate+=month[birthMonth - 1];
                 currentMonth--;
            }
            if (birthMonth > currentMonth)
            {
                currentYear--;
                currentMonth += 12;
            }
           
            

        }

        public int getYears()
        {
            return currentYear-birthYear;
        }

        public int getMonths()
        {
            return currentMonth-birthMonth;
        }

        public int getDays()
        {
            return currentDate-birthMonth;
        }
    }
}
