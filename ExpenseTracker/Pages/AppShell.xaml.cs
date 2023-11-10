namespace ExpenseTracker.Pages
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            GoToAsync("//MainPage");
        }

    }
}