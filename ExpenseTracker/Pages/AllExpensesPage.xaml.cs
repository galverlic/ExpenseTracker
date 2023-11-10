using ExpenseTracker.Services;

namespace ExpenseTracker.Pages
{

    public partial class AllExpensesPage : ContentPage
    {
        private readonly DatabaseService _databaseService;

        public AllExpensesPage()
        {
            InitializeComponent();
            LoadExpenses();

        }

        public AllExpensesPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
            LoadExpenses();
        }

        private async void LoadExpenses()
        {
            var expenses = await App.DatabaseService.GetExpensesAsync();
            expensesCollectionView.ItemsSource = expenses;
        }
    }
}

