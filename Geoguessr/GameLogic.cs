using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Java.Lang;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace Geoguessr
{
    public class GameLogic
    {
        private int roundNum;
        private int roundPoints;
        private int finalPoints;
        private bool isHintUsed = false;

        public GameLogic()
        {
            roundNum = 1;
            roundPoints = 0;
            finalPoints = 0;
        }
        public int GetRoundNum()
        {
            return roundNum;
        }
        public void NextRound()
        {
            roundNum++;
        }
        public int RoundPoints
        {
            get { return roundPoints; }
            set { roundPoints = value; }
        }
        public int FinalPoints
        {
            get { return finalPoints; }
            set { finalPoints = value; }
        }
        public bool GetIsHintUsed()
        {
            return isHintUsed;
        }
        public void SetIsHintUsed()
        {
            if(isHintUsed)
                isHintUsed = false;
            else
                isHintUsed = true;
        }
        public int MessureDistance(LatLng marker, LatLng location)
        {
            Location locationStart = new Location(marker.Latitude, marker.Longitude);
            Location locationEnd = new Location(location.Latitude, location.Longitude);

            int distance = (int)Location.CalculateDistance(locationStart, locationEnd, DistanceUnits.Kilometers);
            return distance;
        }
        public string UpdateScores(double distance)
        {
            int thisRoundPoints = 3000 - (int)System.Math.Pow(distance, 1.1);
            if (isHintUsed)
                thisRoundPoints -= 1000;
            if (thisRoundPoints < 0) { thisRoundPoints = 0; }
            finalPoints += thisRoundPoints;
            return thisRoundPoints.ToString();
        }
        public async Task<string> GetCountryFromCoordinates(double latitude, double longitude)
        {
            try
            {
                var placemarks = await Geocoding.GetPlacemarksAsync(latitude, longitude);
                var placemark = placemarks?.FirstOrDefault();

                if (placemark != null)
                {
                    return placemark.CountryName;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
                Console.WriteLine($"Feature not supported: {fnsEx.Message}");
            }
            catch (System.Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }
    }
}