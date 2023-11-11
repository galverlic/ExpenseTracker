using ExpenseTracker.Models;
using SQLite;

namespace ExpenseTracker.Service
{


    public class IncomeService
    {
        private readonly SQLiteAsyncConnection _database;
        public IncomeService(SQLiteAsyncConnection database)
        {
            _database = database;

        }

        public async Task<List<Income>> GetIncomeAsync()
        {
            return await _database.Table<Income>().ToListAsync();
        }

        public async Task<List<IncomeCategory>> GetIncomeCategoriesAsync()
        {
            return await _database.Table<IncomeCategory>().ToListAsync();
        }

        public async Task SaveIncomeAsync(Income income)
        {
            if (income.Id != 0)
            {
                await _database.UpdateAsync(income);
            }
            else
            {
                await _database.InsertAsync(income);
            }
        }

        public async Task SaveIncomeCategoryAsync(IncomeCategory category)
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

        public async Task DeleteIncomeAsync(Expense expense)
        {
            await _database.DeleteAsync(expense);
        }


        // METHOD TO UPDATE AN EXPENSE
        public async Task UpdateIncomeAsync(Income income)
        {
            await _database.UpdateAsync(income);
        }


    }
}
