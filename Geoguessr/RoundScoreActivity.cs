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
        private GameLogic gameLogic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.roundScore);
            continuebtn = FindViewById<Button>(Resource.Id.continuebtn);

            roundview = FindViewById<TextView>(Resource.Id.roundview2);
            this.round = "Round " + Intent.GetStringExtra("round") + "/5";
            roundview.Text = this.round;

            if (Intent != null)
            {
                string serializedObj = Intent.GetStringExtra("gameLogic");
                gameLogic = JsonConvert.DeserializeObject<GameLogic>(serializedObj);
            }
            this.round = "Round " + gameLogic.GetRoundNum() + "/5";
            roundview.Text = this.round;

            continuebtn.SetOnClickListener(this);
            // Create your application here
        }
        public void OnClick(Android.Views.View view)
        {
            if(flag)
            {
                Intent intent = new Intent(this, typeof(PlayActivity));
                string serializedObj = JsonConvert.SerializeObject(gameLogic);
                intent.PutExtra("gameLogic", serializedObj);
                StartActivity(intent);
            }
            else
            {
                Intent intent = new Intent(this, typeof(FinalResultActivity));
                StartActivity(intent);
            }
        }
    }
}