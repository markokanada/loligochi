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
using static System.Net.Mime.MediaTypeNames;

namespace loligochi_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer welcome_sound_timer = new DispatcherTimer();
        DispatcherTimer background_music_timer = new DispatcherTimer();
        string current_save_name = "";

        private int champ_index { get; set; } = 2;
        private static string[] allChampionJsonFiles = Directory.GetFiles("src/jsons/default-champion-datas/", "*.json").OrderBy(name => name).ToArray();
        private Entity champ { get; set; } = null;
        public MainWindow()
        {
            InitializeComponent();

            Welcome_Scene.Visibility = Visibility.Visible;

            welcome_sound_timer.Interval = TimeSpan.FromSeconds(5);
            welcome_sound_timer.Tick += Welcome_Sound_Timer_Tick;
            welcome_sound_timer.Start();


            background_music_timer.Interval = TimeSpan.FromSeconds(9);
            background_music_timer.Tick += Background_Music_Timer_Tick;
            background_music_timer.Start();
        }
        #region Welcome_scene
        private void Welcome_Sound_Timer_Tick(object sender, EventArgs e)
        {
            welcome_sound_timer.Stop();

            welcome_sound.Play();
        }

        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Welcome_Scene.Visibility == Visibility.Visible && e.Key == Key.E)
            {
                Welcome_Scene.Visibility = Visibility.Hidden;
                Main_Menu_Scene.Visibility = Visibility.Visible;
                
            }

            if(Champ_Select_Scene.Visibility == Visibility.Visible && !Name_Of_The_Champ.IsFocused && (e.Key == Key.A || e.Key == Key.D))
            {
                if(e.Key == Key.A)
                {
                    if (champ_index == 0)
                    {
                        champ_index = 4;
                    }
                    else
                    {
                        champ_index--;
                    }

                    
                }
                else if(e.Key == Key.D)
                {
                    if (champ_index == 4)
                    {
                        champ_index = 0;
                    }
                    else
                    {
                        champ_index++;
                    }

                    
                }
                Load_Champ();
            }
        }

        #endregion

        #region Main_Menu_Scene
        private void New_Game_Button_Click(object sender, RoutedEventArgs e)
        {
            New_Game_Menu.Visibility = Visibility.Visible;
            Main_Menu_Scene.Visibility = Visibility.Hidden;
        }
        private void Continue_Game_Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Settings_Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Credits_Button_Click(object sender, RoutedEventArgs e)
        {

        }
        private void Exit_Button_Click(object sender, RoutedEventArgs e)
        {
            Sure_To_Exit_Scene.Visibility = Visibility.Visible;
            Main_Menu_Scene.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Sure to exit Scene
        private void Exit_Button_Back_To_Main_Menu(object sender, RoutedEventArgs e)
        {
            Sure_To_Exit_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void Exit_Button_Sure_To_Exit(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        #endregion

        #region Start_A_New_Game_Scene
        private void Save_Name_Start_Editing(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Save.Text = "";
        }



        private void Save_Name_Stop_Editing(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Save.Text == "")
            {
                Name_Of_The_Save.Text = "Write the name of the save here.";
            }
        }

        private void Continue_To_Champ_Select(object sender, MouseButtonEventArgs e)
        {
            if (Name_Of_The_Save.Text == "Write the name of the save here.")
            {
                current_save_name = $"-.d-.{DateTime.Now.ToString("yyyy-MM-dd-HH:mm")}";
            }
            else
            {
                current_save_name = $"{Name_Of_The_Save.Text}-.d-.{DateTime.Now.ToString("yyyy-MM-dd-HH:mm")}";
            }
            Load_Champ();
            New_Game_Menu.Visibility = Visibility.Hidden;
            Champ_Select_Scene.Visibility = Visibility.Visible;


        }

        private void Start_A_New_Game_Scene_Back_To_The_Main_Menu(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Save.Text = "Write the name of the save here.";
            New_Game_Menu.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        #endregion

        #region Champ_Select_Scene


        
        private void Load_Champ()
        {
         string jsonFilePath = allChampionJsonFiles[champ_index];
         string jsonString = File.ReadAllText(jsonFilePath);
         champ = JsonSerializer.Deserialize<Entity>(jsonString);

        }

        private void Left_Arrow(object sender, MouseButtonEventArgs e)
        {
            if(champ_index == 0)
            {
                champ_index = 4;
            }
            else {
                champ_index--;
            }

            Load_Champ();
        }

        private void Right_Arrow(object sender, MouseButtonEventArgs e)
        {
            if (champ_index == 4)
            {
                champ_index = 0;
            }
            else
            {
                champ_index++;
            }

            Load_Champ();
        }

        private void Champ_Name_Start_Editing(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Champ.Text = "";
        }



        private void Champ_Name_Stop_Editing(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Champ.Text == "")
            {
                Name_Of_The_Champ.Text = champ.name;
            }
        }

        private void Continue_To_Game(object sender, MouseButtonEventArgs e)
        {

            Champ_Select_Scene.Visibility = Visibility.Hidden;
            Game_Scene.Visibility = Visibility.Visible;
        }

        private void Champ_Select_Game_Scene_Back_To_The_Main_Menu(object sender, RoutedEventArgs e)
        {
            Champ_Select_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        #endregion

        #region Other multiple asset scenes

        private void Background_Music_Timer_Tick(object sender, EventArgs e)
        {
            background_music_timer.Stop();

            background_music.Play();
        }

        private void Button_Hover_SoundEffect(object sender, MouseEventArgs e)
        {
            button_hover.Stop();
            button_hover.Position = TimeSpan.Zero;
            button_hover.Play();
        }

        #endregion

    }
}