using System;

namespace ExpenseTracker.Models
{
    public class ExpenseData
    {
        public string UniqueId { get; set; }
        public DateTime DateTime { get; set; }
        public string Category { get; set; }
        public string PaymentMode { get; set; }
        public string TransactionType { get; set; }
        public string Description { get; set; }
        public int Amount { get; set; }
        public string MonthShort { get; set; }
        public string MonthFull { get; set; }
        public string FormattedDate { get; set; }
    }
}
