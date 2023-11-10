using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Pages;

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
        ExpensesCollectionView.ItemsSource = expenses;
    }

    private async void OnExpenseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is Expense selectedExpense)
        {
            // Assuming you have a reference to DatabaseService as _databaseService
            await Navigation.PushAsync(new ExpenseDetailPage(selectedExpense, _databaseService));
        }
        ((CollectionView)sender).SelectedItem = null;
    }

}