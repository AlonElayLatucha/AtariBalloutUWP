using System;
using System.Data.SqlClient;
using System.Diagnostics;
using AtariBallout.AnimatedObjects;
using AtariBallout.Model;
using AtariBallout.ViewModels;
using Windows.Foundation;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace AtariBallout.Views
{
    public enum BallType
    {
        RED,
        BLUE
    };

    public enum BoardType
    {
        DEFAULT,
        RED,
        BLUE,
        ORANGE,
        MINT
    }

    public sealed partial class MainPage : Page
    {
        public MainViewModel ViewModel { get; } = new MainViewModel();

        public static DispatcherTimer dispatcherTimerObject = new DispatcherTimer();

        public static DispatcherTimer secondsTimerObject = new DispatcherTimer();

        public static DispatcherTimer userConnectionTimer = new DispatcherTimer();

        private int secondsCounter = 0;

        public const int UPPER_LIMIT_CANVAS = 761;
        public const int LOWER_LIMIT_CANVAS = -35;

        public BallObject ballObject;
        public RectangleObject rectangleObject;

        public MainPage()
        {
            InitializeComponent();
            Windows.UI.ViewManagement.ApplicationView.GetForCurrentView().ExitFullScreenMode();

            userConnectionTimer.Tick += UserConnectionTimer_Tick;
            userConnectionTimer.Start();

            dispatcherTimerObject.Tick += DispatcherTimerObject_Tick;
            //dispatcherTimerObject.Start();

            ballObject = new BallObject(width: 50, height: 50, distancePerMove: 3, canvas: Game_Canvas, ballType: BallType.BLUE, ellipse: Ball_Ellipse);
            rectangleObject = new RectangleObject(width: 150, height: 20, distancePerMove: 15, canvas: Game_Canvas, rectangle: Moving_Rectangle);

            secondsTimerObject.Interval = new TimeSpan(0, 0, 1);
            secondsTimerObject.Tick += SecondsTimerObject_Tick;
            // secondsTimerObject.Start();
        }

        private void UserConnectionTimer_Tick(object sender, object e)
        {
            if (App.CONNECTED_USER == null)
            {
                Frame.Navigate(typeof(StartScreenPage), null);
            }
            if (App.GAME_BOARD_TYPE_CHANGED)
            {
                switch (App.GAME_BOARD_TYPE)
                {
                    case BoardType.RED:
                        rectangleObject.rectangle.Fill = new SolidColorBrush(Colors.Red);
                        break;
                    case BoardType.BLUE:
                        rectangleObject.rectangle.Fill = new SolidColorBrush(Colors.Blue);
                        break;
                    case BoardType.MINT:
                        rectangleObject.rectangle.Fill = new SolidColorBrush(Color.FromArgb(255, 152, 255, 152));
                        break;
                    case BoardType.ORANGE:
                        rectangleObject.rectangle.Fill = new SolidColorBrush(Colors.Orange);
                        break;
                }
                App.GAME_BOARD_TYPE_CHANGED = false;
            }
        }

        public async void PlaySoundEffectAsync(string audiofile_name)
        {
            var element = new MediaElement();
            var folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Sound Effects");
            var file = await folder.GetFileAsync(audiofile_name);
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            element.SetSource(stream, "");
            element.Play();
        }

        private void SecondsTimerObject_Tick(object sender, object e)
        {
            secondsCounter += 1;

            int currSec = secondsCounter % 60,
                currMin = secondsCounter / 60;
            string secStr = $"{currSec}", mintStr = $"{currMin}";
            if (currSec < 10)
                secStr = $"0{currSec}";
            if (currMin < 10)
                mintStr = $"0{currMin}";
            TimeTextBox.Text = $"{mintStr}:{secStr}";

            switch (currSec)
            {
                case 15:
                    IncreaseLevel();
                    break;
                case 30:
                    IncreaseLevel();
                    break;
                case 40:
                    IncreaseLevel();
                    break;
                case 55:
                    IncreaseLevel();
                    break;
                default:
                    break;
            }
        }

        public void IncreaseLevel()
        {
            ballObject.yDistancePerMove += 5;
            rectangleObject.distancePerMove *= 1.3;
            LevelTextBox.Text = $"{int.Parse(LevelTextBox.Text) + 1}";
        }

        private void DispatcherTimerObject_Tick(object sender, object e)
        {
            double xCoord = Canvas.GetLeft(ballObject.ellipse);
            double yCoord = Canvas.GetTop(ballObject.ellipse);
            if (xCoord <= 0 || xCoord >= ballObject.canvas.ActualWidth - ballObject.width)
            {
                ballObject.distancePerMove *= -1;
                PlaySoundEffectAsync(@"Pong Wall hit.wav");
            }
            if (yCoord <= 0 || yCoord >= ballObject.canvas.ActualHeight - ballObject.height)
            {
                ballObject.yDistancePerMove *= -1;
                if (yCoord >= rectangleObject.DEFAULT_Y_LOCATION)
                {
                    PlaySoundEffectAsync(@"Pong Game fail.wav");
                    App.GAME_STOPPED = true;
                    ballObject.ResetLocation();
                    rectangleObject.ResetLocation();
                    PointsTextBox.Text = $"{0}";
                    secondsCounter = 0;
                    TimeTextBox.Text = "00:00";
                }
            }
            Canvas.SetLeft(ballObject.ellipse, Canvas.GetLeft(ballObject.ellipse) + ballObject.distancePerMove);
            Canvas.SetTop(ballObject.ellipse, Canvas.GetTop(ballObject.ellipse) + ballObject.yDistancePerMove);
            Rect ballRect = new Rect(ballObject.GetLocation(), ballObject.GetSize());
            Debug.WriteLine($"BALL'S RECT SIZE: {ballObject.GetSize()}");
            Rect rectRect = new Rect(rectangleObject.GetLocation(), rectangleObject.GetSize());
            Debug.WriteLine($"RECT'S RECT SIZE: {rectangleObject.GetSize()}");
            Rect r = RectHelper.Intersect(ballRect, rectRect);
            if (r.Width > 0 || r.Height > 0)
            {
                ballObject.yDistancePerMove *= -1;
                Debug.WriteLine("Intersect!");
                rectangleObject.BlinkWhite();
                PlaySoundEffectAsync(@"Pong hit.wav");
                PointsTextBox.Text = $"{int.Parse(PointsTextBox.Text) + 1}";
                if (int.Parse(PointsTextBox.Text) > App.CONNECTED_USER.Highscore)
                {
                    App.CONNECTED_USER.Highscore += 1;
                    IncreaseGamescore();
                }
            }
        }
        public static void IncreaseGamescore()
        {
            UserObject user = App.CONNECTED_USER;
            string CreateUserQuery =
                $"UPDATE UsersTable SET Highscore={user.Highscore + 1} " +
                $"WHERE Username='{user.Username}'";
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

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown; ;
        }

        private void CoreWindow_KeyDown(Windows.UI.Core.CoreWindow sender, Windows.UI.Core.KeyEventArgs args)
        {
            double xRectangle = rectangleObject.GetLocation().X;
            double canvasWidthMinusRectWidth = rectangleObject.canvas.ActualWidth - rectangleObject.width;

            if (App.GAME_STOPPED == false && (args.VirtualKey == Windows.System.VirtualKey.Right || args.VirtualKey == Windows.System.VirtualKey.Left) && App.EnablityOfPlayButton == false)
            {
                if (args.VirtualKey == Windows.System.VirtualKey.Right)
                {
                    rectangleObject.distancePerMove = -Math.Abs(rectangleObject.distancePerMove);
                }
                else if (args.VirtualKey == Windows.System.VirtualKey.Left)
                {
                    rectangleObject.distancePerMove = Math.Abs(rectangleObject.distancePerMove);
                }

                if (xRectangle <= LOWER_LIMIT_CANVAS || xRectangle >= UPPER_LIMIT_CANVAS)
                {
                    if (xRectangle <= LOWER_LIMIT_CANVAS)
                    {
                        Canvas.SetLeft(rectangleObject.rectangle, LOWER_LIMIT_CANVAS + 10);
                    }
                    else if (xRectangle >= UPPER_LIMIT_CANVAS)
                    {
                        Canvas.SetLeft(rectangleObject.rectangle, UPPER_LIMIT_CANVAS - 10);
                    }
                    rectangleObject.distancePerMove *= -1;
                }
                else
                {
                    Canvas.SetLeft(rectangleObject.rectangle, Canvas.GetLeft(rectangleObject.rectangle) + rectangleObject.distancePerMove);
                    rectangleCoordinatesTextBox.Text = $"{Canvas.GetLeft(rectangleObject.rectangle)}";
                }

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            App.CURRENT_PAGE_IS_MAINPAGE = true;
            if (App.WAS_USER_BLESSED == false)
            {
                App.PopUp_ContentDialog("נהנית? ספר לחבריך! לא נהנית? ספר לחבריך! למה שתסבול לבד?", "ברכה מהמפתח 🧑‍💻");
                App.WAS_USER_BLESSED = true;
            }
        }
    }
}
