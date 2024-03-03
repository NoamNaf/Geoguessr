using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android.Views;
using Newtonsoft.Json;
using System.Drawing.Printing;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using static Android.Gms.Maps.GoogleMap;
using Android.Media;
using static Xamarin.Essentials.Platform;

namespace Geoguessr
{
    [Activity(Label = "RoundScoreActivity")]
    public class RoundScoreActivity : Activity, View.IOnClickListener, IOnMapReadyCallback
    {
        private TextView roundview;
        private TextView distance;
        private TextView points;
        private Button continuebtn;
        private string round;
        private bool flag = true;
        private GoogleMap _googleMap;
        private LatLng latLng;
        private LatLng googlePoint; 
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.roundScore);
            continuebtn = FindViewById<Button>(Resource.Id.continuebtn);
            distance = FindViewById<TextView>(Resource.Id.distanceview);
            roundview = FindViewById<TextView>(Resource.Id.roundview2);
            points = FindViewById<TextView>(Resource.Id.pointsview);

            string roundst = Intent.GetStringExtra("round");
            this.round = "Round " + roundst + "/5";
            roundview.Text = this.round;

            string getDis = Intent.GetStringExtra("distance");
            string diswrite = "Distance from point: " + getDis;
            distance.Text = diswrite;

            string getPoi = Intent.GetStringExtra("points");
            if(getPoi == "10000")
                diswrite = "Points: " + getPoi + " (Perfect Score!!!)";
            else
                diswrite = "Points: " + getPoi;
            points.Text = diswrite;

            string latitude = Intent.GetStringExtra("panoLat");
            string longitude = Intent.GetStringExtra("panoLon");
            latLng = new LatLng(double.Parse(latitude), double.Parse(longitude));

            latitude = Intent.GetStringExtra("mapLat");
            longitude = Intent.GetStringExtra("mapLon");
            googlePoint = new LatLng(double.Parse(latitude), double.Parse(longitude));

            int roundint = int.Parse(roundst);
            if(roundint == 5)
                flag = false;

            continuebtn.SetOnClickListener(this);

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
            var mapContainer = FindViewById<FrameLayout>(Resource.Id.map);

            MarkerOptions markerOptions = new MarkerOptions()
               .SetPosition(googlePoint)
               .SetTitle("Clicked Location")
               .SetSnippet("Latitude: " + googlePoint.Latitude + ", Longitude: " + googlePoint.Longitude)
               .Visible(false);

            _googleMap.AddMarker(markerOptions);

            AddMarker(googlePoint);

            MarkerOptions markerOptions1 = new MarkerOptions()
               .SetPosition(latLng)
               .SetTitle("Clicked Location")
               .SetSnippet("Latitude: " + googlePoint.Latitude + ", Longitude: " + googlePoint.Longitude)
               .Visible(false);

            _googleMap.AddMarker(markerOptions1);

            AddMarker(latLng);
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
            }
        }
        public void OnClick(Android.Views.View view)
        {
            if(flag)
            {
                Android.Content.Intent intent = new Android.Content.Intent();
                Finish();
            }
            else
            {
                string finalpoints = Intent.GetStringExtra("finalpoints");
                Android.Content.Intent intent = new Android.Content.Intent(this, typeof(FinalResultActivity));
                intent.PutExtra("finalpoints", finalpoints);
                StartActivity(intent);
            }
        }
    }
}