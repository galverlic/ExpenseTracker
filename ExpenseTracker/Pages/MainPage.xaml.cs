using ExpenseTracker.Models;
using ExpenseTracker.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace ExpenseTracker.Pages
{
    public partial class MainPage : ContentPage
    {
        private ObservableCollection<Expense> _expenses;
        private DatabaseService _databaseService;
        private ObservableCollection<ExpenseCategory> _categories;

        public MainPage()
        {
            InitializeComponent();
            try
            {
                _databaseService = new DatabaseService(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "expenses.db3"));
            }
            catch (Exception ex)
            {
                // Handle the exception, for example, log it or display an alert.
                Debug.WriteLine($"Database initialization failed: {ex.Message}");
            }
            _categories = new ObservableCollection<ExpenseCategory>();
            categoryPicker.ItemsSource = _categories;
            LoadCategoriesAndDefaults();
            _expenses = new ObservableCollection<Expense>();
            LoadExpenses();
        }

        public MainPage(DatabaseService databaseService)
        {
            InitializeComponent();
            _databaseService = databaseService;
        }

        private async void LoadExpenses()
        {
            var expenses = await _databaseService.GetExpensesAsync();
            _expenses.Clear();
            foreach (var expense in expenses)
            {
                _expenses.Add(expense);
            }
        }

        private async void LoadCategoriesAndDefaults()
        {
            await App.DatabaseService.AddDefaultCategoriesAsync();
            LoadCategories();
        }
        private async void LoadCategories()
        {
            var categories = await App.DatabaseService.GetExpenseCategoriesAsync();
            _categories.Clear();
            foreach (var category in categories)
            {
                _categories.Add(category);
            }
            _categories.Add(new ExpenseCategory { Name = "Add your own" });
        }

        private void OnCategorySelected(object sender, EventArgs e)
        {
            var picker = (Picker)sender;
            var selectedCategory = (ExpenseCategory)picker.SelectedItem;

            if (selectedCategory?.Name == "Add your own")
            {
                // Prompt the user to add a new category
                PromptForNewCategory();
            }
        }

        private async void PromptForNewCategory()
        {
            string result = await DisplayPromptAsync("New Category", "Enter the name of the new category:");
            if (!string.IsNullOrWhiteSpace(result))
            {
                var newCategory = new ExpenseCategory { Name = result };
                await App.DatabaseService.SaveExpenseCategoryAsync(newCategory);
                _categories.Add(newCategory);
                categoryPicker.SelectedItem = newCategory;
            }
            else
            {
                // Reset the Picker selection if the user cancels or enters a blank category
                categoryPicker.SelectedItem = null;
            }
        }

        private async void OnAddExpenseClicked(object sender, EventArgs e)
        {
            if (decimal.TryParse(amountEntry.Text, out var amount))
            {
                var selectedCategory = (ExpenseCategory)categoryPicker.SelectedItem;
                // Ensure a category is selected
                if (selectedCategory == null || selectedCategory.Name == "Add your own")
                {
                    await DisplayAlert("Invalid Input", "Please select a valid category.", "OK");
                    return;
                }

                var expense = new Expense
                {
                    Description = descriptionEntry.Text,
                    Amount = amount,
                    Date = DateTime.Now,
                    CategoryId = selectedCategory.Id // Assuming your Expense model has a CategoryId property
                };

                await _databaseService.SaveExpenseAsync(expense);
                _expenses.Add(expense);

                descriptionEntry.Text = string.Empty;
                amountEntry.Text = string.Empty;
                categoryPicker.SelectedItem = null; // Reset the picker
            }
            else
            {
                // Handle invalid amount entry
                await DisplayAlert("Invalid Input", "Please enter a valid amount.", "OK");
            }
        }

        private async void OnViewExpenseChartClicked(object sender, EventArgs e)
        {
            var expensesPieChartPage = new ExpensesPieChartPage(_databaseService);
            await Navigation.PushAsync(expensesPieChartPage);
        }



        private void OnViewAllExpensesClicked(object sender, EventArgs e)
        {
            Shell.Current.GoToAsync("//AllExpenses");
        }




    }
}
