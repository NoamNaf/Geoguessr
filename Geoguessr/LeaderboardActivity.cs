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

            // Create your application here
        }
        public void OnClick(Android.Views.View view)
        {
            Intent intent = new Intent(this, typeof(MainActivity));
            StartActivity(intent);
        }
        private void GetInformationFromDB(int rank)//פעולה שמקבלת דרגה מטבלת הנתונים, ומחזירה את המשתמש עם כמות הנקודות, לפי המבוקש.
        {

        }
    }
}