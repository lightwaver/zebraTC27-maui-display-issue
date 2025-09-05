using Android.App;
using Android.OS;
using Android.Widget;
using Android.Content;

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

            //var buttonClose = FindViewById<Android.Widget.Button>(Resource.Id.buttonClose);
            //buttonClose.Click += (s, e) =>
            //{
            //    Finish();
            //};
        }
    }
}
