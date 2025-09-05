using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.Widget;
using Microsoft.Maui.Controls.PlatformConfiguration.AndroidSpecific;

namespace MauiApp4.Platforms.Android
{
    //[Activity(Label = "SecondActivity")]
    [Activity(Label = "", Theme = "@android:style/Theme.NoTitleBar")]
    public class SecondActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.activity_second);

            var buttonBack = FindViewById< global::Android.Widget.Button>(Resource.Id.buttonBack);
            var buttonLogin = FindViewById<global::Android.Widget.Button>(Resource.Id.buttonLogin);

            if (buttonLogin != null)
                buttonLogin.Click += ButtonLogin_Click;
            if (buttonBack != null)
                buttonBack.Click += ButtonBack_Click;

        }

        private void ButtonLogin_Click(object sender, EventArgs e)
        {
            var context = this.ApplicationContext;
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

            // Show info in an AlertDialog
            var dialog = new AlertDialog.Builder(this)
                .SetTitle("Display Info")
                .SetMessage(info)
                .SetPositiveButton("OK", (s, e) => { })
                .Create();
            dialog.Show();
        }

        private void ButtonBack_Click(object sender, EventArgs e)
        {
            Toast.MakeText(this, "Back clicked", ToastLength.Short).Show();
            Finish(); // Closes the activity
        }
    }
}
