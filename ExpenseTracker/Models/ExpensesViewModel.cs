using System.Collections.ObjectModel;

namespace ExpenseTracker.Models
{
    public class ExpensesViewModel
    {
        public ObservableCollection<ExpenseData> ExpenseSummary { get; set; }

        public ExpensesViewModel()
        {
            ExpenseSummary = new ObservableCollection<ExpenseData>();
            LoadExpenses();
        }

        private void LoadExpenses()
        {
            // Logic to fetch and aggregate expenses from the database
            // For each category, sum the Amount of Expenses and add to ExpenseSummary
        }
    }

}
