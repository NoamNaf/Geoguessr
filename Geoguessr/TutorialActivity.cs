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
    [Activity(Label = "TutorialActivity")]
    public class TutorialActivity : Activity
    {
        private Button mainpagebtn;
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.tutorial);
            mainpagebtn = FindViewById<Button>(Resource.Id.mainpagebtn);
            mainpagebtn.SetBackgroundResource(Resource.Drawable.rounded_corner);
            mainpagebtn.Click += MainPage_Click;

            // Create your application here
        }
        private void MainPage_Click(object sender, EventArgs e)
        {
            Intent intent = new Intent();
            Finish();
        }
    }
}