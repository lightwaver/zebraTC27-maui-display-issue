#if ANDROID
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
#endif
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;
using Microsoft.Maui.Platform;
using System.Runtime.CompilerServices;
namespace MauiApp4;

public partial class MainPage : ContentPage
{

    public MainPage()
    {
        InitializeComponent();
        DeviceDisplay.MainDisplayInfoChanged += DeviceDisplay_MainDisplayInfoChanged; ;
    }

    private void DeviceDisplay_MainDisplayInfoChanged(object? sender, DisplayInfoChangedEventArgs e)
    {
        var xy = App.Current.MainPage;
    }

    private void Login_Clicked(object sender, EventArgs e)
    {

#if ANDROID
        var context = Android.App.Application.Context;
        var intent = new Intent(context, typeof(MauiApp4.Platforms.Android.SecondActivity));
        intent.SetFlags(ActivityFlags.NewTask);
        context.StartActivity(intent);
#endif
    }
    private IEnumerable<IView> GetAllVisualElements(IView root)
    {
        var elements = new List<IView> { root };
        if (root is Shell shell)
        {
            elements.AddRange(GetAllVisualElements(shell.CurrentPage));
        }
        // For ContentPage, get its Content
        else if (root is ContentPage contentPage && contentPage.Content is VisualElement content)
        {
            elements.AddRange(GetAllVisualElements(content));
        }
        // For Layouts, iterate their children
        else if (root is Layout layout)
        {
            foreach (var child in layout.Children)
            {
                elements.AddRange(GetAllVisualElements(child));
            }
        }

        return elements;
    }
    private async Task ShowVisualElementSizes(List<IView> visualElements)
    {
        var sb = new System.Text.StringBuilder();

        foreach (var element in visualElements)
        {
            if (element is VisualElement ve)
            {
                sb.AppendLine(
                    $"{ve.GetType().Name}: " +
                    $"DesiredSize=({ve.DesiredSize.Width:F2}, {ve.DesiredSize.Height:F2}), " +
                    $"ActualSize=({ve.Width:F2}, {ve.Height:F2})"
                );
            }
        }

        await DisplayAlert("VisualElement Sizes", sb.ToString(), "OK");
    }
    private async void Back_Clicked(object sender, EventArgs e)
    {
        try
        {
            var visualElements = GetAllVisualElements(App.Current.MainPage);
            await ShowVisualElementSizes(visualElements.ToList());
#if ANDROID
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
#endif
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", ex.Message, "OK");
        }
    }
    private void ClickMe_Clicked(object sender, EventArgs e)
    {
        //((IVisualElementController)this).InvalidateMeasure(Microsoft.Maui.Controls.Internals.InvalidationTrigger.MeasureChanged);

        //var visualElements = GetAllVisualElements(App.Current.MainPage);
        //foreach(var view in visualElements)
        //{
        //    if(view is Microsoft.Maui.Controls.ContentView v)
        //    {
        //        v.ForceLayout();

        //    }
        //}
#if ANDROID
        //https://github.com/xamarin/Xamarin.Forms/issues/13043
        var context = Android.App.Application.Context;
        var metricsDisplay = context.Resources.DisplayMetrics;

        var displayManager = (Android.Hardware.Display.DisplayManager)context.GetSystemService(Context.DisplayService);
        var display = displayManager.GetDisplays().FirstOrDefault(a => a.DisplayId != 0);

        if (display != null)
        {
            display.GetMetrics(metricsDisplay);
        }
        //var width = metricsDisplay.WidthPixels / metricsDisplay.Density;
        //var height = metricsDisplay.HeightPixels / metricsDisplay.Density;

        var width = context.FromPixels(metricsDisplay.WidthPixels);
        var height = context.FromPixels(metricsDisplay.HeightPixels);


        var handler = this.Handler as IPlatformViewHandler;
        var nativeView = handler.PlatformView;
        this.Arrange(new Rect(0, 0, width, height));
        handler?.UpdateValue(nameof(IView.Arrange));

#endif
    }

}
