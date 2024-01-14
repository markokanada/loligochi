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
using System.Configuration;
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
        private Envirovment? Envirovment { get; set; } = null;
        private int SaveIndex { get; set; } = 0;

        public MainWindow()
        {
            InitializeComponent(); ;
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
            if (Welcome_Scene.Visibility == Visibility.Visible && e.Key == Key.E && Press_E.Opacity == 1)
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
            Credits_Scene.Visibility = Visibility.Visible;
            Main_Menu_Scene.Visibility = Visibility.Hidden;
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
            Champion.LastSaw = DateTime.Now;
            Envirovment.SerializeEntity(Champion, CurrentSaveName);
            var champ_image = Converter.ConvertFromString(Champion.NormalImage);
            if (champ_image == null) throw new FileMissingException();
            Loaded_Champ_Image.Source = (ImageSource)champ_image;
            Loaded_Champ_Name.Text = Champion.Name;
            LoadTheStatus();
            Envirovment = new Envirovment();
            ChampionStatisticsUpdater();
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
            Champion.LastSaw = DateTime.Now;
            Envirovment.SerializeEntity(Champion, CurrentSaveName);
            Game_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void SaveTheGameFromInGame(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.LastSaw = DateTime.Now;
            Envirovment.SerializeEntity(Champion, CurrentSaveName);
        }

        private void FeedTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.HungerLevel -= 10;
            LoadTheStatus();
        }

        private void HealTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.HP += 10;
            Champion.EntitySicknessLevel -= 20;

            LoadTheStatus();

        }

        private void DrinkTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.ThirstLevel -= 10;
            LoadTheStatus();

        }

        private void PetTheChamp(object sender, RoutedEventArgs e)
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.GotHappy();
            LoadTheStatus();

        }

        private void LoadTheStatus()
        {
            if (Champion == null) throw new ChampIsNullException();
            Champion.SetCurrentStatus();
            Dispatcher.Invoke(() =>
            {
                if (Champion == null) throw new ChampIsNullException();
                #pragma warning disable CS8600 // Converting null literal or possible null value to non-nullable type.
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                #pragma warning disable CS8604 // Possible null reference argument.
                Loaded_Champ_Image.Source = (ImageSource)Converter.ConvertFromString(Champion.GetType().GetProperty(Champion.CurrentStatus + "Image")?.GetValue(Champion).ToString()) ?? throw new WrongChampPropertyException() ;
                var voicePath = (string)Champion.GetType().GetProperty(Champion.CurrentStatus + "Voice")?.GetValue(Champion)?.ToString();
                if (string.IsNullOrWhiteSpace(voicePath))
                {
                    throw new WrongChampPropertyException();
                }

                Loaded_Champ_Sound.Stop();

                Loaded_Champ_Sound.Source = new Uri(voicePath, UriKind.Relative);


                Loaded_Champ_Sound.MediaOpened += (s, e) =>
                {
                        Loaded_Champ_Sound.Play();
                };

                Loaded_Champ_Sound.MediaFailed += (s, e) =>
                {
                    Trace.WriteLine($"Media Failed: {e.ErrorException.Message}");
                };

                Loaded_Champ_Sound.Play();
                #pragma warning restore CS8604 // Possible null reference argument.
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
                #pragma warning restore CS8600 // Converting null literal or possible null value to non-nullable type.
                Loaded_Champ_Name.Text = $"{Champion.Name}";
                Status_Status.Text = $"Status: {Champion.CurrentStatus}";
                Level_Status.Text = $"Level: {Math.Round(Champion.Level)}";
                Age_Status.Text = $"Age: {Math.Round(Champion.Age)}";
                Thirst_Status.Text = $"Thirst: {Math.Round(Champion.ThirstLevel)}";
                Hunger_Status.Text = $"Hunger: {Math.Round(Champion.HungerLevel)}";
                HP_Status.Text = $"HP: {Math.Round(Champion.HP)}";
            });
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
            string directoryPath = "src\\save";
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
            if(Save_Select_Scene_Option_1.Text != "-Empty Save Slot-") { 
            LoadSaveFromSaveSelect(Save_Select_Scene_Option_1.Text);
            
            if (Champion == null) throw new ChampIsNullException();
                Envirovment = new Envirovment();
                Champion = Envirovment.UpdatePetStatusByElapsedTime(Champion);
                ChampionStatisticsUpdater();
                Load_Game_Scene.Visibility = Visibility.Hidden;
                LoadTheStatus();
                Game_Scene.Visibility = Visibility.Visible;
            }
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
            else
            {
                Save_Select_Scene_Option_1.Text = "-Empty Save Slot-";
            }
            
        }
        #endregion

        #region Credits Scene Logic
        private void BackToTheMainMenuFromCreditsScene(object sender, RoutedEventArgs e)
        {
            Credits_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
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

        private void ChampionStatisticsUpdater()
        {
            Task.Delay(60000).ContinueWith((t) =>
            {
                if(Game_Scene.Visibility == Visibility.Visible) { 
                if (Champion == null) throw new ChampIsNullException();
                if (Envirovment == null) throw new EnvirovmentIsNullException();
                Champion = Envirovment.UpdatePetStatus(Champion);
                if (Champion == null) throw new ChampIsNullException();
                LoadTheStatus();
                ChampionStatisticsUpdater();
                }
            });
        }

        #endregion
    }
}