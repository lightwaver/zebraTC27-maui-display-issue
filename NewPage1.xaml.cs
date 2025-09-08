
namespace MauiApp4;

public partial class NewPage1 : ContentPage
{
    public NewPage1()
    {
        InitializeComponent();
    }
    private async void ClickMe_Clicked(object sender, EventArgs e)
    {
        try
        {

        }
        catch (Exception ex)
        {
            var xx = ex.Message;
        }
    }
    private async void Back_Clicked(object sender, EventArgs e)
    {
        try
        {
            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private void Login_Clicked(object sender, EventArgs e)
    {

    }
}