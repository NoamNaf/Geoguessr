using Android.App;
using Android.Content;
using Android.Gms.Maps;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using static Android.Gms.Maps.GoogleMap;
using static Android.Gms.Maps.StreetViewPanorama;

namespace Geoguessr
{
    [Activity(Label = "Play")]
    public class PlayActivity : Activity, IOnMapReadyCallback, IOnMapClickListener, IOnStreetViewPanoramaReadyCallback, IOnStreetViewPanoramaChangeListener
    {
        private double latitude;
        private double longitude;
        private double distance;
        private string round;
        private bool firstlocation = true;
        private bool isLatLngOk = false;
        private TextView roundview;
        private Button guessbtn;
        private Button hintbtn;
        private Button openmapbtn;
        private GameLogic gameLogic;
        private GoogleMap _googleMap;
        private Marker currentMarker;
        private StreetViewPanoramaView streetViewPanoramaView;
        private StreetViewPanorama streetPanorama;
        private LatLng latlng;
        private LatLng googlePoint;
        private Player player;
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

            string serializedObj = Intent.GetStringExtra("user");
            player = JsonConvert.DeserializeObject<Player>(serializedObj);

            roundview.Text = round;

            guessbtn.Enabled = false;
            guessbtn.Click += guessbtn_Click;
            hintbtn.Click += Hintbtn_Click;
            openmapbtn.Click += OpenMapbtn_Click;

            streetViewPanoramaView = FindViewById<StreetViewPanoramaView>(Resource.Id.panorama);
            streetViewPanoramaView.OnCreate(savedInstanceState);

            streetViewPanoramaView.Visibility = ViewStates.Gone;

            while (!isLatLngOk)
            {
                latitude = GetRandomCoordinate(-60, 70);
                longitude = GetRandomCoordinate(-130, 150);
                if ((latitude > -60 && latitude < -10 && longitude > 15 && longitude < 63) || (latitude > -60 && latitude < -8 && longitude > 60 && longitude < 95) || (latitude > -60 && latitude < 0 && longitude > -30 && longitude < -15))
                    isLatLngOk = false;
                else
                    isLatLngOk = true;
            }
            latlng = new LatLng(GetRandomCoordinate(-60, 70), GetRandomCoordinate(-130, 150));
            streetViewPanoramaView.Visibility = ViewStates.Visible;
            streetViewPanoramaView.GetStreetViewPanoramaAsync(this);

            var mapFrag = MapFragment.NewInstance();
            FragmentManager.BeginTransaction()
                                    .Add(Resource.Id.map, mapFrag, "map_fragment")
                                    .Commit();
            //var mapFragment = (MapFragment)FragmentManager.FindFragmentById(Resource.Id.map);

            mapFrag.GetMapAsync(this);
        }
        private double GetRandomCoordinate(double min, double max)
        {
            Random random = new Random();
            return random.NextDouble() * (max - min) + min;
        }
        public void OnStreetViewPanoramaReady(StreetViewPanorama panorama)
        {
            this.streetPanorama = panorama;
            streetPanorama.SetOnStreetViewPanoramaChangeListener(this);
            streetPanorama.StreetNamesEnabled = false;
            streetPanorama.PanningGesturesEnabled = true;
            streetPanorama.ZoomGesturesEnabled = true;

            streetPanorama.SetPosition(latlng, 1500000);
        }
        public void OnStreetViewPanoramaChange(StreetViewPanoramaLocation location)
        {
            if (location != null && firstlocation)
            {
                latlng = location.Position;
                firstlocation = false;
                streetViewPanoramaView.Visibility = ViewStates.Visible;
            }
        }
        public void OnMapReady(GoogleMap googleMap)
        {
            _googleMap = googleMap;
            var mapContainer = FindViewById<FrameLayout>(Resource.Id.map);
            mapContainer.Visibility = ViewStates.Gone;
            _googleMap.SetOnMapClickListener(this);
        }
        public void OnMapClick(LatLng point)
        {
            MarkerOptions markerOptions = new MarkerOptions()
                .SetPosition(point)
                .SetTitle("Clicked Location")
                .SetSnippet("Latitude: " + point.Latitude + ", Longitude: " + point.Longitude)
                .Visible(false);

            googlePoint = point;

            _googleMap.AddMarker(markerOptions);
            
            AddMarker(point);
        }

