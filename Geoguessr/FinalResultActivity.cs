using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Google.Android.Material.Snackbar;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    [Activity(Label = "FinalResultActivity")]
    public class FinalResultActivity : Activity, View.IOnClickListener
    {
        private TextView finalscoreview;
        private TextView bestscoreview;
        private Button playagainbtn;
        private Button homescreenbtn;
        private Button leaderboard2btn;
        private Player player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.finalScore);

            finalscoreview = FindViewById<TextView>(Resource.Id.finalscoreview);
            bestscoreview = FindViewById<TextView> (Resource.Id.bestscoreview);
            playagainbtn = FindViewById<Button>(Resource.Id.playagainbtn);
            homescreenbtn = FindViewById<Button>(Resource.Id.mainpagebtn);
            leaderboard2btn = FindViewById<Button>(Resource.Id.leaderboardbtn2);
            playagainbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            homescreenbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            leaderboard2btn.SetBackgroundResource(Resource.Drawable.rounded_corner);

            string finalpoints = Intent.GetStringExtra("finalpoints");
            finalscoreview.Text = finalpoints;

            string serializedObj = Intent.GetStringExtra("user");
            player = JsonConvert.DeserializeObject<Player>(serializedObj);

            if(int.Parse(finalpoints) > player.bestScore)
                DbHelper.NewTopScore(player, int.Parse(finalpoints));
            string bestscore = player.bestScore.ToString();
            bestscoreview.Text = "Best Score: " + bestscore;

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
                string serializedObj = JsonConvert.SerializeObject(player);
                intent.PutExtra("user", serializedObj);
                StartActivity(intent);
            }
            if(view == homescreenbtn)
            {
                Intent intent = new Intent(this, typeof(MainActivity));
                string serializedObj = JsonConvert.SerializeObject(player);
                intent.PutExtra("user", serializedObj);

                string l = "True";
                intent.PutExtra("check", l);

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