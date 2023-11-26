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

namespace Geoguessr
{
    [Activity(Label = "RoundScoreActivity")]
    public class RoundScoreActivity : Activity, View.IOnClickListener
    {
        private TextView roundview;
        private Button continuebtn;
        private GameLogic gameLogic;
        private string round;
        private bool flag = true;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.roundScore);
            continuebtn = FindViewById<Button>(Resource.Id.continuebtn);

            roundview = FindViewById<TextView>(Resource.Id.roundview2);
            this.round = "Round " + Intent.GetStringExtra("round") + "/5";
            roundview.Text = this.round;
            int rnum = int.Parse(Intent.GetStringExtra("round"));
            if (rnum == 5)
            {
                continuebtn.Text = "See Final Result";
                flag = false;
            }
            

            continuebtn.SetOnClickListener(this);
            // Create your application here
        }
        public void OnClick(Android.Views.View view)
        {
            if(flag)
            {
                Intent intent = new Intent();
                //string round = intent.GetStringExtra("round");
                string round = intent.GetStringExtra("round");
                intent.PutExtra("round", round);
                SetResult(Result.Ok, intent);
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