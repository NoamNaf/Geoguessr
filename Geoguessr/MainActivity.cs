using Android.App;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using AndroidX.AppCompat.App;
using Android.Views;
using Android.Content;
using System;
using System.IO;
using Newtonsoft.Json;

namespace Geoguessr
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        private TextView usernameview;
        private Button loginOrSignUpbtn;
        private Button playbtn;
        private Button leaderboardbtn;
        private Player player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.activity_main);
            usernameview = FindViewById<TextView>(Resource.Id.usernameshow);
            loginOrSignUpbtn = FindViewById<Button>(Resource.Id.loginorsignupbtn);
            playbtn = FindViewById<Button>(Resource.Id.playbtn);
            leaderboardbtn = FindViewById<Button>(Resource.Id.leaderboardbtn1);
            loginOrSignUpbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            playbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            leaderboardbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);

            player = new Player();
            string l = Intent.GetStringExtra("check");
            if (Intent != null && l == "True")
            {
                string serializedObj = Intent.GetStringExtra("user");
                player = JsonConvert.DeserializeObject<Player>(serializedObj);
                usernameview.Text = "Welcome, " + player.userName;
            }
            loginOrSignUpbtn.Click += LoginOrSignUpbtn_Click;
            playbtn.Click += Playbtn_Click;
            leaderboardbtn.SetOnClickListener(this);
        }

        private void LoginOrSignUpbtn_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Notification");
            builder.SetMessage("");
            builder.SetCancelable(true);
            builder.SetPositiveButton("Login", LoginAction);
            builder.SetNegativeButton("Register", RegisterAction);
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        private void LoginAction(object sender, DialogClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }

        private void RegisterAction(object sender, DialogClickEventArgs e)
        {
            Intent intent = new Intent(this, typeof(RegisterActivity));
            StartActivity(intent);
        }

        private void Playbtn_Click(object sender, EventArgs e)
        {
            if(player.userName != null)
            {
                Intent intent = new Intent(this, typeof(PlayActivity));
                StartActivity(intent);
            }
            else
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("Notification");
                builder.SetMessage("In oreder to play the game, you have to login. Don't have an account? Create one now for free!");
                builder.SetCancelable(true);
                builder.SetPositiveButton("Login", LoginAction);
                builder.SetNegativeButton("Register", RegisterAction);
                Android.App.AlertDialog dialog = builder.Create();
                dialog.Show();
            }
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