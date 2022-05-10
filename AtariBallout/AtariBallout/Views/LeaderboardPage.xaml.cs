using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using AtariBallout.Model;
using AtariBallout.ViewModels;

using Windows.UI.Xaml.Controls;

namespace AtariBallout.Views
{
    public sealed partial class LeaderboardPage : Page
    {
        private bool READ_ALOUD_PLAYING = false;

        public LeaderboardViewModel ViewModel { get; } = new LeaderboardViewModel();

        public LeaderboardPage()
        {
            InitializeComponent();
        }

        public static ObservableCollection<UserObject> GetLeaderboard()
        {
            const string GetUsersQuery =
               "SELECT TOP(10) * " +
               "FROM UsersTable " +
               "WHERE Highscore != 0 " +
               "ORDER BY Highscore DESC";
            var users = new ObservableCollection<UserObject>();
            try
            {
                using (SqlConnection conn = new SqlConnection(App.CONNECTION_STRING))
                {
                    conn.Open();
                    if (conn.State == System.Data.ConnectionState.Open)
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandText = GetUsersQuery;
                            using (SqlDataReader reader = cmd.ExecuteReader())
                            {
                                while (reader.Read())
                                {
                                    var userObj = new UserObject();
                                    userObj.Username = App.RemoveRedundantSpaces(reader.GetString(0));
                                    userObj.Hash = App.RemoveRedundantSpaces(reader.GetString(1));
                                    userObj.Salt = App.RemoveRedundantSpaces(reader.GetString(2));
                                    userObj.FirstName = App.RemoveRedundantSpaces(reader.GetString(3));
                                    userObj.LastName = App.RemoveRedundantSpaces(reader.GetString(4));
                                    userObj.Highscore = reader.GetInt32(5);
                                    users.Add(userObj);
                                }
                            }
                        }
                    }
                }
                return users;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.CURRENT_PAGE_IS_MAINPAGE = false;
            InventoryList.ItemsSource = GetLeaderboard();
        }

        public void ToSpeech()
        {
            var list = new List<UserObject>(GetLeaderboard());
            foreach (var item in list)
            {
                var txt = (("ל" + item.FirstName + " " + item.LastName + " יֵשׁ " + item.Highscore + " נְקוּדוֹת ") as string);
                App.allitem += txt.ToString() + "<break time='500ms'/>";
            }
            App.readText(App.allitem);
            App.allitem = "";
        }

        private void ReadAloud_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (READ_ALOUD_PLAYING)
            {
                App.mediaplayer.Stop();
                READ_ALOUD_PLAYING = false;
                ReadAloud_Button_SymbolIcon = new SymbolIcon(Symbol.Play);
            }
            else
            {
                ToSpeech();
                READ_ALOUD_PLAYING = true;
                ReadAloud_Button_SymbolIcon = new SymbolIcon(Symbol.Stop);
            }
        }
    }
}
