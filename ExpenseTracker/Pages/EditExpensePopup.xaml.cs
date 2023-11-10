using CommunityToolkit.Maui.Views;
using ExpenseTracker.Models;
using ExpenseTracker.Services;

namespace ExpenseTracker.Pages
{
    public partial class EditExpensePopup : Popup
    {

        private readonly Expense _expense;
        private readonly DatabaseService _databaseService;

        public EditExpensePopup(Expense expense, DatabaseService databaseService)
        {
            InitializeComponent();
            _expense = expense;
            _databaseService = databaseService;
            BindingContext = _expense;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var selectedCategory = (ExpenseCategory)CategoryPicker.SelectedItem;

            // Update the expense object with the new details
            _expense.Description = DescriptionEntry.Text;
            _expense.CategoryId = selectedCategory.Id;
            _expense.Amount = Convert.ToDecimal(AmountEntry.Text);
            _expense.Date = DateEntry.Date;

            // Call the method to update the expense
            await _databaseService.UpdateExpenseAsync(_expense);

            // Close the current page
            Close();
        }

    }
}
