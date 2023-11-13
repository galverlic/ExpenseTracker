using ExpenseTracker.Data;
using ExpenseTracker.Models;
using ExpenseTracker.Service;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ExpenseTracker.Pages;

public partial class MainPage : ContentPage
{
    private readonly ObservableCollection<ExpenseCategory> _categories;
    private readonly DatabaseService _databaseService;
    private readonly ExpenseService _expenseService;
    // private readonly ObservableCollection<Expense> _expenses;
    private readonly IncomeService _incomeService;
    // private readonly ObservableCollection<Income> _incomes;

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

    public MainPage(ExpenseService expenseService)
    {
        InitializeComponent();
        _expenseService = expenseService;
    }

    private async void OnAddIncomeClicked(object sender, EventArgs e)
    {
        var addIncomePage = new AddIncomePage(_incomeService);
        await Navigation.PushAsync(addIncomePage);

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
}