using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpenseTracker.Pages.Expense;

namespace ExpenseTracker.Service
{
    public class AppState
    {
        public ExpenseDataService ExpenseDataService { get; set; }

        public string CurrentBalance { get; set; }
        public FilterMenu FilterComponent { get; private set; }

        public event Action OnChange;

        public AppState()
        {
            ExpenseDataService = new ExpenseDataService();
        }

        public void SetCommonData(ExpenseDataService data)
        {
            this.ExpenseDataService = data;
            NotifyStateChanged();
        }

        public void UpdateCurrentBalance(string balance)
        {
            this.CurrentBalance = balance;
            NotifyStateChanged();
        }

        public void SetFilterComponent(FilterMenu component)
        {
            this.FilterComponent = component;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
