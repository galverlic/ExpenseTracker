using ExpenseTracker.Models;
using SQLite;

namespace ExpenseTracker.Services
{
    public class DatabaseService
    {
        private readonly SQLiteAsyncConnection _database;

        public DatabaseService(string dbPath)
        {
            _database = new SQLiteAsyncConnection(dbPath);

        }

        public async Task InitializeAsync()
        {
            await CreateTablesAsync();
        }

        private async Task CreateTablesAsync()
        {
            await _database.CreateTableAsync<Expense>();
            await _database.CreateTableAsync<ExpenseCategory>();
        }
        public async Task<int> SaveExpenseAsync(Expense expense)
        {
            return await _database.InsertAsync(expense);
        }

        public async Task<int> SaveExpenseCategoryAsync(ExpenseCategory category)
        {
            return await _database.InsertAsync(category);
        }
        public async Task<List<Expense>> GetExpensesAsync()
        {
            return await _database.Table<Expense>().ToListAsync();
        }
        public async Task<Expense> GetExpenseAsync(int id)
        {
            return await _database.Table<Expense>().Where(x => x.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<ExpenseCategory>> GetExpenseCategoriesAsync()
        {
            return await _database.Table<ExpenseCategory>().ToListAsync();
        }

        public async Task<int> UpdateExpenseAsync(Expense expense)
        {
            return await _database.UpdateAsync(expense);
        }
        public async Task<int> DeleteExpenseAsync(Expense expense)
        {
            return await _database.DeleteAsync(expense);
        }
        public async Task AddDefaultCategoriesAsync()
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

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }
    }
}
