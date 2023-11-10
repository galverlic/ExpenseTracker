using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Collections.ObjectModel;

namespace ExpenseTracker.Pages
{

    public partial class ExpensesPieChartPage : ContentPage
    {
        private readonly DatabaseService _databaseService;
        public ObservableCollection<ExpenseData> AggregatedExpenseData { get; set; }

        public ExpensesPieChartPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
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
            var categories = await _databaseService.GetExpenseCategoriesAsync();
            var categoryMap = categories.ToDictionary(cat => cat.Id, cat => cat.Name);

            var expenses = await _databaseService.GetExpensesAsync(); // Fetch expenses from database
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