using Android.App;
using Android.Content;
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
        public int roundNum;
        private int roundPoints;
        private int finalPoints;
        private int distance;
        private string hint;

        public GameLogic()
        {
            roundNum = 1;
            roundPoints = 0;
            finalPoints = 0;
        }
        public string GetRoundNum()
        {
            return roundNum.ToString();
        }
        public void NextRound()
        {
            roundNum++;
        }
        public int RoundPoints
        { get { return roundPoints; }
        set { roundPoints = value; }
        }

        public int MessureDistance(GoogleMapsShow maker, StreetView location)
        {
            
        }
        public void UpdateScores(int roundPoints)
        {

        }
    }
}