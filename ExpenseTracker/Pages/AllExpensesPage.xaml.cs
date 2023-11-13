using ExpenseTracker.Models;
using ExpenseTracker.Service;

namespace ExpenseTracker.Pages;

public partial class AllExpensesPage : ContentPage
{
    private readonly ExpenseService _expenseService;

    public AllExpensesPage()
    {
        InitializeComponent();
        LoadExpenses();
    }

    public AllExpensesPage(ExpenseService expenseService)
    {
        InitializeComponent();
        _expenseService = expenseService;
        LoadExpenses();
    }

    private async void LoadExpenses()
    {
        var expenses = await App.ExpenseService.GetExpensesAsync();
        ExpensesCollectionView.ItemsSource = expenses;
    }

    private async void OnExpenseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Expense selectedExpense)
        {
            // Assuming you have a reference to DatabaseService as _databaseService
            await Navigation.PushAsync(new ExpenseDetailPage(selectedExpense, _expenseService));
        }
        ((CollectionView)sender).SelectedItem = null;
    }

}