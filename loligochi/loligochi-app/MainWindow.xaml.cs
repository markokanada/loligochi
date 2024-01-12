using System.IO;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using loligochi_classlib;
using System.Diagnostics;
using static System.Net.Mime.MediaTypeNames;
namespace loligochi_app
{
    public partial class MainWindow : Window
    {
        DispatcherTimer WelcomeSoundTimer = new DispatcherTimer();
        DispatcherTimer BackgroundMusicTimer = new DispatcherTimer();
        string CurrentSaveName = "";
        ImageSourceConverter Converter = new ImageSourceConverter();
        private int ChampIndex { get; set; } = 2;
        private static string[] AllChampionJsonFiles = Directory.GetFiles("src/jsons/default-champion-datas/", "*.json").OrderBy(name => name).ToArray();
        private Entity? Champion { get; set; } = null;
        private int SaveIndex { get; set; } = 0;

        public MainWindow()
        {
            InitializeComponent();
            Welcome_Scene.Visibility = Visibility.Visible;
            WelcomeSoundTimer.Interval = TimeSpan.FromSeconds(5);
            WelcomeSoundTimer.Tick += WelcomeSoundTimerTick!;
            WelcomeSoundTimer.Start();
            BackgroundMusicTimer.Interval = TimeSpan.FromSeconds(9);
            BackgroundMusicTimer.Tick += BackgroundMusicTimerTick!;
            BackgroundMusicTimer.Start();

        }
        #region Welcome Scene logic
        private void WelcomeSoundTimerTick(object sender, EventArgs e)
        {
            WelcomeSoundTimer.Stop();
            welcome_sound.Play();
        }

        public void WindowKeyDown(object sender, KeyEventArgs e)
        {
            if (Welcome_Scene.Visibility == Visibility.Visible && e.Key == Key.E)
            {
                Welcome_Scene.Visibility = Visibility.Hidden;
                Main_Menu_Scene.Visibility = Visibility.Visible;
            }

            if (Champ_Select_Scene.Visibility == Visibility.Visible && !Name_Of_The_Champ.IsFocused && (e.Key == Key.A || e.Key == Key.D))
            {
                if (e.Key == Key.A)
                {
                    if (ChampIndex == 0)
                    {
                        ChampIndex = 4;
                    }
                    else
                    {
                        ChampIndex--;
                    }
                }
                else if (e.Key == Key.D)
                {
                    if (ChampIndex == 4)
                    {
                        ChampIndex = 0;
                    }
                    else
                    {
                        ChampIndex++;
                    }
                }
                LoadChampInChampSelectPreview();
            }
        }

        #endregion

        #region Main Menu Scene logic
        private void NewGameButtonClick(object sender, RoutedEventArgs e)
        {
            New_Game_Menu.Visibility = Visibility.Visible;
            Main_Menu_Scene.Visibility = Visibility.Hidden;
        }
        private void ContinueGameButtonClick(object sender, RoutedEventArgs e)
        {
            InitSaveSlots();
            Main_Menu_Scene.Visibility = Visibility.Hidden;
            Load_Game_Scene.Visibility = Visibility.Visible;
        }
        private void SettingsButtonClick(object sender, RoutedEventArgs e)
        {
            Settings_Scene.Visibility = Visibility.Visible;
            Main_Menu_Scene.Visibility = Visibility.Hidden;
        }

        private void CreditsButtonClick(object sender, RoutedEventArgs e)
        {

        }
        private void ExitButtonClick(object sender, RoutedEventArgs e)
        {
            Sure_To_Exit_Scene.Visibility = Visibility.Visible;
            Main_Menu_Scene.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Sure To Exit Scene logic
        private void ExitButtonBackToMainMenu(object sender, RoutedEventArgs e)
        {
            Sure_To_Exit_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void ExitButtonSureToExit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region Start a New Game Scene logic
        private void SaveNameStartEditing(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Save.Text = "";
        }



        private void SaveNameStopEditing(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Save.Text == "")
            {
                Name_Of_The_Save.Text = "Write the name of the save here.";
            }
        }

        private void ContinueToChampSelect(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Save.Text == "Write the name of the save here.")
            {
                CurrentSaveName = $"-.d-.{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}";
            }
            else
            {
                CurrentSaveName = $"{Name_Of_The_Save.Text}-.d-.{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}";
            }
            Name_Of_The_Save.Text = "Write the name of the save here.";
            LoadChampInChampSelectPreview();
            New_Game_Menu.Visibility = Visibility.Hidden;
            Champ_Select_Scene.Visibility = Visibility.Visible;


        }

