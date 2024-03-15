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
    [Activity(Label = "LeaderboardActivity")]
    public class LeaderboardActivity : Activity, View.IOnClickListener
    {
        private Button returnbtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.leaderboard);
            returnbtn = FindViewById<Button>(Resource.Id.returnbtn);
            returnbtn.SetBackgroundResource(Resource.Drawable.rounded_corner);

            returnbtn.SetOnClickListener(this);

            TextView[] rankTextViews = new TextView[5];
            TextView[] nameTextViews = new TextView[5];
            TextView[] scoreTextViews = new TextView[5];

            rankTextViews[0] = FindViewById<TextView>(Resource.Id.r1rank);
            rankTextViews[1] = FindViewById<TextView>(Resource.Id.r2rank);
            rankTextViews[2] = FindViewById<TextView>(Resource.Id.r3rank);
            rankTextViews[3] = FindViewById<TextView>(Resource.Id.r4rank);
            rankTextViews[4] = FindViewById<TextView>(Resource.Id.r5rank);

            nameTextViews[0] = FindViewById<TextView>(Resource.Id.r1name);
            nameTextViews[1] = FindViewById<TextView>(Resource.Id.r2name);
            nameTextViews[2] = FindViewById<TextView>(Resource.Id.r3name);
            nameTextViews[3] = FindViewById<TextView>(Resource.Id.r4name);
            nameTextViews[4] = FindViewById<TextView>(Resource.Id.r5name);

            scoreTextViews[0] = FindViewById<TextView>(Resource.Id.r1score);
            scoreTextViews[1] = FindViewById<TextView>(Resource.Id.r2score);
            scoreTextViews[2] = FindViewById<TextView>(Resource.Id.r3score);
            scoreTextViews[3] = FindViewById<TextView>(Resource.Id.r4score);
            scoreTextViews[4] = FindViewById<TextView>(Resource.Id.r5score);

            List<Player> topPlayers = DbHelper.GetTopPlayers();

            for (int i = 0; i < 5; i++)
            {
                if (i < topPlayers.Count)
                {
                    nameTextViews[i].Text = topPlayers[i].userName;
                    scoreTextViews[i].Text = topPlayers[i].bestScore.ToString();
                }
                else
                {
                    rankTextViews[i].Visibility = ViewStates.Gone;
                    nameTextViews[i].Visibility = ViewStates.Gone;
                    scoreTextViews[i].Visibility = ViewStates.Gone;
                }
            }
        }
        public void OnClick(Android.Views.View view)
        {
            Intent intent = new Intent();
            Finish();
        }
    }
}