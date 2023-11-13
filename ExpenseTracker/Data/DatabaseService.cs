using ExpenseTracker.Models;
using SQLite;

namespace ExpenseTracker.Data
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
            await _database.CreateTableAsync<Income>();
            await _database.CreateTableAsync<IncomeCategory>();
        }

        public SQLiteAsyncConnection GetConnection()
        {
            return _database;
        }
    }
}
