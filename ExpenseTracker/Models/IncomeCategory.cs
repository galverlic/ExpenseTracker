using SQLite;

namespace ExpenseTracker.Models
{
    public class IncomeCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
