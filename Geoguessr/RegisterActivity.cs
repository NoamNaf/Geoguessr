﻿using Android.App;
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
    [Activity(Label = "RegisterActivity")]
    public class RegisterActivity : Activity, View.IOnClickListener
    {
        private EditText newUsername;
        private EditText newPassword;
        private Button registerbtn;
        private Player player;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.register);
            newUsername = FindViewById<EditText>(Resource.Id.newUsername);
            newPassword = FindViewById<EditText>(Resource.Id.newPassword);
            registerbtn = FindViewById<Button>(Resource.Id.registerbtn);
            registerbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            registerbtn.SetOnClickListener(this);
        }
        public void OnClick(View v)
        {
            this.player = new Player(newUsername.Text, newPassword.Text);
            if(!DbHelper.IsUserValid(player))
            {
                DbHelper.AddPlayer(player);
                Intent intent = new Intent(this, typeof(MainActivity));
                string text = "Register";
                intent.PutExtra("Register", text);
                StartActivity(intent);
            }
            else
                Toast.MakeText(this, "Username or Password faild, please try again", ToastLength.Long).Show();
        }
    }
}