using ExpenseTracker.Data;
using ExpenseTracker.Service;

namespace ExpenseTracker.Pages
{
    public partial class App : Application
    {
        public IServiceProvider ServiceProvider { get; private set; }

        public App()
        {
            InitializeComponent();
            var services = new ServiceCollection();
            ConfigureServices(services);
            ServiceProvider = services.BuildServiceProvider();

            // Set the main page
            MainPage = new SplashPage();
        }

        protected override async void OnStart()
        {
            // Perform any asynchronous initialization here
            var databaseService = ServiceProvider.GetService<DatabaseService>();
            if (databaseService != null)
            {
                await databaseService.InitializeAsync();
            }

            // After initialization, set the main page
            MainPage = new AppShell(ServiceProvider);
        }
        private async Task ConfigureServices(IServiceCollection services)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.db3");
            var databaseService = new DatabaseService(dbPath);


            // Register services
            services.AddSingleton(databaseService);
            services.AddSingleton(databaseService.GetConnection());
            services.AddSingleton<ExpenseService>();
            services.AddSingleton<IncomeService>();
        }
    }
}
