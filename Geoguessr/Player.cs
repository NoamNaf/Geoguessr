﻿using Android.App;
using Android.Content;
using Android.Media;
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

        public Player()
        {
            this.userName = null;
            this.password = null;
            this.bestScore = 0;
        }
        /*public bool IsLegal(string userName, string passwrod)//בודקת אם השם משתמש והסיסמה שהמשתמש שם נכונים, אם לא מחזיר flase.
        {

        }
        public bool DoesExist(string userName)//בודקת האם המשתמש קיים. אם לא, מחזירה שקר.
        {

        }
        public bool IsLoggedIn()//כאשר לוחצים על startgame בעמוד הראשי.
        {

        }
        public void GetAccountFromDB(string userName, string password)//מקבלת את נתוני המשתמש, ושומרת אותם במחלקות המשחק.
        {

        }
        public void AddBestScoreToDB(string userName, string password)//מעדכנת את תוצאת שיא כל הזמנים של המשתמש ללוח הנתונים. אם זהו הפעם הראשנונה שנכנסו למשתמש, השיא יהיה 0.
        {

        }
        public void AddAccountToDB(string userName, string password)
        {

        }
        */
    }
}