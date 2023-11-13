using CommunityToolkit.Maui.Views;
using ExpenseTracker.Models;
using ExpenseTracker.Service; // Assuming ExpenseService is in this namespace

namespace ExpenseTracker.Pages
{
    public partial class EditExpensePopup : Popup
    {
        private readonly Expense _expense;
        private readonly ExpenseService _expenseService;

        public EditExpensePopup(Expense expense, ExpenseService expenseService)
        {
            InitializeComponent();
            _expense = expense;
            _expenseService = expenseService;
            BindingContext = _expense;
            LoadCategories(); // Load categories into the picker
        }

        private async void LoadCategories()
        {
            var categories = await _expenseService.GetExpenseCategoriesAsync();
            CategoryPicker.ItemsSource = categories;
            CategoryPicker.SelectedItem = categories.FirstOrDefault(c => c.Id == _expense.CategoryId);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate input here (e.g., check if any field is empty)

            // Update the expense object with the new details
            // This might be redundant if using TwoWay data binding
            _expense.Description = DescriptionEntry.Text;
            _expense.CategoryId = ((ExpenseCategory)CategoryPicker.SelectedItem).Id;
            _expense.Amount = Convert.ToDecimal(AmountEntry.Text);

            // Call the method to update the expense
            await _expenseService.UpdateExpenseAsync(_expense);

            // Close the popup
            Close();
        }
        private void OnCancelClicked(object sender, EventArgs e)
        {
            Close();
        }

    }
}
