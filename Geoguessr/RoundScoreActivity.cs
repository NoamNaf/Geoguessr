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

namespace Geoguessr
{
    [Activity(Label = "RoundScoreActivity")]
    public class RoundScoreActivity : Activity, View.IOnClickListener
    {
        private TextView roundview;
        private Button continuebtn;
        private string round;
        private bool flag = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.roundScore);
            continuebtn = FindViewById<Button>(Resource.Id.continuebtn);
            roundview = FindViewById<TextView>(Resource.Id.roundview2);

            string roundst = Intent.GetStringExtra("round");
            this.round = "Round " + roundst + "/5";
            roundview.Text = this.round;

            int roundint = int.Parse(roundst);
            if(roundint == 5)
                flag = false;

            continuebtn.SetOnClickListener(this);
            // Create your application here
        }
        public void OnClick(Android.Views.View view)
        {
            if(flag)
            {
                Intent intent = new Intent();
                Finish();
            }
            else
            {
                Intent intent = new Intent(this, typeof(FinalResultActivity));
                StartActivity(intent);
            }
        }
    }
}