        private void StartANewGameSceneBackToTheMainMenu(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Save.Text = "Write the name of the save here.";
            New_Game_Menu.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        #endregion

        #region Champ Select Scene logic

        private void LeftArrow(object sender, RoutedEventArgs e)
        {
            if (ChampIndex == 0)
            {
                ChampIndex = 4;
            }
            else
            {
                ChampIndex--;
            }

            LoadChampInChampSelectPreview();
        }

        private void RightArrow(object sender, RoutedEventArgs e)
        {
            if (ChampIndex == 4)
            {
                ChampIndex = 0;
            }
            else
            {
                ChampIndex++;
            }
            LoadChampInChampSelectPreview();
        }

        private void ChampNameStartEditing(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Champ.Text = "";
        }



        private void ChampNameStopEditing(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Champ.Text == "" && Champion != null)
            {
                Name_Of_The_Champ.Text = Champion.Name;
            }
            else if (Champion == null) throw new ChampIsNullException();
        }

        private void ContinueToGame(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.Name = Name_Of_The_Champ.Text;
            Envirovment.SerializeEntity(Champion, CurrentSaveName);
            var champ_image = Converter.ConvertFromString(Champion.NormalImage);
            if (champ_image == null) throw new FileMissingException();
            Loaded_Champ_Image.Source = (ImageSource)champ_image;
            Loaded_Champ_Name.Text = Champion.Name;
            LoadTheStatus();
            Champ_Select_Scene.Visibility = Visibility.Hidden;
            Game_Scene.Visibility = Visibility.Visible;
        }

        private void ChampSelectSceneBackToTheMainMenu(object sender, RoutedEventArgs e)
        {
            Champ_Select_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        #endregion

        #region In Game Scene Logic


        private void SaveAndExitFromTheInGameScene(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Envirovment.SerializeEntity(Champion, CurrentSaveName);
            Game_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void SaveTheGameFromInGame(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Envirovment.SerializeEntity(Champion, CurrentSaveName);
        }

        private void FeedTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.HungerLevel -= 10;
        }

        private void HealTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.HP += 10;
        }

        private void DrinkTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.ThirstLevel -= 10;
        }

