using System;

namespace APMPBilling
{
    public class FormatterHelper
    {
        public static string FormatPeriod(DateTime startOfPeriod, DateTime endOfPeriod)
        {
            string startDay = (startOfPeriod.Day == 1 ? "1er" : startOfPeriod.Day.ToString());
            string endDay = (endOfPeriod.Day == 1 ? "1er" : endOfPeriod.Day.ToString());

            if (startOfPeriod.Date == endOfPeriod.Date)
                return "Le " + startDay + " " + startOfPeriod.ToString("MMMM") + " " +
                startOfPeriod.Year;

            if (startOfPeriod.Year == endOfPeriod.Year)
            {
                if (startOfPeriod.Month == endOfPeriod.Month)
                    return "Du " + startDay + " au " + endDay + " " + endOfPeriod.ToString("MMMM") + " " + endOfPeriod.Year;
                return "Du " + startDay + " " + startOfPeriod.ToString("MMMM") + " au " + endDay + " " + endOfPeriod.ToString("MMMM") + " " + endOfPeriod.Year;
            }
            return "Du " + startDay + " " + startOfPeriod.ToString("MMMM") + " " + 
                startOfPeriod.Year + " au " + endDay + " " + endOfPeriod.ToString("MMMM") + " " + 
                endOfPeriod.Year;
        }

        public static string FormatMonth(DateTime date)
        {
            switch (date.Month)
            {
                case 1:
                    return "Janv";
                case 2:
                    return "Févr";
                case 3:
                    return "Mars";
                case 4:
                    return "Avril";
                case 5:
                    return "Mai";
                case 6:
                    return "Juin";
                case 7:
                    return "Juil";
                case 8:
                    return "Août";
                case 9:
                    return "Sept";
                case 10:
                    return "Oct";
                case 11:
                    return "Nov";
                case 12:
                    return "Déc";
            }
            return "???";
        }
    }
}