using ExpenseTracker.Models;
using ExpenseTracker.Service;
using System.Collections.ObjectModel;

namespace ExpenseTracker.Pages
{

    public partial class ExpensesPieChartPage : ContentPage
    {
        private readonly ExpenseService _expenseService;
        public ObservableCollection<ExpenseData> AggregatedExpenseData { get; set; }

        public ExpensesPieChartPage(ExpenseService expenseService)
        {
            InitializeComponent();
            _expenseService = expenseService;
            InitializePage();
        }

        // Constructor with DatabaseService for manual instantiation


        private void InitializePage()
        {
            AggregatedExpenseData = new ObservableCollection<ExpenseData>();
            LoadAggregatedExpenses();
        }

        private async void LoadAggregatedExpenses()
        {
            var categories = await _expenseService.GetExpenseCategoriesAsync();
            var categoryMap = categories.ToDictionary(cat => cat.Id, cat => cat.Name);

            var expenses = await _expenseService.GetExpensesAsync(); // Fetch expenses from database
            var aggregatedData = expenses
                .GroupBy(e => e.CategoryId)
                .Where(group => categoryMap.ContainsKey(group.Key))
                .Select(group => new ExpenseData
                {
                    CategoryName = categoryMap[group.Key],
                    TotalAmount = group.Sum(e => e.Amount)
                });


            AggregatedExpenseData.Clear();
            foreach (var data in aggregatedData)
            {
                AggregatedExpenseData.Add(data);
            }

            this.BindingContext = this;
        }

    }
}