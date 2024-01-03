using Android.Animation;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Geoguessr
{
    public static class DbHelper
    {
        static string path = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), "database.db3");
        static readonly SQLiteConnection database;
        static DbHelper()
        {
            database = new SQLiteConnection(path);
            database.CreateTable<Player>();
        }
        public static void AddPlayer(Player player)
        {
            var db = new SQLiteConnection(path);
            db.Insert(player);
        }
        public static bool IsUserValid(Player player)
        {
            string strsql = string.Format($"SELECT * FROM Player WHERE userName='{player.userName}' AND password='{player.password}'");
            var users = database.Query<Player>(strsql);
            return users.Count != 0;
        }
        public static Player GetPlayerFromDB(Player player)
        {
            string strsql = string.Format($"SELECT * FROM Player WHERE userName='{player.userName}' AND password='{player.password}'");
            var users = database.Query<Player>(strsql);
            return users[0];
        }
        public static void NewTopScore(Player player)
        {
            string strsql = string.Format($"UPDATE * SET bestScore='{player.bestScore}' WHERE userName='{player.userName}' AND password='{player.password}')");
            var users = database.Query<Player>(strsql);
            var user = users[0];
        }
    }
}   