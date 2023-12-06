using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.AppCompat.View.Menu;
using Google.Android.Material.Behavior;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    [Activity(Label = "Play")]
    public class PlayActivity : Activity
    {
        private string round;
        private TextView roundview;
        private Button guessbtn;
        private Button hintbtn;
        private Button openmapbtn;
        private GameLogic gameLogic;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Resource.Layout.play);
            roundview = FindViewById<TextView>(Resource.Id.roundview1);
            guessbtn = FindViewById<Button>(Resource.Id.guessbtn);
            hintbtn = FindViewById<Button>(Resource.Id.hintbtn);
            openmapbtn = FindViewById<Button>(Resource.Id.openmapbtn);

            if (Intent != null)
            {
                string serializedObj = Intent.GetStringExtra("gameLogic");
                gameLogic = JsonConvert.DeserializeObject<GameLogic>(serializedObj);
            }
            /*else
            {
                gameLogic = new GameLogic();
            }
            */

            gameLogic.NextRound();
            this.round = "Round " + gameLogic.GetRoundNum() + "/5";

            roundview.Text = "Round 1/5";

            guessbtn.Click += guessbtn_Click;
            hintbtn.Click += Hintbtn_Click;
            openmapbtn.Click += OpenMapbtn_Click;
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

        private void HintAction(object sender, DialogClickEventArgs e)//פעולה שמקבלת את מדינת מיקום ה streetview, ורושמת את המדינה במקום הכפתור, ומכבה אותו.
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
            string serializedObj = JsonConvert.SerializeObject(gameLogic);
            intent.PutExtra("gameLogic", serializedObj);
            StartActivity(intent);
        }
        public void OpenMapbtn_Click(object sender, EventArgs e)//פעם אחת הכפתור פותח את המפה על ה streetview, פעם אחרת הוא סוגר את המפה וחוזר ל streetview.
        {
            
        }
        public void TurnOnGuessButton()//פעולה שבודקת האם אפשר להדליק את הכפתור guess, אם כן היא מדליקה את הכפתור.
        {
            
        }
    }
}