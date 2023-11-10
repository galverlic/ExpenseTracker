using CommunityToolkit.Maui.Views;
using ExpenseTracker.Data;
using ExpenseTracker.Models;

namespace ExpenseTracker.Pages;

public partial class ExpenseDetailPage : ContentPage
{
    private readonly Expense _expense;
    private readonly DatabaseService _databaseService;
    public ExpenseDetailPage(Expense expense, DatabaseService databaseService)
    {
        InitializeComponent();
        _expense = expense;
        _databaseService = databaseService;
        BindingContext = _expense; // Set the binding context to the expense.
    }


    private async void OnEditClicked(object sender, EventArgs e)
    {
        var expenseToEdit = (Expense)((Button)sender).BindingContext;
        var editExpensePopup = new EditExpensePopup(expenseToEdit, _databaseService);

        // This is the correct way to display a Popup
        await this.ShowPopupAsync(editExpensePopup);
    }


    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Delete", "Are you sure you want to delete this expense?", "Yes", "No");
        if (isConfirmed)
        {
            await _databaseService.DeleteExpenseAsync(_expense);
            await Navigation.PopAsync();
        }
    }

}