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
    [Table("Player")]
    public class Player
    {
        [PrimaryKey, AutoIncrement, Column("_id")]
        public int id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int bestScore { get; set; }

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
        public Player() { }
        public void SetPerson(string userName, string password, int bestScore)
        {
            this.userName = userName;
            this.password = password;
            this.bestScore = bestScore;
        }
    }
}