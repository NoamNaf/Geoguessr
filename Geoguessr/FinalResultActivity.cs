using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    [Activity(Label = "FinalResultActivity")]
    public class FinalResultActivity : Activity, View.IOnClickListener
    {
        private Button playagainbtn;
        private Button homescreenbtn;
        private Button leaderboard2btn;
        private GameLogic gameLogic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.finalScore);

            playagainbtn = FindViewById<Button>(Resource.Id.playagainbtn);
            homescreenbtn = FindViewById<Button>(Resource.Id.mainpagebtn);
            leaderboard2btn = FindViewById<Button>(Resource.Id.leaderboardbtn2);
            playagainbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            homescreenbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            leaderboard2btn.SetBackgroundResource(Resource.Drawable.rounded_corner);

            playagainbtn.SetOnClickListener(this);
            homescreenbtn.SetOnClickListener(this);
            leaderboard2btn.SetOnClickListener(this);

            // Create your application here
        }
        public void OnClick(Android.Views.View view)
        {
            if(view == playagainbtn)
            {
                Intent intent = new Intent(this, typeof(PlayActivity));
                string num = "1";
                intent.PutExtra("round", num);
                StartActivity(intent);
            }
            if(view == homescreenbtn)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                StartActivity(intent);
            }
            if(view ==  leaderboard2btn)
            {
                Intent intent = new Intent(this, typeof(LeaderboardActivity));
                StartActivity(intent);
            }
        }
    }
}