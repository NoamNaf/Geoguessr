using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.View.Menu;
using Google.Android.Material.Behavior;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using static Android.Gms.Maps.GoogleMap;

namespace Geoguessr
{
    [Activity(Label = "Play")]
    public class PlayActivity : Activity, IOnMapReadyCallback, IOnMapClickListener
    {
        private string round;   
        private TextView roundview;
        private Button guessbtn;
        private Button hintbtn;
        private Button openmapbtn;
        private GameLogic gameLogic;
        private GoogleMap _googleMap;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.play);
            roundview = FindViewById<TextView>(Resource.Id.roundview1);
            guessbtn = FindViewById<Button>(Resource.Id.guessbtn);
            hintbtn = FindViewById<Button>(Resource.Id.hintbtn);
            openmapbtn = FindViewById<Button>(Resource.Id.openmapbtn);

            gameLogic = new GameLogic();
            this.round = "Round 1/5";

            roundview.Text = round;

            guessbtn.Click += guessbtn_Click;
            hintbtn.Click += Hintbtn_Click;
            openmapbtn.Click += OpenMapbtn_Click;

            var mapFrag = MapFragment.NewInstance();
            FragmentManager.BeginTransaction()
                                    .Add(Resource.Id.map, mapFrag, "map_fragment")
                                    .Commit();
            //var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);


            mapFrag.GetMapAsync(this);
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            _googleMap.SetOnMapClickListener(this);
        }
        public void OnMapClick(LatLng point)
        {
            MarkerOptions markerOptions = new MarkerOptions()
                .SetPosition(point)
                .SetTitle("Clicked Location")
                .SetSnippet("Latitude: " + point.Latitude + ", Longitude: " + point.Longitude);

            _googleMap.AddMarker(markerOptions);
            Toast.MakeText(this, "Clicked at: " + point.Latitude + ", " + point.Longitude, ToastLength.Short).Show();//זמני\
            
            AddMarker(point);
        }

        private void AddMarker(LatLng point)
        {
            if (_googleMap != null)
            {
                LatLng location = new LatLng(point.Latitude, point.Longitude);

                MarkerOptions markerOptions = new MarkerOptions()
                    .SetPosition(location)
                    .SetTitle("Marker Title")
                    .SetSnippet("Marker Snippet");

                _googleMap.AddMarker(markerOptions);
                CameraUpdate cameraUpdate = CameraUpdateFactory.NewLatLngZoom(location, 12);
                _googleMap.MoveCamera(cameraUpdate);
            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        private void Hintbtn_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Warning");
            builder.SetMessage("Are you sure you need a hint? Getting a hint will hurt this round's final score.");
            builder.SetCancelable(true);
            builder.SetPositiveButton("Give me a hint", HintAction);
            builder.SetNegativeButton("Nevermind", CancelAction);
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        private void HintAction(object sender, DialogClickEventArgs e)//פעולה שמקבלת את מדינת מיקום ה streetview, ורושמת את המדינה במקום הכפתור, ומכבה אותו.
        {
            hintbtn.Enabled = false;
            hintbtn.Text = "USA";
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Cancel clicked", ToastLength.Short).Show();
        }
        private void guessbtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RoundScoreActivity));
            string rnd = gameLogic.GetRoundNum().ToString();
            intent.PutExtra("round", rnd);
            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            gameLogic.NextRound();
            this.round = "Round " + gameLogic.GetRoundNum() + "/5";
            roundview.Text = round;
        }
        public void OpenMapbtn_Click(object sender, EventArgs e)//פעם אחת הכפתור פותח את המפה על ה streetview, פעם אחרת הוא סוגר את המפה וחוזר ל streetview.
        {
            
        }
        public void TurnOnGuessButton()//פעולה שבודקת האם אפשר להדליק את הכפתור guess, אם כן היא מדליקה את הכפתור.
        {
            
        }
    }
}