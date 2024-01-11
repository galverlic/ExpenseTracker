namespace ExpenseTracker.Pages;

public partial class RegistrationPage : ContentPage
{

    public RegistrationPage()
    {
        InitializeComponent();
    }

    private async void ClickedOnLoginButton(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("//LoginPage");
    }
}