using System;
using System.Data.SqlClient;
using System.Diagnostics;
using AtariBallout.Model;
using AtariBallout.ViewModels;
using Windows.ApplicationModel.Core;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;

namespace AtariBallout.Views
{
    public sealed partial class StartScreenPage : Page
    {
        public StartScreenViewModel ViewModel { get; } = new StartScreenViewModel();

        public StartScreenPage()
        {
            InitializeComponent();
        }

        private void RegisterButtonTextBlock_PointerPressed(object sender, Windows.UI.Xaml.Input.PointerRoutedEventArgs e)
        {
            Frame.Navigate(typeof(RegisterPage));
        }

        private void UsernameTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            CheckIfSignInButtonEligible();
        }

        private void PasswordBox_StartPage_PasswordChanged(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            CheckIfSignInButtonEligible();
        }

        private void CheckIfSignInButtonEligible()
        {
            if (String.IsNullOrEmpty(PasswordBox_StartPage.Password) == false && String.IsNullOrEmpty(User_TextBox_StartPage.Text) == false)
            {
                PassportSignInButton.IsEnabled = true;
            }
            else
            {
                PassportSignInButton.IsEnabled = false;
            }
        }

        private void PassportSignInButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            string username = User_TextBox_StartPage.Text;
            UserObject user = App.GetUserByUsername(username);
            bool isPasswordMatched = App.VerifyUserAuth(PasswordBox_StartPage.Password, user.Hash, user.Salt);
            PasswordBox_StartPage.Password = ""; // SECURITY RISK

            if (isPasswordMatched)
            {
                App.CONNECTED_USER = App.GetUserByUsername(username);
                PlaySoundEffectAsync($"win11.wav");
                Frame.Navigate(typeof(MainPage), null);
            }
        }

        public static async void PlaySoundEffectAsync(string audiofile_name)
        {
            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Sound Effects");
            var file = await folder.GetFileAsync(audiofile_name);
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        private void Page_Loaded(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            App.CURRENT_PAGE_IS_MAINPAGE = false;
            ApplicationViewTitleBar formattableTitleBar = ApplicationView.GetForCurrentView().TitleBar;
            formattableTitleBar.ButtonBackgroundColor = Colors.Transparent;
            CoreApplicationViewTitleBar coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        private void User_TextBox_StartPage_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (FocusManager.GetFocusedElement() == PasswordBox_StartPage) // Change the inputTextBox to your TextBox name
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

        private void PasswordBox_StartPage_KeyDown(object sender, Windows.UI.Xaml.Input.KeyRoutedEventArgs e)
        {
            if (e.Key == Windows.System.VirtualKey.Enter)
            {
                if (FocusManager.GetFocusedElement() == PassportSignInButton) // Change the inputTextBox to your TextBox name
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
