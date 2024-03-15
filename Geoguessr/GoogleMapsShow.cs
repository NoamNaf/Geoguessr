using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Javax.Crypto.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    public class GoogleMapsShow
    {
        private double latitude;
        private double longitude;

        public GoogleMapsShow(double latitude, double longitude)
        {
            this.latitude = latitude;
            this.longitude = longitude;
        }
        public double GetLatitude()
        {
            return latitude;
        }
        public double GetLongitude()
        {
            return longitude;
        }
    }
}