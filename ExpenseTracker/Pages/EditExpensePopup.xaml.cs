using CommunityToolkit.Maui.Views;
using ExpenseTracker.Models;
using ExpenseTracker.Service; // Assuming ExpenseService is in this namespace
using System.Collections.ObjectModel;

namespace ExpenseTracker.Pages
{
    public partial class EditExpensePopup : Popup
    {
        private readonly Expense _expense;
        private readonly ExpenseService _expenseService;
        private readonly ObservableCollection<ExpenseCategory> _categories;


        public EditExpensePopup(Expense expense, ExpenseService expenseService)
        {
            InitializeComponent();

            if (expense == null)
                throw new ArgumentNullException(nameof(expense));

            if (expenseService == null)
                throw new ArgumentNullException(nameof(expenseService));

            _expense = expense;
            _expenseService = expenseService;
            _categories = new ObservableCollection<ExpenseCategory>();
            CategoryPicker.ItemsSource = _categories;
            BindingContext = _expense;
            LoadCategoriesAndDefaults();
        }

        private async void LoadCategoriesAndDefaults()
        {
            await _expenseService.AddDefaultExpenseCategoriesAsync();
            LoadCategories();
        }
        private async void LoadCategories()
        {
            var categories = await _expenseService.GetExpenseCategoriesAsync();
            _categories.Clear();
            foreach (var category in categories) _categories.Add(category);
            _categories.Add(new ExpenseCategory { Name = "Add your own" });
        }


        private async void OnSaveClicked(object sender, EventArgs e)
        {
            // Validate input
            if (!decimal.TryParse(AmountEntry.Text, out var amount))
            {
                // Handle invalid amount input (e.g., show a message or highlight the field)
                return;
            }

            var selectedCategory = (ExpenseCategory)CategoryPicker.SelectedItem;
            if (selectedCategory == null)
            {
                // Handle unselected category (e.g., show a message or highlight the field)
                return;
            }

            // Update the expense object with the new details
            _expense.Description = DescriptionEntry.Text;
            _expense.CategoryId = selectedCategory.Id;
            _expense.Amount = amount;

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
