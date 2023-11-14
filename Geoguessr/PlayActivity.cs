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
    [Activity(Label = "Play")]
    public class PlayActivity : Activity
    {
        private TextView roundview;
        private Button guessbtn;
        private string round;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //string num = Intent.GetStringExtra("round2");
            this.round = "Round " + Intent.GetStringExtra("round") + "/5";
            SetContentView(Resource.Layout.play);
            roundview = FindViewById<TextView>(Resource.Id.roundview1);
            roundview.Text = this.round;
            guessbtn = FindViewById<Button>(Resource.Id.guessbtn);

            guessbtn.Click += guessbtn_Click;
            // Create your application here
        }

        private void guessbtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RoundScoreActivity));
            intent.PutExtra("round", Intent.GetStringExtra("round"));
            StartActivity(intent);
        }
    }
}