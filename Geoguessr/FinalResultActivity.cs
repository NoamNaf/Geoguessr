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
        private Player player;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.finalScore);

            finalscoreview = FindViewById<TextView>(Resource.Id.finalscoreview);
            bestscoreview = FindViewById<TextView> (Resource.Id.bestscoreview);
            playagainbtn = FindViewById<Button>(Resource.Id.playagainbtn);
            homescreenbtn = FindViewById<Button>(Resource.Id.mainpagebtn);
            playagainbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            homescreenbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);

            string finalpoints = Intent.GetStringExtra("finalpoints");
            finalscoreview.Text = finalpoints;

            string serializedObj = Intent.GetStringExtra("user");
            player = JsonConvert.DeserializeObject<Player>(serializedObj);

            if(int.Parse(finalpoints) > player.bestScore)
            {
                DbHelper.NewTopScore(player, int.Parse(finalpoints));
                player.bestScore = int.Parse(finalpoints);
                Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
                builder.SetTitle("New Personal Record!!!");
                builder.SetMessage("Click anywhere on the screen to close the alert");
                builder.SetCancelable(true);
                Android.App.AlertDialog dialog = builder.Create();
                dialog.Show();
            }
            string bestscore = player.bestScore.ToString();
            bestscoreview.Text = "Best Score: " + bestscore;

            playagainbtn.SetOnClickListener(this);
            homescreenbtn.SetOnClickListener(this);
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
        }
    }
}