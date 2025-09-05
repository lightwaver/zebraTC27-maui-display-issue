#if ANDROID
using Android.Content;
using Microsoft.Maui.ApplicationModel;
namespace MauiApp4;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
    }

    private void Login_Clicked(object sender, EventArgs e)
    {


var context = Android.App.Application.Context;
var intent = new Intent(context, typeof(MauiApp4.Platforms.Android.SecondActivity));
intent.SetFlags(ActivityFlags.NewTask);
context.StartActivity(intent);
    }
}

#endif