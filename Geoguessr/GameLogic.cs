using Android.App;
using Android.Content;
using Android.Gms.Maps.Model;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    public class GameLogic
    {
        private int roundNum;
        private int roundPoints;
        private int finalPoints;
        private int distance;
        private string hint;
        private GoogleMapsShow googleMaps;
        private StreetView streetView;

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
        /*public int MessureDistance(GoogleMapsShow maker, StreetView location)//מחשב את המרחק בין המרקר לנקודת streetview.
        {
            
        }
        public void UpdateScores(int roundPoints)
        {

        }
        public string GetHint()//מקבלת את הרמז ממחלקת streetview.
        {

        }
        */
    }
}