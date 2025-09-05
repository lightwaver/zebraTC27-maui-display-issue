#if ANDROID
using Android.Content;
using Android.Runtime;
using Android.Views;
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

    private async void Back_Clicked(object sender, EventArgs e)
    {
        try
        {
            var context = Android.App.Application.Context;
            var metrics = context.Resources.DisplayMetrics;
            var widthPixels = metrics.WidthPixels;
            var heightPixels = metrics.HeightPixels;
            var density = metrics.Density;
            var dpi = metrics.DensityDpi;

            var windowManager = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var rotation = windowManager.DefaultDisplay.Rotation;
            var orientation = context.Resources.Configuration.Orientation;

            string info = $"Resolution: {widthPixels}x{heightPixels}\n" +
                          $"Density: {density}\n" +
                          $"DPI: {dpi}\n" +
                          $"Rotation: {rotation}\n" +
                          $"Orientation: {orientation}";

            await DisplayAlert("Display Info", info, "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
}

#endif