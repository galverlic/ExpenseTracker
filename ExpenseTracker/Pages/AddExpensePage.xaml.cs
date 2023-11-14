using ExpenseTracker.Models;
using ExpenseTracker.Service;
using System.Collections.ObjectModel;

namespace ExpenseTracker.Pages;

public partial class AddExpensePage : ContentPage
{
    private readonly ObservableCollection<ExpenseCategory> _categories;
    private readonly ExpenseService _expenseService;
    private readonly ObservableCollection<Expense> _expenses;
    public AddExpensePage(ExpenseService expenseService)
    {
        InitializeComponent();
        _expenseService = expenseService;
        _categories = new ObservableCollection<ExpenseCategory>();
        CategoryPicker.ItemsSource = _categories;
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
            await _expenseService.SaveExpenseCategoryAsync(newCategory);
            _categories.Add(newCategory);
            CategoryPicker.SelectedItem = newCategory;
        }
        else
        {
            // Reset the Picker selection if the user cancels or enters a blank category
            CategoryPicker.SelectedItem = null;
        }
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (decimal.TryParse(AmountEntry.Text, out var amount))
        {
            var selectedCategory = (ExpenseCategory)CategoryPicker.SelectedItem;
            if (selectedCategory == null || selectedCategory.Name == "Add your own")
            {
                await DisplayAlert("Invalid Input", "Please select a valid category.", "OK");
                return;
            }


            var expense = new Expense
            {
                Description = DescriptionEntry.Text,
                Amount = amount,
                Date = DateTime.Now, // Or use a DatePicker to allow user to select date
                CategoryId = selectedCategory.Id
            };

            await _expenseService.SaveExpenseAsync(expense);

            // Optionally clear the form or navigate away after saving
            DescriptionEntry.Text = string.Empty;
            AmountEntry.Text = string.Empty;
            CategoryPicker.SelectedItem = null; // Reset the picker

            // Navigate back to the main page or refresh the list if necessary
            await Navigation.PopAsync(); // Uncomment if you want to pop the current page
        }
        else
        {
            await DisplayAlert("Invalid Input", "Please enter a valid amount.", "OK");
        }
    }

    private async void OnCancelClicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();


    }

}