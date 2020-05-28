using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace ExpenseTracker.Service
{
    public static class CommonService
    {
        public static void GetDate(DateTime date)
        {
        }

        public static string GetCurrencyVal(int val)
        {
            CultureInfo NewCulture = new CultureInfo("en-US");
            if (val < 0)
            {
                NewCulture.NumberFormat.CurrencyNegativePattern = 1;
            }
            string formattedValue = val.ToString("C0", NewCulture);
            return formattedValue;
        }

        public static string GetNumberVal(int val)
        {
            return val.ToString("0,0", new CultureInfo("en-US"));
        }

        public static T GetMethodValue<T>(object obj)
        {
            var returnValue = JsonConvert.SerializeObject(obj);
            var Settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                MissingMemberHandling = MissingMemberHandling.Ignore
            };
            return JsonConvert.DeserializeObject<T>(returnValue, Settings);
        }
    }
}
