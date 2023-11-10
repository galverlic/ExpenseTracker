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

        _categories = new ObservableCollection<ExpenseCategory>();
        CategoryPicker.ItemsSource = _categories;
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
        foreach (var expense in expenses) _expenses.Add(expense);
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
        foreach (var category in categories) _categories.Add(category);
        _categories.Add(new ExpenseCategory { Name = "Add your own" });
    }

    private void OnCategorySelected(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedCategory = (ExpenseCategory)picker.SelectedItem;

        if (selectedCategory?.Name == "Add your own")
            // Prompt the user to add a new category
            PromptForNewCategory();
    }

    private async void PromptForNewCategory()
    {
        var result = await DisplayPromptAsync("New Category", "Enter the name of the new category:");
        if (!string.IsNullOrWhiteSpace(result))
        {
            var newCategory = new ExpenseCategory { Name = result };
            await App.DatabaseService.SaveExpenseCategoryAsync(newCategory);
            _categories.Add(newCategory);
            CategoryPicker.SelectedItem = newCategory;
        }
        else
        {
            // Reset the Picker selection if the user cancels or enters a blank category
            CategoryPicker.SelectedItem = null;
        }
    }

    private async void OnAddExpenseClicked(object sender, EventArgs e)
    {
        if (decimal.TryParse(AmountEntry.Text, out var amount))
        {
            var selectedCategory = (ExpenseCategory)CategoryPicker.SelectedItem;
            // Ensure a category is selected
            if (selectedCategory == null || selectedCategory.Name == "Add your own")
            {
                await DisplayAlert("Invalid Input", "Please select a valid category.", "OK");
                return;
            }

            var expense = new Expense
            {
                Description = DescriptionEntry.Text,
                Amount = amount,
                Date = DateTime.Now,
                CategoryId = selectedCategory.Id // Assuming your Expense model has a CategoryId property
            };

            await _databaseService.SaveExpenseAsync(expense);
            _expenses.Add(expense);

            DescriptionEntry.Text = string.Empty;
            AmountEntry.Text = string.Empty;
            CategoryPicker.SelectedItem = null; // Reset the picker
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


    private async void OnViewAllExpensesClicked(object sender, EventArgs e)
    {
        var allExpensesPage = new AllExpensesPage(_databaseService);
        await Navigation.PushAsync(allExpensesPage);
    }
}