        private void PetTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.GotHappy();
        }

        private void LoadTheStatus()
        {
            if (Champion == null) throw new ChampIsNullException();
            Loaded_Champ_Name.Text = $"{Champion.Name}";
            Status_Status.Text = $"Status: {Champion.CurrentStatus}";
            Level_Status.Text = $"Level: {Math.Round(Champion.Level, 1)}";
            Age_Status.Text = $"Age: {Math.Round(Champion.Age, 1)}";
            Thirst_Status.Text = $"Thirst: {Math.Round(Champion.ThirstLevel, 1)}";
            Hunger_Status.Text = $"Hunger: {Math.Round(Champion.HungerLevel, 1)}";
            HP_Status.Text = $"HP: {Math.Round(Champion.HP, 1)}";
        }

        #endregion

        #region Settings Scene Logic

        private void BackToTheMainMenuFromSettingsScene(object sender, RoutedEventArgs e)
        {
            Settings_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void RemoveAllTheSaves(object sender, RoutedEventArgs e)
        {
            string directoryPath = "src/saves";

            if (Directory.Exists(directoryPath))
            {
                string[] files = Directory.GetFiles(directoryPath);

                foreach (string file in files)
                {
                    try
                    {
                        File.Delete(file);
                        Console.WriteLine($"Deleted file: {file}");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error deleting file {file}: {ex.Message}");
                    }
                }
            }
            else
            {
                Console.WriteLine($"The directory {directoryPath} does not exist.");
            }
        }
        #endregion

        #region Load Saved Game logic


        private void BackToTheMainMenuFromLoadGameScene(object sender, RoutedEventArgs e)
        {
            SaveIndex = 0;
            Load_Game_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void SaveSelectOption1Clicked(object sender, RoutedEventArgs e)
        {
            LoadSaveFromSaveSelect(Save_Select_Scene_Option_1.Text);
            LoadTheStatus();
            Load_Game_Scene.Visibility = Visibility.Hidden;
            Game_Scene.Visibility = Visibility.Visible;
        }


        private void RotateTheSaveSlotsByArrowDown(object sender, RoutedEventArgs e)
        {
            List<String> avaibleSaves = Envirovment.GetAvaibleSaves();
            if (avaibleSaves.Count != SaveIndex+1 && avaibleSaves.Count != 0)
            {
                SaveIndex++;
                Save_Select_Scene_Option_1.Text = avaibleSaves[SaveIndex].Substring(avaibleSaves[SaveIndex].IndexOf("save\\") + 5);
            }
            Down_Arrow_Save_Select.IsEnabled = false;
            var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1200) };
            timer.Tick += (sender, args) =>
            {
                Down_Arrow_Save_Select.IsEnabled = true;
                timer.Stop();
            };
            timer.Start();
        }

        private void RotateTheSaveSlotsByArrowUp(object sender, RoutedEventArgs e)
        {
            List<String> avaibleSaves = Envirovment.GetAvaibleSaves();

            if (SaveIndex != 0 && avaibleSaves.Count != 0)
            {
                SaveIndex--;
                Save_Select_Scene_Option_1.Text = avaibleSaves[SaveIndex].Substring(avaibleSaves[SaveIndex].IndexOf("save\\") + 5);
            }
            Up_Arrow_Save_Select.IsEnabled = false;
            var timer = new System.Windows.Threading.DispatcherTimer { Interval = TimeSpan.FromMilliseconds(1200) };
            timer.Tick += (sender, args) =>
            {
                Up_Arrow_Save_Select.IsEnabled = true;
                timer.Stop();
            };
            timer.Start();
        }
        private void InitSaveSlots()
        {
            List<String> avaibleSaves = Envirovment.GetAvaibleSaves();

            if (avaibleSaves.Count >= 1)
            {
                Save_Select_Scene_Option_1.Text = avaibleSaves[0].Substring(avaibleSaves[0].IndexOf("save\\") + 5);
            }
            
        }
        #endregion
        #region Other function & event handlers which used at more places.
        private void LoadSaveFromSaveSelect(string saveName)
        {
            CurrentSaveName = "src\\save\\" + saveName;
            Champion = Envirovment.DeserializeEntity(CurrentSaveName);
            if (Champion == null) throw new ChampIsNullException();
            var champ_image = Converter.ConvertFromString(Champion.NormalImage);
            if (champ_image == null) throw new FileMissingException();
            Loaded_Champ_Image.Source = (ImageSource)champ_image;
            Loaded_Champ_Name.Text = Champion.Name;
        }


        private void LoadChampInChampSelectPreview()
        {
            Champion = Envirovment.DeserializeEntity(AllChampionJsonFiles[ChampIndex]);
            if (Champion == null) throw new ChampIsNullException();
            var champ_image = Converter.ConvertFromString(Champion.NormalImage);
            if (champ_image == null) throw new FileMissingException();
            Champ_Image_On_Champ_Select.Source = (ImageSource)champ_image;
            Name_Of_The_Champ.Text = Champion.BasedOn;
        }

        private void SaveChamp()
        {
            if (Champion == null) throw new ChampIsNullException();
            Envirovment.SerializeEntity(Champion, CurrentSaveName); //After continue TODO load the current_Save_name
        }

        private void BackgroundMusicTimerTick(object sender, EventArgs e)
        {
            BackgroundMusicTimer.Stop();
            background_music.Play();
        }

        private void ButtonHoverSoundEffect(object sender, MouseEventArgs e)
        {
            button_hover.Stop();
            button_hover.Position = TimeSpan.Zero;
            button_hover.Play();
        }

        #endregion
    }
}