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
            if (player.userName == "" || player.password == "")
                return true;
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
        public static void NewTopScore(Player player, int points)
        {
            string strsql = string.Format($"UPDATE Player SET bestScore='{points}' WHERE userName='{player.userName}' AND password='{player.password}'");
            database.Execute(strsql);
        }
        public static List<Player> GetTopPlayers()
        {
            string strsql = "SELECT * FROM Player ORDER BY bestScore DESC LIMIT 5";
            var topPlayers = database.Query<Player>(strsql);
            return topPlayers;
        }
    }
}   