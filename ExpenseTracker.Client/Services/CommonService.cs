using System.Globalization;

namespace ExpenseTracker.Service
{
    public static class CommonService
    {
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
    }
}
