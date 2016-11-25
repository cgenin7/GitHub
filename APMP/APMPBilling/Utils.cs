using System;
using System.Collections.Generic;

namespace APMPBilling
{
    public class Utils
    {
        public static List<float> GetPossibleHours()
        { 
            return new List<float>() { 0, 0.5F, 1, 1.5F, 2, 2.5F, 3, 3.5F, 4, 4.5F, 5, 5.5F, 6, 6.5F, 7, 7.5F,
                8, 8.5F, 9, 9.5F, 10, 10.5F, 11, 11.5F, 12, 12.5F, 13, 13.5F, 14, 14.5F, 15, 15.5F, 16, 16.5F};
        }

        public static string GetExceptionMessage(Exception ex)
        {
            string msg = ex.Message;

            while (ex.InnerException != null)
            {
                msg += " - " + ex.InnerException.Message;
                ex = ex.InnerException;
            }

            return msg;
        }
    }
}