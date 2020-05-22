using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Models;

namespace ExpenseTracker.Service
{
    public class DashboardService
    {
        public string Name { get; set; }
        public ExpenseData CurDateTime { get; set; }
        public List<ExpenseData> ColIncomeDS { get; set; }
        public List<ExpenseData> ColExpenseDS { get; set; }
        private List<TempExpenseData> TempIncomeDS { get; set; }
        public List<TempExpenseData> TempExpenseDS { get; set; }
        public List<ExpenseData> LineDS { get; set; }

        public DashboardService() {
            CurDateTime = new ExpenseData();
            ColIncomeDS = new List<ExpenseData>();
            ColExpenseDS = new List<ExpenseData>();
            TempIncomeDS = new List<TempExpenseData>();
            TempExpenseDS = new List<TempExpenseData>();
            LineDS = new List<ExpenseData>();
        }

        public string getName()
        {
            return this.Name;
        }

        public void SetColumnChartIncomeDS(List<ExpenseData> incomeData)
        {
            for (int i = 0; i < incomeData.Count(); i++)
            {
                var data = Helper.Clone<ExpenseData>(incomeData[i]);
                int index = TempIncomeDS.FindIndex(s => s.Month == data.DateTime.Month);
                if (index >= 0)
                {
                    this.CurDateTime = this.TempIncomeDS[index].ExpenseData;
                    this.TempIncomeDS[index].ExpenseData.Amount = this.CurDateTime.Amount + data.Amount;
                }
                else
                {
                    TempIncomeDS.Add(new TempExpenseData { Month = data.DateTime.Month, ExpenseData = data });
                    index = TempIncomeDS.Count() - 1;
                    TempIncomeDS[index].ExpenseData.DateTime = new DateTime(this.TempIncomeDS[index].ExpenseData.DateTime.Year, this.TempIncomeDS[index].ExpenseData.DateTime.Month, 1, 0, 0, 0, 0);
                }
            }
        }
        public List<ExpenseData> GetColumnChartIncomeDS()
        {
            foreach (var data in this.TempIncomeDS)
            {
                this.ColIncomeDS.Add(data.ExpenseData);
            }

            return this.ColIncomeDS;
        }
        public List<ExpenseData> GetColumnChartExpenseDS(List<ExpenseData> expenseData)
        {
            for (int i = 0; i < expenseData.Count(); i++)
            {
                var data = Helper.Clone<ExpenseData>(expenseData[i]);
                int index = TempExpenseDS.FindIndex(s => s.Month == data.DateTime.Month);
                if (index >= 0)
                {
                    this.CurDateTime = TempExpenseDS[index].ExpenseData;
                    this.TempExpenseDS[index].ExpenseData.Amount = this.CurDateTime.Amount + data.Amount;
                }
                else
                {
                    TempExpenseDS.Add(new TempExpenseData { Month = data.DateTime.Month, ExpenseData = data });
                    index = TempExpenseDS.Count() - 1;
                    TempExpenseDS[index].ExpenseData.DateTime = new DateTime(this.TempExpenseDS[index].ExpenseData.DateTime.Year, this.TempExpenseDS[index].ExpenseData.DateTime.Month, 1, 0, 0, 0, 0);
                }
            }

            foreach (var data in this.TempExpenseDS)
            {
                this.ColExpenseDS.Add(data.ExpenseData);
            }

            return this.ColExpenseDS;
        }

        public List<ExpenseData> GetLineChartDS()
        {

            List<TempExpenseData> TempLineDS = new List<TempExpenseData>();
            foreach (var data in this.TempIncomeDS)
            {
                TempLineDS.Add(Helper.Clone<TempExpenseData>(data));
            }
            TempLineDS.AddRange(this.TempExpenseDS);
            List<TempExpenseData> LineD = new List<TempExpenseData>();
            for (int i = 0; i < TempLineDS.Count(); i++)
            {
                var data = TempLineDS[i].ExpenseData;
                int index = LineD.FindIndex(s => s.Month == data.DateTime.Month);
                if (index >= 0)
                {
                    this.CurDateTime = LineD[index].ExpenseData;
                    LineD[index].ExpenseData.Amount = Math.Abs(this.CurDateTime.Amount - data.Amount);
                }
                else
                {
                    LineD.Add(new TempExpenseData { Month = data.DateTime.Month, ExpenseData = data });
                }
            }

            foreach (var data in LineD)
            {
                this.LineDS.Add(data.ExpenseData);
            }
            return this.LineDS;
        }
    }

    public static class Helper
    {
        // Return a shallow clone of a list.
        public static List<T> ShallowClone<T>(this List<T> items)
        {
            return new List<T>(items);
        }

        public static T Clone<T>(T source)
        {
            var serialized = JsonConvert.SerializeObject(source);
            return JsonConvert.DeserializeObject<T>(serialized);
        }
    }

    public class TempExpenseData
    {
        public int Month { get; set; }
        public ExpenseData ExpenseData { get; set; }
    }
}
