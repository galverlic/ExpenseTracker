using ExpenseTracker.Models;
using ExpenseTracker.Service;
using System.Collections.ObjectModel;

namespace ExpenseTracker.Pages;

public partial class AddIncomePage : ContentPage
{
    private readonly ObservableCollection<IncomeCategory> _categories;
    private readonly IncomeService _incomeService;
    public AddIncomePage(IncomeService incomeService)
    {
        InitializeComponent();
        _incomeService = incomeService;
        _categories = new ObservableCollection<IncomeCategory>();
        CategoryPicker.ItemsSource = _categories;
        LoadCategoriesAndDefaults();
    }

    private async void LoadCategoriesAndDefaults()
    {
        await _incomeService.AddDefaultIncomeCategoriesAsync();
        LoadCategories();
    }

    private async void LoadCategories()
    {
        var categories = await _incomeService.GetIncomeCategoriesAsync();
        _categories.Clear();
        foreach (var category in categories) _categories.Add(category);

    }

    private void OnCategorySelected(object sender, EventArgs e)
    {
        var picker = (Picker)sender;
        var selectedCategory = (ExpenseCategory)picker.SelectedItem;
    }

    private async void OnSaveClicked(object sender, EventArgs e)
    {
        if (decimal.TryParse(AmountEntry.Text, out var amount))
        {
            var selectedCategory = (IncomeCategory)CategoryPicker.SelectedItem;
            if (selectedCategory == null)
            {
                await DisplayAlert("Invalid Input", "Please select a valid category.", "OK");
                return;
            }

            var income = new Income
            {
                Description = DescriptionEntry.Text,
                Amount = amount,
                Date = DateTime.Now, // Or use a DatePicker to allow user to select date
                CategoryId = selectedCategory.Id
            };

            await _incomeService.SaveIncomeAsync(income);

            // Optionally clear the form or navigate away after saving
            DescriptionEntry.Text = string.Empty;
            AmountEntry.Text = string.Empty;
            CategoryPicker.SelectedItem = null; // Reset the picker

            // Navigate back to the main page or refresh the list if necessary
            // await Navigation.PopAsync(); // Uncomment if you want to pop the current page
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