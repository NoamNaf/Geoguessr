using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    [Table("Persons")]
    public class Player
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int bestScore { get; set; }

        //private string userName;
        //private string password;
        //private int bestScore;

        public Player(string userName, string password, int bestScore)
        {
            this.userName = password;
            this.password = password;
            this.bestScore = bestScore;
        }
        public Player(string userName, string password)
        {
            this.userName = userName;
            this.password = password;
            this.bestScore = 0;
        }
        public Player()
        {

        }
        public void SetPerson(string userName, string password, int bestScore)
        {
            this.userName = userName;
            this.password = password;
            this.bestScore = bestScore;
        }

                /*public Player()
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