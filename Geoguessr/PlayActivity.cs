using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.View.Menu;
using Google.Android.Material.Behavior;
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
        private Button hintbtn;
        private string round;
        private GameLogic gamelogic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            //string num = Intent.GetStringExtra("round2");
            this.round = "Round " + Intent.GetStringExtra("round") + "/5";
            SetContentView(Resource.Layout.play);
            roundview = FindViewById<TextView>(Resource.Id.roundview1);
            guessbtn = FindViewById<Button>(Resource.Id.guessbtn);
            hintbtn = FindViewById<Button>(Resource.Id.hintbtn);
            //roundview.Text = this.round;
            //string num = gameLogic.GetRoundNum();

            roundview.Text = gamelogic.GetRoundNum();

            guessbtn.Click += guessbtn_Click;
            hintbtn.Click += Hintbtn_Click;
            // Create your application here
        }

        private void Hintbtn_Click(object sender, EventArgs e)
        {
            Android.App.AlertDialog.Builder builder = new Android.App.AlertDialog.Builder(this);
            builder.SetTitle("Warning");
            builder.SetMessage("Are you sure you need a hint? Getting a hint will hurt this round's final score.");
            builder.SetCancelable(true);
            builder.SetPositiveButton("Give me a hint", HintAction);
            builder.SetNegativeButton("Nevermind", CancelAction);
            Android.App.AlertDialog dialog = builder.Create();
            dialog.Show();
        }

        private void HintAction(object sender, DialogClickEventArgs e)
        {
            hintbtn.Enabled = false;
            hintbtn.Text = "USA";
        }

        private void CancelAction(object sender, DialogClickEventArgs e)
        {
            Toast.MakeText(this, "Cancel clicked", ToastLength.Short).Show();
        }

        private void guessbtn_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent(this, typeof(RoundScoreActivity));
            StartActivity(intent);
        }
    }
}