        private void AddMarker(LatLng point)
        {
            if (_googleMap != null)
            {
                RemoveMarker();
                guessbtn.Enabled = true;
                
                LatLng location = new LatLng(point.Latitude, point.Longitude);

                MarkerOptions markerOptions = new MarkerOptions()
                    .SetPosition(location)
                    .SetTitle("Marker Title")
                    .SetSnippet("Marker Snippet");

                currentMarker = _googleMap.AddMarker(markerOptions);

                distance = gameLogic.MessureDistance(location, latlng);
            }
        }
        private void RemoveMarker()
        {
            if (currentMarker != null)
            {
                currentMarker.Remove();
                currentMarker = null;
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
            builder.SetMessage("Are you sure you need a hint? Getting a hint will decrese this round's final score by 1000 points.");
            builder.SetCancelable(true);
            builder.SetPositiveButton("Give me a hint", HintAction);
            builder.SetNegativeButton("Nevermind", CancelAction);
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        private async void HintAction(object sender, DialogClickEventArgs e)//פעולה שמקבלת את מדינת מיקום ה streetview, ורושמת את המדינה במקום הכפתור, ומכבה אותו.
        {
            gameLogic.SetIsHintUsed();
            string country = await gameLogic.GetCountryFromCoordinates(latlng.Latitude, latlng.Longitude);

            if (!string.IsNullOrEmpty(country))
            {
                hintbtn.Enabled = false;
                hintbtn.Text = country;
            }
            else
            {
                Console.WriteLine("Unable to determine the country for the given coordinates.");
            }
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

            string disSet = distance.ToString();
            intent.PutExtra("distance", disSet);

            string points = gameLogic.UpdateScores(distance);
            intent.PutExtra("points", points);

            int finalpoints = gameLogic.FinalPoints;
            string fp = finalpoints.ToString();
            intent.PutExtra("finalpoints", fp);

            string latitude = latlng.Latitude.ToString();
            intent.PutExtra("panoLat", latitude);
            
            string Longitude = latlng.Longitude.ToString();
            intent.PutExtra("panoLon", Longitude);

            latitude = googlePoint.Latitude.ToString();
            intent.PutExtra("mapLat", latitude);

            Longitude = googlePoint.Longitude.ToString();
            intent.PutExtra("mapLon", Longitude);

            string serializedObj = JsonConvert.SerializeObject(player);
            intent.PutExtra("user", serializedObj);

            StartActivityForResult(intent, 0);
        }
        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            gameLogic.NextRound();
            this.round = "Round " + gameLogic.GetRoundNum() + "/5";
            roundview.Text = round;
            var mapContainer = FindViewById<FrameLayout>(Resource.Id.map);
            firstlocation = true;
            mapContainer.Visibility = ViewStates.Gone;
            streetViewPanoramaView.Visibility = ViewStates.Gone;
            isLatLngOk = false;
            while (!isLatLngOk)
            {
                latitude = GetRandomCoordinate(-60, 70);
                longitude = GetRandomCoordinate(-130, 150);
                if ((latitude > -60 && latitude < -10 && longitude > 15 && longitude < 63) || (latitude > -60 && latitude < -8 && longitude > 60 && longitude < 95) || (latitude > -60 && latitude < 0 && longitude > -30 && longitude < -15))
                    isLatLngOk = false;
                else
                    isLatLngOk = true;
            }
            latlng = new LatLng(latitude, longitude);
            streetViewPanoramaView.GetStreetViewPanoramaAsync(this);
            guessbtn.Enabled = false;
            RemoveMarker();
            hintbtn.Enabled = true;
            hintbtn.Text = "Hint";
            if (gameLogic.GetIsHintUsed())
                gameLogic.SetIsHintUsed();
        }
        public void OpenMapbtn_Click(object sender, EventArgs e)
        {
            var mapContainer = FindViewById<FrameLayout>(Resource.Id.map);
            if(mapContainer.Visibility == ViewStates.Gone)
            {
                mapContainer.Visibility = ViewStates.Visible;
                streetViewPanoramaView.Visibility = ViewStates.Gone;
            }
            else
            {
                mapContainer.Visibility = ViewStates.Gone;
                streetViewPanoramaView.Visibility = ViewStates.Visible;
            }
        }
    }
}