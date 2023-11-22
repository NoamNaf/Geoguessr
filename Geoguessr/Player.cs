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
    public class Player
    {
        private string userName;
        private string password;
        private int bestScore;

        public bool IsLegal(string userName, string passwrod)
        {

        }
        public bool DoesExist(string userName, string passwrod)
        {

        }
        public bool IsLoggedIn()//כאשר לוחצים על startgame בעמוד הראשי.
        {

        }
        public void GetAccountFromDB(string userName, string passwrod)
        {

        }
        public void AddToDB(string userName, string passwrod)
        {
            bestScore = 0;
        }
    }
}