namespace ExpenseTracker.Pages
{

    public partial class AllExpensesPage : ContentPage
    {
        public AllExpensesPage()
        {
            InitializeComponent();
            LoadExpenses();

        }

        private async void LoadExpenses()
        {
            var expenses = await App.DatabaseService.GetExpensesAsync();
            expensesCollectionView.ItemsSource = expenses;
        }
    }
}

