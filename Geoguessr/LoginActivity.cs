using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Newtonsoft.Json;
using Org.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    [Activity(Label = "LoginActivity")]
    public class LoginActivity : Activity, View.IOnClickListener
    {
        private EditText username;
        private EditText password;
        private Button loginbtn;
        private Button goRegister;
        private Player player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.login);
            loginbtn = FindViewById<Button>(Resource.Id.loginbtn);
            username = FindViewById<EditText>(Resource.Id.username);
            password = FindViewById<EditText>(Resource.Id.password);
            //goRegister = FindViewById<Button>(Resource.Id.goRegisterbtn);

            loginbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            loginbtn.SetOnClickListener(this);
            // Create your application here
        }
        public void OnClick(View v)
        {
            if (loginbtn == null || password == null)
            {
                Toast.MakeText(this, "Please enter a legal username and password", ToastLength.Long).Show();
                return;
            }
            player = new Player(username.Text, password.Text);
            if (DbHelper.IsUserValid(player))
            {
                player = DbHelper.GetPlayerFromDB(player);
                Intent intent = new Intent(this, typeof(MainActivity));
                string serializedObj = JsonConvert.SerializeObject(player);
                intent.PutExtra("user", serializedObj);

                string l = "True";
                intent.PutExtra("check", l);
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "Username or/and password are wrong, please try again", ToastLength.Long).Show();
            }

        }
        /*public bool IsAccountOK()
        {

        }
        */
    }
}