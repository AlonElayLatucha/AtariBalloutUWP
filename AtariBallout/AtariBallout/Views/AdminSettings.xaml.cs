using AtariBallout.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace AtariBallout.Views
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AdminSettings : Page
    {
        public AdminSettings()
        {
            this.InitializeComponent();
        }

        private void DeleteAllRecords_Button_Click(object sender, RoutedEventArgs e)
        {
            DeleteAllUsersButConnectedOne();
        }

        public static void DeleteAllUsersButConnectedOne()
        {
            string CreateUserQuery =
                "DELETE FROM UsersTable " +
                $"WHERE Username != '{App.CONNECTED_USER.Username}'";
            try
            {
                using (SqlConnection connection = new SqlConnection(App.CONNECTION_STRING))
                {
                    using (SqlCommand command = new SqlCommand(CreateUserQuery, connection))
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result == 1)
                        {
                            Debug.WriteLine("Users deletion - success");
                        }
                        else
                        {
                            Debug.WriteLine("Users deletion - fail");
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.CURRENT_PAGE_IS_MAINPAGE = false;
        }
    }
}
