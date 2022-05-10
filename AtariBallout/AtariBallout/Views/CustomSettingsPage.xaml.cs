using System;
using System.Data.SqlClient;
using System.Diagnostics;
using AtariBallout.ViewModels;

using Windows.UI.Xaml.Controls;

namespace AtariBallout.Views
{
    public sealed partial class CustomSettingsPage : Page
    {
        bool FIRSTNAME_TEXTBOX_NOT_EMPTY = true;
        bool LASTNAME_TEXTBOX_NOT_EMPTY = true;
        bool USERNAME_TEXTBOX_NOT_EMPTY = true;

        public CustomSettingsViewModel ViewModel { get; } = new CustomSettingsViewModel();

        public CustomSettingsPage()
        {
            InitializeComponent();
        }

        private void EnableButton()
        {
            if (FIRSTNAME_TEXTBOX_NOT_EMPTY && LASTNAME_TEXTBOX_NOT_EMPTY && USERNAME_TEXTBOX_NOT_EMPTY)
                ChangeData_Button.IsEnabled = true;
        }

        private void Username_Change_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(Username_Change_TextBox.Text))
            {
                USERNAME_TEXTBOX_NOT_EMPTY = false;
                ChangeData_Button.IsEnabled = false;
            }
            else
            {
                USERNAME_TEXTBOX_NOT_EMPTY = true;
                EnableButton();
            }
        }

        private void FirstName_Change_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(FirstName_Change_TextBox.Text))
            {
                FIRSTNAME_TEXTBOX_NOT_EMPTY = false;
                ChangeData_Button.IsEnabled = false;
            }
            else
            {
                FIRSTNAME_TEXTBOX_NOT_EMPTY = true;
                EnableButton();
            }
        }

        private void LastName_Change_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (string.IsNullOrEmpty(LastName_Change_TextBox.Text))
            {
                LASTNAME_TEXTBOX_NOT_EMPTY = false;
                ChangeData_Button.IsEnabled = false;
            }
            else
            {
                FIRSTNAME_TEXTBOX_NOT_EMPTY = true;
                EnableButton();
            }
        }

        public static void ChangeUsername(string newUsername)
        {
            string CreateUserQuery =
                "UPDATE UsersTable " +
                $"Set Username = '{newUsername}' " +
                $"WHERE Username = '{App.RemoveRedundantSpaces(App.CONNECTED_USER.Username)}'";
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
                            Debug.WriteLine("Username change - successful");
                            //return true;
                        }
                        else
                        {
                            Debug.WriteLine("Username change - unsuccessful");
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            //return false;
        }

        public static void ChangeFirstName(string newFirstName)
        {
            string CreateUserQuery =
                "UPDATE UsersTable " +
                $"Set FirstName = '{newFirstName}' " +
                $"WHERE Username = '{App.RemoveRedundantSpaces(App.CONNECTED_USER.Username)}'";
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
                            Debug.WriteLine("Username change - successful");
                            //return true;
                        }
                        else
                        {
                            Debug.WriteLine("Username change - unsuccessful");
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            //return false;
        }

        public static void ChangeLastName(string newLastName)
        {
            string CreateUserQuery =
                "UPDATE UsersTable " +
                $"Set LastName = '{newLastName}' " +
                $"WHERE Username = '{App.RemoveRedundantSpaces(App.CONNECTED_USER.Username)}'";
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
                            Debug.WriteLine("Username change - successful");
                            //return true;
                        }
                        else
                        {
                            Debug.WriteLine("Username change - unsuccessful");
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            //return false;
        }

        private void ChangeData_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string newUsername = Username_Change_TextBox.Text;
            ChangeUsername(newUsername);
            ChangeFirstName(FirstName_Change_TextBox.Text);
            ChangeLastName(FirstName_Change_TextBox.Text);
            App.USER_DATA_CHANGED = true;
            App.CONNECTED_USER = App.GetUserByUsername(newUsername);
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.CURRENT_PAGE_IS_MAINPAGE = false;
            Username_Change_TextBox.Text = App.RemoveRedundantSpaces(App.CONNECTED_USER.Username);
            FirstName_Change_TextBox.Text = App.RemoveRedundantSpaces(App.CONNECTED_USER.FirstName);
            LastName_Change_TextBox.Text = App.RemoveRedundantSpaces(App.CONNECTED_USER.LastName);
        }
    }
}
