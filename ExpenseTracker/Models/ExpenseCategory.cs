using SQLite;

namespace ExpenseTracker.Models
{
    public class ExpenseCategory
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
