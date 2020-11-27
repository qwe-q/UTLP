#nullable enable
using System;
using System.IO;
using System.Linq;
using Android.App;
using Android.OS;
using Android.Support.V7.App;
using Android.Widget;
using Newtonsoft.Json;

namespace UTLP
{
    [Activity(Label = "@string/FuelAddTitle", Theme = "@style/AppTheme")]
    public class FuelAddActivity : AppCompatActivity
    {
        private FuelStruct? LastTrip;
        private FuelStruct CurrentTrip = new FuelStruct();
        
        private FileInfo? GetLastFuelFileName()
        {
            var FuelPath = Path.Combine(
                System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), 
                Resources.GetString(Resource.String.FuelDirectory));
            var FuelDir = new DirectoryInfo(FuelPath);
            if(!FuelDir.Exists)
                FuelDir.Create();
            var AllFuels = FuelDir.GetFiles();
            //https://stackoverflow.com/questions/1179970/how-to-find-the-most-recent-file-in-a-directory-using-net-and-without-looping
            if (AllFuels.Length == 0)
                //In this instance, this is the first fuel add ever;
                //Null coalescence operators will handle the rest
                return null;
            
            return AllFuels.OrderByDescending(f => f.LastWriteTime).First();
        }
        
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_fuel_add);

            // Button Button = FindViewById<Button>(Resource.Id.TestButton);
            // Button.Click += (Sender, Args) =>
            // {
            //     Toast.MakeText(Application.Context, Resources.GetString(Resource.String.app_name),
            //         ToastLength.Long).Show();
            // };
            
            //Load the last trip.
            //This is necessary for things such as getting the number of miles driven.
            var FileInfo = GetLastFuelFileName();
            if (FileInfo == null)
                LastTrip = null;
            else
            {
                string Json = File.ReadAllText(FileInfo.FullName);
                LastTrip = JsonConvert.DeserializeObject<FuelStruct>(Json);
            }

            var GallonsFilled = FindViewById<EditText>(Resource.Id.FuelGallons);
            GallonsFilled.FocusChange += (Sender, Args) =>
            {
                //If the user is done editing (shifted to some other item)
                if (!Args.HasFocus)
                {
                    UpdateMPG();
                }
            };

            var Odometer = FindViewById<EditText>(Resource.Id.FuelOdometer);
            Odometer.FocusChange += (Sender, Args) =>
            {
                if (!Args.HasFocus)
                {
                    UpdateMilesDriven();
                    UpdateMPG();
                }
            };
        }

        private void UpdateMilesDriven()
        {
            var Odometer = FindViewById<EditText>(Resource.Id.FuelOdometer);
            CurrentTrip.MilesDriven = float.Parse(Odometer.Text) - (LastTrip?.Odometer ?? 0);

            var MilesDriven = FindViewById<TextView>(Resource.Id.FuelMilesDriven);
            MilesDriven.Text = Resources.GetString(Resource.String.FuelCalculatedMilesDriven) + '\n' +
                               CurrentTrip.MilesDriven;
        }

        private void UpdateMPG()
        {
            var GallonsFilledView = FindViewById<EditText>(Resource.Id.FuelGallons);
            
            //If miles driven isn't set, we can just move on and wait for
            //the user to input it; but we'll need to error out if it's not set
            //when the user tries to save it
            //Actually
            //TODO: Need to check on Trip Add and Fuel Add if user hasn't input a value yet
            
            //ReSharper says that floating-point comparison is better like this
            //I believe it, honestly
            //Point is, check if it hasn't been set already
            if (CurrentTrip.MilesDriven < 1 || GallonsFilledView.Text == "")
                return;
            var GallonsFilled = float.Parse(GallonsFilledView.Text);

            CurrentTrip.MilesPerGallon = CurrentTrip.MilesDriven / GallonsFilled;

            var MPG = FindViewById<TextView>(Resource.Id.FuelMPG);
            MPG.Text = Resources.GetString(Resource.String.FuelCalculatedMPG) + '\n' +
                CurrentTrip.MilesPerGallon;
        }
    }
}