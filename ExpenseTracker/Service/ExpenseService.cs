using ExpenseTracker.Models;
using SQLite;

namespace ExpenseTracker.Services
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

        public async Task DeleteExpenseCategoryAsync(ExpenseCategory category)
        {
            await _database.DeleteAsync(category);
        }

        // Additional methods for updating and deleting expenses and categories
    }
}
