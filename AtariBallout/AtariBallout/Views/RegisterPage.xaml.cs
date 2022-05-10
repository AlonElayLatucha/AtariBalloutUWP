using System;
using System.Data.SqlClient;
using System.Diagnostics;
using AtariBallout.Model;
using AtariBallout.ViewModels;
using Windows.UI.Popups;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AtariBallout.Views
{
    public sealed partial class RegisterPage : Page
    {
        public RegisterViewModel ViewModel { get; } = new RegisterViewModel();

        public RegisterPage()
        {
            InitializeComponent();
        }
        private void CreateUser_Button_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            HashSalt hashSalt = App.GenerateSaltedHash(64, UserPassword_PasswordBox.Password);
            UserObject user = new UserObject(UserName_TextBox.Text, hashSalt.Hash, hashSalt.Salt, FirstName_TextBox.Text, LastName_TextBox.Text);

            UserPassword_PasswordBox.Password = "";

            if (App.UserExistenceCheck(user.Username) == false)
            {
                InsertUser(user);
                // App.CONNECTED_USER = user;
                Frame.Navigate(typeof(StartScreenPage), null); // CHANGE PARAMETERS
            }
            else
            {
                Popup_UserExists_MessageDialog();
            }
        }

        public static void Popup_UserExists_MessageDialog()
        {
            App.CURRENT_PAGE_IS_MAINPAGE = false;
            // Create the message dialog and set its content
            var messageDialog = new MessageDialog("שם המשתמש תפוס", "אופס!");
            // Show the message dialog
            _ = messageDialog.ShowAsync();
        }



        public static void InsertUser(UserObject user)
        {
            string CreateUserQuery =
                "INSERT INTO UsersTable (Username, Hash, Salt, FirstName, LastName, Highscore) " +
                $"VALUES ('{user.Username}', '{user.Hash}', '{user.Salt}', '{user.FirstName}', '{user.LastName}', {user.Highscore})";
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
                            Debug.WriteLine("User add - success");
                        }
                        else
                        {
                            Debug.WriteLine("User add - fail");
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
        }


        private void FirstName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty((FirstName_TextBox.Text)))
            {
                LastName_TextBox.IsEnabled = true;
            }
            else
            {
                LastName_TextBox.IsEnabled = false;
            }
        }

        private void LastName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty((LastName_TextBox.Text)))
            {
                UserCredentials_PivotItem.IsEnabled = true;
                FullName_RunText.Text = $"{FirstName_TextBox.Text} {LastName_TextBox.Text}";
            }
            else
            {
                UserCredentials_PivotItem.IsEnabled = false;
            }
        }

        private void UserName_TextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!String.IsNullOrEmpty((UserName_TextBox.Text)))
            {
                UserPassword_PasswordBox.IsEnabled = true;
                Summary_PivotItem.IsEnabled = true;
                PersonPicture_SummarySequence.DisplayName = UserName_TextBox.Text;
                UserName_RunText.Text = UserName_TextBox.Text;
            }
            else
            {
                UserPassword_PasswordBox.IsEnabled = false;
                Summary_PivotItem.IsEnabled = false;
            }
        }

        private void UserPassword_PasswordBox_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty((UserPassword_PasswordBox.Password)))
            {
                Summary_PivotItem.IsEnabled = true;
                if (FirstName_TextBox.IsEnabled && LastName_TextBox.IsEnabled && UserName_TextBox.IsEnabled)
                {
                    CreateUser_Button.IsEnabled = true;
                }
            }
            else
            {
                Summary_PivotItem.IsEnabled = false;
                CreateUser_Button.IsEnabled = false;
            }
        }

        private void UserName_TextBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (FocusManager.GetFocusedElement() == UserPassword_PasswordBox) // Change the inputTextBox to your TextBox name
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }
                else
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }

                // Make sure to set the Handled to true, otherwise the RoutedEvent might fire twice
                e.Handled = true;
            }
        }

        private void UserPassword_PasswordBox_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (FocusManager.GetFocusedElement() == CreateUser_Button) // Change the inputTextBox to your TextBox name
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }
                else
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }

                // Make sure to set the Handled to true, otherwise the RoutedEvent might fire twice
                e.Handled = true;
            }
        }

        private void FirstName_TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (FocusManager.GetFocusedElement() == LastName_TextBox) // Change the inputTextBox to your TextBox name
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }
                else
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }

                // Make sure to set the Handled to true, otherwise the RoutedEvent might fire twice
                e.Handled = true;
            }
        }

        private void LastName_TextBox_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (FocusManager.GetFocusedElement() == UserName_TextBox) // Change the inputTextBox to your TextBox name
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }
                else
                {
                    FocusManager.TryMoveFocus(FocusNavigationDirection.Next);
                }

                // Make sure to set the Handled to true, otherwise the RoutedEvent might fire twice
                e.Handled = true;
            }
        }
    }
}
