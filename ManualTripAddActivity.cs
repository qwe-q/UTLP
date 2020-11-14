using System;
using System.IO;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace UTLP
{
    [Activity(Label = "@string/ManualAddTripTitle", Theme = "@style/AppTheme")]
    public class ManualTripAddActivity : AppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_manual_trip_add);

            // Button Button = FindViewById<Button>(Resource.Id.TestButton);
            // Button.Click += (Sender, Args) =>
            // {
            //     Toast.MakeText(Application.Context, Resources.GetString(Resource.String.app_name),
            //         ToastLength.Long).Show();
            // };
            Button Button = FindViewById<Button>(Resource.Id.TripSaveButton);
            Button.Click += (Sender, Args) =>
            {
                //Blah blah save stuff
                TripStruct Trip;
                Trip.Date = DateTime.Now;
                Trip.Destination = FindViewById<EditText>(Resource.Id.TripDestination).Text;
                Trip.MilesElapsed = float.Parse(FindViewById<EditText>(Resource.Id.TripMilesDriven).Text);
                Trip.MPG = float.Parse(FindViewById<EditText>(Resource.Id.TripMPG).Text);
                Trip.TimeElapsed = FindViewById<EditText>(Resource.Id.TripLength).Text;

                string Json = JsonConvert.SerializeObject(Trip);
                var OutPath = Path.Combine(
                    System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), 
                    Trip.Date.ToString("yyyy-M-d HH:mm:ss"));
                
                //If this causes trouble, switch it to async
                //Also, we naively assume that a user can't have possibly created two trips in the same second.
                using (var Writer = File.CreateText(OutPath)) 
                    Writer.Write(Json);

                Finish(); //We're done, go back
            };
        }
    }
}