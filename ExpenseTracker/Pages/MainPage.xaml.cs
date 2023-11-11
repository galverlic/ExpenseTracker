using ExpenseTracker.Data;
using ExpenseTracker.Models;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ExpenseTracker.Pages;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<ExpenseCategory> _categories;
    private readonly DatabaseService _databaseService;
    private readonly ObservableCollection<Expense> _expenses;

    public MainPage()
    {
        InitializeComponent();
        try
        {
            _databaseService =
                new DatabaseService(Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.db3"));
        }
        catch (Exception ex)
        {
            // Handle the exception, for example, log it or display an alert.
            Debug.WriteLine($"Database initialization failed: {ex.Message}");
        }
    }

    public MainPage(DatabaseService databaseService)
    {
        InitializeComponent();
        _databaseService = databaseService;
    }



    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        /*



         */
    }
    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        var addExpensesPage = new AddExpensePage(_databaseService);
        await Navigation.PushAsync(addExpensesPage);
    }


    private async void OnViewExpenseChartClicked(object sender, EventArgs e)
    {
        var expensesPieChartPage = new ExpensesPieChartPage(_databaseService);
        await Navigation.PushAsync(expensesPieChartPage);
    }


    private async void OnViewAllExpensesClicked(object sender, EventArgs e)
    {
        var allExpensesPage = new AllExpensesPage(_databaseService);
        await Navigation.PushAsync(allExpensesPage);
    }
}