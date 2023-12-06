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
            newUsername = FindViewById<EditText>(Resource.Id.username);
            newPassword = FindViewById<EditText>(Resource.Id.newPassword);
            registerbtn = FindViewById<Button>(Resource.Id.registerbtn);
            registerbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            registerbtn.SetOnClickListener(this);
            // Create your application here
        }
        public void OnClick(View v)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        /*public bool IsAccountOK()
        {

        }
        */
        
    }
}