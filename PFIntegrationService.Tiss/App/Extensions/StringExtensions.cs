using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PFIntegrationService.Tiss.App.Extensions
{
    public static class StringExtensions
    {
        public static int ToInt(this string current)
        {
            int convertedValue;

            int.TryParse(current, out convertedValue);

            return convertedValue;
        }

        public static int ToInt(this string number, int defaultInt)
        {
            int resultNum = defaultInt;
            try
            {
                if (!string.IsNullOrEmpty(number))
                    resultNum = Convert.ToInt32(number);
            }
            catch
            {
            }
            return resultNum;
        }
    }
}
