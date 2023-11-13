using ExpenseTracker.Models;
using SQLite;
using System.Collections.ObjectModel;

namespace ExpenseTracker.Service
{
    public class ExpenseService
    {
        private readonly SQLiteAsyncConnection _database;

        public ExpenseService(SQLiteAsyncConnection database)
        {
            _database = database;
        }

        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _database.Table<Expense>().ToListAsync();
        }

        public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
        {
            return await _database.Table<ExpenseCategory>().ToListAsync();
        }


        public async Task AddDefaultExpenseCategoriesAsync()
        {
            var defaultCategories = new List<ExpenseCategory>
        {
            new ExpenseCategory { Name = "Health" },
            new ExpenseCategory { Name = "Bills" },
            new ExpenseCategory { Name = "Grocery" },
            new ExpenseCategory { Name = "Entertainment" },
            new ExpenseCategory { Name = "Eating Out" }
        };

            foreach (var category in defaultCategories)
            {
                // Check if the category already exists to avoid duplicates
                var existingCategory = await _database.Table<ExpenseCategory>()
                                                      .FirstOrDefaultAsync(c => c.Name == category.Name);
                if (existingCategory == null)
                {
                    await _database.InsertAsync(category);
                }
            }
        }

        public async Task<ObservableCollection<ExpenseCategory>> LoadExpenseCategoriesAsync()
        {
            var categories = new ObservableCollection<ExpenseCategory>(await GetExpenseCategoriesAsync());
            categories.Add(new ExpenseCategory { Name = "Add your own" });
            return categories;
        }


        public async Task SaveExpenseAsync(Expense expense)
        {
            if (expense.Id != 0)
            {
                await _database.UpdateAsync(expense);
            }
            else
            {
                await _database.InsertAsync(expense);
            }
        }

        public async Task SaveExpenseCategoryAsync(ExpenseCategory category)
        {
            if (category.Id != 0)
            {
                await _database.UpdateAsync(category);
            }
            else
            {
                await _database.InsertAsync(category);
            }
        }

        public async Task DeleteExpenseAsync(Expense expense)
        {
            await _database.DeleteAsync(expense);
        }

        // COMMENTED BECAUSE I DO NOT NEED TO DO THIS

        //public async Task DeleteExpenseCategoryAsync(ExpenseCategory category)
        //{
        //    await _database.DeleteAsync(category);
        //}



        // Additional methods for updating and deleting expenses and categories

        // METHOD TO UPDATE AN EXPENSE
        public async Task UpdateExpenseAsync(Expense expense)
        {
            await _database.UpdateAsync(expense);
        }
    }
}
