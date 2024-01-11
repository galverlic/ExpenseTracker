using ExpenseTracker.Service;

namespace ExpenseTracker.Pages
{
    public partial class AppShell : Shell
    {
        public AppShell(IServiceProvider serviceProvider)
        {
            InitializeComponent();

            // Register routes for navigation
            RegisterRoutes();

            // Example of resolving a service, if needed
            var expenseService = serviceProvider.GetService<ExpenseService>();
            // You can now use expenseService as needed



        }

        protected override async void OnNavigatedTo(NavigatedToEventArgs args)
        {
            base.OnNavigatedTo(args);
            await GoToAsync("//MainPage");
        }


        private void RegisterRoutes()
        {
            Routing.RegisterRoute(nameof(AddExpensePage), typeof(AddExpensePage));
            Routing.RegisterRoute(nameof(AddIncomePage), typeof(AddIncomePage));
            Routing.RegisterRoute(nameof(AllExpensesPage), typeof(AllExpensesPage));
            Routing.RegisterRoute(nameof(EditExpensePopup), typeof(EditExpensePopup));
            Routing.RegisterRoute(nameof(ExpenseDetailPage), typeof(ExpenseDetailPage));
            Routing.RegisterRoute(nameof(ExpensesPieChartPage), typeof(ExpensesPieChartPage));


        }
    }
}
