using Android.App;
using Android.Content;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;

namespace UTLP
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);

            var Button = FindViewById<Button>(Resource.Id.TestButton1);
            Button.Click += (Sender, Args) =>
            {
                // Toast.MakeText(Application.Context, Resources.GetString(Resource.String.app_name),
                //     ToastLength.Long).Show();
                var ActivityStartIntent = new Intent(this, typeof(ManualTripAddActivity));
                StartActivity(ActivityStartIntent);
            };

            var Button2 = FindViewById<Button>(Resource.Id.TestButton2);
            Button2.Click += (Sender, Args) =>
            {
                var ActivityStartIntent = new Intent(this, typeof(FuelAddActivity));
                StartActivity(ActivityStartIntent);
            };

            var Button3 = FindViewById<Button>(Resource.Id.TestButton3);
            Button3.Click += (Sender, Args) =>
            {
                var ActivityStartIntent = new Intent(this, typeof(CarAddActivity));
                StartActivity(ActivityStartIntent);
            };
        }
    }
}
