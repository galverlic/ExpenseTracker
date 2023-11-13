using ExpenseTracker.Data;
using ExpenseTracker.Service;

namespace ExpenseTracker.Pages
{
    public partial class App : Application
    {
        private static DatabaseService _databaseService;
        private static ExpenseService _expenseService;
        private static IncomeService _incomeService;
        public static DatabaseService DatabaseService => _databaseService;
        public static ExpenseService ExpenseService => _expenseService;
        public static IncomeService IncomeService => _incomeService;

        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            InitializeServicesAsync();
        }

        private async Task InitializeServicesAsync()
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.db3");
            _databaseService = new DatabaseService(dbPath);
            await _databaseService.InitializeAsync(); // This should be an async method that wraps CreateTablesAsync
            _expenseService = new ExpenseService(_databaseService.GetConnection());
            _incomeService = new IncomeService(_databaseService.GetConnection());
        }
    }

}