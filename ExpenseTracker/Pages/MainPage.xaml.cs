using ExpenseTracker.Service;
using System.Diagnostics;

namespace ExpenseTracker.Pages;

public partial class MainPage : ContentPage
{
    private readonly ExpenseService _expenseService;
    private readonly IncomeService _incomeService;

    // Constructor with service dependencies
    public MainPage()
    {
        InitializeComponent();

        // Manually resolve the services
        _expenseService = ((App)Application.Current).ServiceProvider.GetService<ExpenseService>();
        _incomeService = ((App)Application.Current).ServiceProvider.GetService<IncomeService>();

        // Additional initialization if needed
    }


    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        if (_incomeService != null)
        {
            var addIncomePage = new AddIncomePage(_incomeService);
            await Navigation.PushAsync(addIncomePage);
        }
        else
        {
            // Log the error or inform the user
            Debug.WriteLine("IncomeService is not initialized.");
            // Optionally display an alert to the user
            await DisplayAlert("Error", "Service is not available. Please try again later.", "OK");
        }



    }

    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        var addExpensesPage = new AddExpensePage(_expenseService);
        await Navigation.PushAsync(addExpensesPage);
    }

    private async void OnViewExpenseChartClicked(object sender, EventArgs e)
    {
        var expensesPieChartPage = new ExpensesPieChartPage(_expenseService);
        await Navigation.PushAsync(expensesPieChartPage);
    }

    private async void OnViewAllExpensesClicked(object sender, EventArgs e)
    {
        var allExpensesPage = new AllExpensesPage(_expenseService);
        await Navigation.PushAsync(allExpensesPage);
    }

    // Other methods and event handlers...
}
