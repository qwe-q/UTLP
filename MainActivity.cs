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

            var Button = FindViewById<Button>(Resource.Id.TestButton);
            Button.Click += (Sender, Args) =>
            {
                // Toast.MakeText(Application.Context, Resources.GetString(Resource.String.app_name),
                //     ToastLength.Long).Show();
                var ActivityStartIntent = new Intent(this, typeof(ManualTripAddActivity));
                StartActivity(ActivityStartIntent);
            };
        }
    }
}