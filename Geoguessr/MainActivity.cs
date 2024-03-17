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
using System.Windows.Input;
using Android.Gms.Common.Data;

namespace Geoguessr
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme", MainLauncher = true)]
    public class MainActivity : AppCompatActivity, View.IOnClickListener
    {
        private TextView usernameview;
        private Button loginbtn;
        private Button signupbtn;
        private Button playbtn;
        private Button leaderboardbtn;
        private Button tutorial;
        private Player player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            loginbtn = FindViewById<Button>(Resource.Id.loginbtn);
            signupbtn = FindViewById<Button>(Resource.Id.signupbtn);
            usernameview = FindViewById<TextView>(Resource.Id.usernameshow);
            playbtn = FindViewById<Button>(Resource.Id.playbtn);
            leaderboardbtn = FindViewById<Button>(Resource.Id.leaderboardbtn1);
            tutorial = FindViewById<Button>(Resource.Id.tutorial);
            playbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            leaderboardbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            tutorial.SetBackgroundResource(Resource.Drawable.rounded_corner);

            string register = Intent.GetStringExtra("Register");
            if(register != null )
                Toast.MakeText(this, "Account successfully created", ToastLength.Long).Show();

            string l = Intent.GetStringExtra("check");
            if (Intent != null && l == "True")
            {
                string serializedObj = Intent.GetStringExtra("user");
                player = JsonConvert.DeserializeObject<Player>(serializedObj);
                usernameview.Text = "Welcome, " + player.userName;
                loginbtn.Visibility = ViewStates.Invisible;
                signupbtn.Text = "Sign Out";
            }
            else
                player = new Player();

            loginbtn.Click += Loginbtn_Click;
            signupbtn.Click += SignInbtn_Click;
            playbtn.Click += Playbtn_Click;
            leaderboardbtn.SetOnClickListener(this);
            tutorial.Click += Tutorial_Click;
        }
        private void Loginbtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(LoginActivity));
            StartActivity(intent);
        }
        private void SignInbtn_Click(object sender, EventArgs e)
        {
            if(player.userName == null)
            {
                Intent intent = new Intent(this, typeof(RegisterActivity));
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                string l = "null";
                intent.PutExtra("check", l);
                StartActivity(intent);
            }
        }

        private void Playbtn_Click(object sender, EventArgs e)
        {
            if(player.userName != null)
            {
                Intent intent = new Intent(this, typeof(PlayActivity));
                string serializedObj = JsonConvert.SerializeObject(player);
                intent.PutExtra("user", serializedObj);
                StartActivity(intent);
            }
            else
            {
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("Notification");
                builder.SetMessage("In oreder to play the game, you have to login. Don't have an account? Create one now for free!");
                builder.SetCancelable(true);
                builder.SetPositiveButton("Login", Loginbtn_Click);
                builder.SetNegativeButton("Register", SignInbtn_Click);
                Android.App.AlertDialog dialog = builder.Create();
                dialog.Show();
            }
        }
        private void Tutorial_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(TutorialActivity));
            StartActivityForResult(intent, 0);
        }
        public void OnClick(Android.Views.View view)
        {
            if(playbtn == view)
            {
                Intent intent = new Intent(this, typeof(PlayActivity));
                intent.PutExtra("round", 1);

                string serializedObj = JsonConvert.SerializeObject(player);
                intent.PutExtra("user", serializedObj);

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