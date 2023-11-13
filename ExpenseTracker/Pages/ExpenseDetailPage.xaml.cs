using CommunityToolkit.Maui.Views;
using ExpenseTracker.Models;
using ExpenseTracker.Service;

namespace ExpenseTracker.Pages;

public partial class ExpenseDetailPage : ContentPage
{
    private readonly Expense _expense;
    private readonly ExpenseService _expenseService;
    public ExpenseDetailPage(Expense expense, ExpenseService expenseService)
    {
        InitializeComponent();
        _expense = expense;
        _expenseService = expenseService;
        BindingContext = _expense; // Set the binding context to the expense.
    }


    private async void OnEditClicked(object sender, EventArgs e)
    {
        var expenseToEdit = (Expense)((Button)sender).BindingContext;
        var editExpensePopup = new EditExpensePopup(expenseToEdit, _expenseService);

        // This is the correct way to display a Popup
        await this.ShowPopupAsync(editExpensePopup);
    }


    private async void OnDeleteClicked(object sender, EventArgs e)
    {
        bool isConfirmed = await DisplayAlert("Delete", "Are you sure you want to delete this expense?", "Yes", "No");
        if (isConfirmed)
        {
            await _expenseService.DeleteExpenseAsync(_expense);
            await Navigation.PopAsync();
        }
    }

}