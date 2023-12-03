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
            if (loginbtn != null && password != null)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            else
            {
                Toast.MakeText(this, "Please enter a legal username and password", ToastLength.Long).Show();
            }
        }
        public bool IsAccountOK()
        {

        }
    }
}