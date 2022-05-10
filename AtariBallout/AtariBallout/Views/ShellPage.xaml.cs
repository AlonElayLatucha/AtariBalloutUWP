using System;
using System.Threading.Tasks;
using AtariBallout.Services;
using AtariBallout.ViewModels;
using Microsoft.UI.Xaml.Controls;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AtariBallout.Views
{
    // TODO WTS: Change the icons and titles for all NavigationViewItems in ShellPage.xaml.
    public sealed partial class ShellPage : Page
    {
        public ShellViewModel ViewModel { get; } = new ShellViewModel();

        DispatcherTimer userUpdateTimer;

        public ShellPage()
        {
            InitializeComponent();
            DataContext = ViewModel;
            ViewModel.Initialize(shellFrame, navigationView, KeyboardAccelerators);
            UserOptions_Button.Visibility = Visibility.Collapsed;
            BoardType_Button.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Collapsed;
            Admin_Settings_Button.Visibility = Visibility.Collapsed;

            userUpdateTimer = new DispatcherTimer();
            userUpdateTimer.Tick += UserUpdateTimer_Tick;
            userUpdateTimer.Start();
        }

        private void UserUpdateTimer_Tick(object sender, object e)
        {
            if (App.CONNECTED_USER != null)
            {
                if (App.CURRENT_PAGE_IS_MAINPAGE)
                    EnableAllUserSensitiveControls();
                else
                    DisableAllUserSensitiveControls();
                navigationView.SelectedItem = 0;
            }
            if (App.USER_DATA_CHANGED)
            {
                UserOptionsButton_TextBlock.Text = App.CONNECTED_USER.Username;
                App.USER_DATA_CHANGED = false;
            }
            if (App.GAME_STOPPED)
            {
                Pause_Stop_Game();
            }
        }

        private void EnableAllUserSensitiveControls()
        {
            UserOptionsButton_TextBlock.Text = App.CONNECTED_USER.Username;
            UserOptions_Button.Visibility = Visibility.Visible;
            BoardType_Button.Visibility = Visibility.Visible;
            PlayButton.Visibility = Visibility.Visible;
            PauseButton.Visibility = Visibility.Visible;
            Admin_Settings_Button.Visibility = Visibility.Visible;

            navigationView.IsBackEnabled = false;
        }

        private void DisableAllUserSensitiveControls()
        {
            UserOptions_Button.Visibility = Visibility.Collapsed;
            BoardType_Button.Visibility = Visibility.Collapsed;
            PlayButton.Visibility = Visibility.Collapsed;
            PauseButton.Visibility = Visibility.Collapsed;
            Admin_Settings_Button.Visibility = Visibility.Collapsed;

            navigationView.IsBackEnabled = true;
        }

        private void PlayButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            ResumeGame();
        }

        public void PauseButton_Click(object sender, Windows.UI.Xaml.RoutedEventArgs e)
        {
            Pause_Stop_Game();
        }

        public void Pause_Stop_Game()
        {
            App.GAME_STOPPED = true;
            MainPage.dispatcherTimerObject.Stop();
            MainPage.secondsTimerObject.Stop();
            PlayButton.IsEnabled = true;
            PauseButton.IsEnabled = false;
        }

        public void ResumeGame()
        {
            App.GAME_STOPPED = false;
            MainPage.dispatcherTimerObject.Start();
            MainPage.secondsTimerObject.Start();
            PlayButton.IsEnabled = false;
            PauseButton.IsEnabled = true;
        }

        private void PlayButton_IsEnabledChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (PlayButton.IsEnabled == true)
            {
                App.EnablityOfPlayButton = true;
            }
            else
            {
                App.EnablityOfPlayButton = false;
            }
        }

        private void SignOut_MenuFlyoutItem_Button_Click(object sender, RoutedEventArgs e)
        {
            GoToStart();
            navigationView.SelectedItem = 0;
            DisableAllUserSensitiveControls();
            App.CONNECTED_USER = null;
        }

        private async void GoToStart()
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
               {
                   shellFrame.Navigate(typeof(StartScreenPage));
               });
        }

        private void RedBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            App.GAME_BOARD_TYPE = BoardType.RED;
            App.GAME_BOARD_TYPE_CHANGED = true;
        }

        private void BlueBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            App.GAME_BOARD_TYPE = BoardType.BLUE;
            App.GAME_BOARD_TYPE_CHANGED = true;
        }

        private void MintBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            App.GAME_BOARD_TYPE = BoardType.MINT;
            App.GAME_BOARD_TYPE_CHANGED = true;
        }

        private void OrangeBoard_Button_Click(object sender, RoutedEventArgs e)
        {
            App.GAME_BOARD_TYPE = BoardType.ORANGE;
            App.GAME_BOARD_TYPE_CHANGED = true;
        }

        private async void ChangeUserData_MenuFlyoutItem_Button_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                shellFrame.Navigate(typeof(CustomSettingsPage));
            });
        }

        private async void Admin_Settings_Button_Click(object sender, RoutedEventArgs e)
        {
            await Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                shellFrame.Navigate(typeof(AdminSettings));
            });
        }

        /*private void DarkLight_Theme_ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch toggleSwitch = sender as ToggleSwitch;
            if (toggleSwitch != null)
            {
                if (toggleSwitch.IsOn == true)
                {
                    App.
                }
                else
                {
                    progress1.IsActive = false;
                    progress1.Visibility = Visibility.Collapsed;
                }
            }
        }*/
    }
}
