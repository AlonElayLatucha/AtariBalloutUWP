using System;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using AtariBallout.Model;
using AtariBallout.Services;
using AtariBallout.Views;
using Windows.ApplicationModel.Activation;
using Windows.ApplicationModel.Core;
using Windows.Media.SpeechSynthesis;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace AtariBallout
{
    public sealed partial class App : Application
    {
        public static MediaElement mediaplayer = new MediaElement();

        public static string allitem { get; set; }

        public static bool WAS_USER_BLESSED = false;

        public static UserObject CONNECTED_USER; // CONNECTED USER

        public static BoardType GAME_BOARD_TYPE;
        public static bool GAME_BOARD_TYPE_CHANGED = false;
        public static bool USER_DATA_CHANGED = false;

        public static bool CURRENT_PAGE_IS_MAINPAGE = false;

        public static bool GAME_STOPPED = true;

        public static bool EnablityOfPlayButton { get; set; }

        public const string CONNECTION_STRING = @"Data Source=ALONHP\ALONSQLSERVER; Initial Catalog=AtariBalloutDB; Integrated Security=True";

        private Lazy<ActivationService> _activationService;
        private ActivationService ActivationService
        {
            get { return _activationService.Value; }
        }

        public App()
        {
            InitializeComponent();
            UnhandledException += OnAppUnhandledException;

            // Deferred execution until used. Check https://docs.microsoft.com/dotnet/api/system.lazy-1 for further info on Lazy<T> class.
            _activationService = new Lazy<ActivationService>(CreateActivationService);
        }

        public static async void readText(string mytext)
        {
            using (var speech = new SpeechSynthesizer())
            {
                speech.Voice = SpeechSynthesizer.AllVoices.First(gender => gender.Gender == VoiceGender.Male);
                string ssml = @"<speak version='1.0' " + "xmlns='http://www.w3.org/2001/10/synthesis' xml:lang='en-US'>" + allitem + "</speak>";
                SpeechSynthesisStream stream = await speech.SynthesizeSsmlToStreamAsync(ssml);
                mediaplayer.SetSource(stream, stream.ContentType);
                mediaplayer.Play();
            }
        }

        public static string RemoveRedundantSpaces(string text)
        {
            return Regex.Replace(text, @"\s+", " ").TrimEnd().TrimStart();
        }

        public static HashSalt GenerateSaltedHash(int size, string password)
        {
            var saltBytes = new byte[size];
            var provider = new RNGCryptoServiceProvider();
            provider.GetNonZeroBytes(saltBytes);
            var salt = Convert.ToBase64String(saltBytes);

            var rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, saltBytes, 10000);
            var hashPassword = Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256));

            HashSalt hashSalt = new HashSalt(hashPassword, salt);
            return hashSalt;
        }

        public static string GetStringSha256Hash(string text)
        {
            if (String.IsNullOrEmpty(text))
                return String.Empty;

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(text);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static UserObject GetUserByUsername(string Username)
        {
            string GetUsersQuery =
               "SELECT * " +
               "FROM UsersTable " +
               $"WHERE Username='{Username}'";
            var userObj = new UserObject();
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
                                    userObj.Username = App.RemoveRedundantSpaces(reader.GetString(0));
                                    userObj.Hash = App.RemoveRedundantSpaces(reader.GetString(1));
                                    userObj.Salt = App.RemoveRedundantSpaces(reader.GetString(2));
                                    userObj.FirstName = App.RemoveRedundantSpaces(reader.GetString(3));
                                    userObj.LastName = App.RemoveRedundantSpaces(reader.GetString(4));
                                    userObj.Highscore = reader.GetInt32(5);
                                }
                            }
                        }
                    }
                }
                return userObj;
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return null;
        }

        public static bool UserExistenceCheck(string Username)
        {
            string CreateUserQuery =
                "SELECT TOP 1 * FROM UsersTable WHERE" +
                $"Username={Username}";
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
                            Debug.WriteLine("User check - exists");
                            return true;
                        }
                        else
                        {
                            Debug.WriteLine("User check - doesn't exist");
                        }
                    }
                }
            }
            catch (Exception eSql)
            {
                Debug.WriteLine("Exception: " + eSql.Message);
            }
            return false;
        }



        public static bool VerifyUserAuth(string enteredPassword, string storedHash, string storedSalt)
        {
            try
            {
                var saltBytes = Convert.FromBase64String(storedSalt);
                var rfc2898DeriveBytes = new Rfc2898DeriveBytes(enteredPassword, saltBytes, 10000);
                return Convert.ToBase64String(rfc2898DeriveBytes.GetBytes(256)) == storedHash;
            }
            catch (Exception e)
            {
                Popup_UserDoesntExist_MessageDialog();
            }
            return false;
        }

        public static void Popup_UserDoesntExist_MessageDialog()
        {
            PopUp_ContentDialog("אופס! המשתמש לא קיים", "טעות קלה");
        }

        public static void PopUp_ContentDialog(object text, string title)
        {
            ContentDialog dialog = new ContentDialog()
            {
                Content = text,
                FontFamily = new Windows.UI.Xaml.Media.FontFamily("Assistant"),
                Title = title,
                CloseButtonText = "סבבה 👍",
                FlowDirection = FlowDirection.RightToLeft
            };
            // Show the message dialog
            _ = dialog.ShowAsync();
        }


        protected override async void OnLaunched(LaunchActivatedEventArgs args)
        {
            if (!args.PrelaunchActivated)
            {
                await ActivationService.ActivateAsync(args);
            }
            // Hide default title bar.
            var coreTitleBar = CoreApplication.GetCurrentView().TitleBar;
            coreTitleBar.ExtendViewIntoTitleBar = true;
        }

        protected override async void OnActivated(IActivatedEventArgs args)
        {
            await ActivationService.ActivateAsync(args);
        }

        private void OnAppUnhandledException(object sender, Windows.UI.Xaml.UnhandledExceptionEventArgs e)
        {
            // TODO WTS: Please log and handle the exception as appropriate to your scenario
            // For more info see https://docs.microsoft.com/uwp/api/windows.ui.xaml.application.unhandledexception
        }

        private ActivationService CreateActivationService()
        {
            return new ActivationService(this, typeof(Views.StartScreenPage), new Lazy<UIElement>(CreateShell));
        }

        private UIElement CreateShell()
        {
            return new Views.ShellPage();
        }
    }
}
