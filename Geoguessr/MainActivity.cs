using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Android.Views;
using Android.Content;
using System;

namespace Geoguessr
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        private Button playbtn;
        private Button leaderboardbtn;
        private GameLogic gameLogic;
        ISharedPreferences sp;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            playbtn = FindViewById<Button>(Resource.Id.playbtn);
            playbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            leaderboardbtn = FindViewById<Button>(Resource.Id.leaderboardbtn1);
            leaderboardbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);

            playbtn.Click += Playbtn_Click;
            leaderboardbtn.SetOnClickListener(this);
        }

        private void Playbtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(PlayActivity));
            string num = "1";
            intent.PutExtra("round", num);
            StartActivity(intent);
        }

        public void OnClick(Android.Views.View view)
        {
            if(playbtn == view)
            {
                Intent intent = new Intent(this, typeof(PlayActivity));
                intent.PutExtra("round", 1);
                StartActivity(intent);
            }
            if(leaderboardbtn == view)
            {
                Intent intent = new Intent(this, typeof(LeaderboardActivity));
                StartActivityForResult(intent, 0);
            }
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}