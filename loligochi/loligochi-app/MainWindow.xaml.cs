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
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 

    public partial class MainWindow : Window
    {
        DispatcherTimer welcome_sound_timer = new DispatcherTimer();
        DispatcherTimer background_music_timer = new DispatcherTimer();
        string current_save_name = "";
        ImageSourceConverter converter = new ImageSourceConverter();

        private int champ_index { get; set; } = 2;
        private static string[] allChampionJsonFiles = Directory.GetFiles("src/jsons/default-champion-datas/", "*.json").OrderBy(name => name).ToArray();
        private Entity? champ { get; set; } = null;

        private int save_index { get; set; } = 0;
        public MainWindow()
        {
            InitializeComponent();
            Welcome_Scene.Visibility = Visibility.Visible;

            welcome_sound_timer.Interval = TimeSpan.FromSeconds(5);
            welcome_sound_timer.Tick += Welcome_Sound_Timer_Tick!;
            welcome_sound_timer.Start();


            background_music_timer.Interval = TimeSpan.FromSeconds(9);
            background_music_timer.Tick += Background_Music_Timer_Tick!;
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
                Load_Champ_In_Champ_Select_Preview();
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
            Init_Save_Slots();
            Main_Menu_Scene.Visibility = Visibility.Hidden;
            Load_Game_Scene.Visibility = Visibility.Visible;
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

        private void Continue_To_Champ_Select(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Save.Text == "Write the name of the save here.")
            {
                current_save_name = $"-.d-.{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}";
            }
            else
            {
                current_save_name = $"{Name_Of_The_Save.Text}-.d-.{DateTime.Now.ToString("yyyy-MM-dd-HH-mm")}";
            }
            Name_Of_The_Save.Text = "Write the name of the save here.";
            Load_Champ_In_Champ_Select_Preview();
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

        private void Left_Arrow(object sender, RoutedEventArgs e)
        {
            if(champ_index == 0)
            {
                champ_index = 4;
            }
            else {
                champ_index--;
            }

            Load_Champ_In_Champ_Select_Preview();
        }

        private void Right_Arrow(object sender, RoutedEventArgs e)
        {
            if (champ_index == 4)
            {
                champ_index = 0;
            }
            else
            {
                champ_index++;
            }
            Load_Champ_In_Champ_Select_Preview();
        }

        private void Champ_Name_Start_Editing(object sender, RoutedEventArgs e)
        {
            Name_Of_The_Champ.Text = "";
        }



        private void Champ_Name_Stop_Editing(object sender, RoutedEventArgs e)
        {
            if (Name_Of_The_Champ.Text == "" && champ != null)
            {
                Name_Of_The_Champ.Text = champ.name;
            }
            else if(champ == null) throw new ChampIsNullException();


        }

        private void Continue_To_Game(object sender, RoutedEventArgs e)
        {
            champ.name = Name_Of_The_Champ.Text;
            Envirovment.SerializeEntity(champ, current_save_name);

            var champ_image = converter.ConvertFromString(champ.normalImage);

            if (champ_image == null) throw new FileMissingException();
            Loaded_Champ_Image.Source = (ImageSource)champ_image;
            Loaded_Champ_Name.Text = champ.name;

            Champ_Select_Scene.Visibility = Visibility.Hidden;
            Game_Scene.Visibility = Visibility.Visible;
        }

        private void Champ_Select_Scene_Back_To_The_Main_Menu(object sender, RoutedEventArgs e)
        {
            Champ_Select_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        #endregion

        #region In_Game_Scene


        private void Save_And_Exit_From_The_In_Game_Scene(object sender, RoutedEventArgs e)
        {
            Envirovment.SerializeEntity(champ, current_save_name);
            Game_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void Save_The_Game_From_In_Game(object sender, RoutedEventArgs e)
        {
            Envirovment.SerializeEntity(champ, current_save_name);
        }

        private void Feed_The_Champ(object sender, RoutedEventArgs e)
        {
            champ.hungerLevel -= 10;
        }

        private void Heal_The_Champ(object sender, RoutedEventArgs e)
        {

        }

        private void Drink_The_Champ(object sender, RoutedEventArgs e)
        {

        }

        private void Pet_The_Champ(object sender, RoutedEventArgs e)
        {

        }

        private void LoadTheStatus()
        {
            Loaded_Champ_Name.Text = champ.name;
            Status_Status.Text = champ.currentStatus;
            Level_Status.Text = $"{Math.Round(champ.level,1)}";
            Age_Status.Text = $"{Math.Round(champ.age, 1)}";
            Thirst_Status.Text = $"{Math.Round(champ.thirstLevel, 1)}";
            Hunger_Status.Text = $"{Math.Round(champ.hungerLevel, 1)}";
            HP_Status.Text = $"{Math.Round(champ.hp, 1)}";
        }

        #endregion

        #region Load_Saved_Game


        private void Back_To_The_Main_Menu_From_Load_Game_Scene(object sender, RoutedEventArgs e)
        {
            save_index = 0;
            Load_Game_Scene.Visibility = Visibility.Hidden;
            Main_Menu_Scene.Visibility = Visibility.Visible;
        }

        private void Save_Select_Option_1_Clicked (object sender, RoutedEventArgs e)
        {
            if (Save_Select_Scene_Option_1.IsEnabled)
            {
                Load_Save_From_Save_Select(Save_Select_Scene_Option_1.Text);
            }
        }


        private void Rotate_The_Save_Slots_By_Arrow_Down(object sender, RoutedEventArgs e)
        {

            List<String> avaibleSaves = new List<String>();

            if (!Directory.Exists("src"))
            {
                throw new FileMissingException();
            }
            else if (!Directory.Exists("src/save"))
            {
                avaibleSaves = [];

            }
            else
            {
                avaibleSaves = Directory.GetFiles("src/save").Order().ToList();
            }

            if (save_index != 0 && avaibleSaves.Count != save_index + 1)
            {

                save_index++;
                Save_Select_Scene_Option_1.Text = avaibleSaves[save_index].Substring(avaibleSaves[save_index].IndexOf("save\\") + 5);
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

        private void Rotate_The_Save_Slots_By_Arrow_Up(object sender, RoutedEventArgs e)
        {

            List<String> avaibleSaves = new List<String>();

            if (!Directory.Exists("src"))
            {
                throw new FileMissingException();
            }
            else if (!Directory.Exists("src/save"))
            {
                avaibleSaves = [];

            }
            else
            {
                avaibleSaves = Directory.GetFiles("src/save").Order().ToList();
            }
            
            if(save_index != 0 )
            {

                save_index--;
                Save_Select_Scene_Option_1.Text = avaibleSaves[save_index].Substring(avaibleSaves[save_index].IndexOf("save\\") + 5);
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

        private void Init_Save_Slots()
        {
            List<String> avaibleSaves = new List<String>();

            if (!Directory.Exists("src"))
            {
                throw new FileMissingException();
            }
            else if (!Directory.Exists("src/save"))
            {
                avaibleSaves = [];

            }
            else
            {
                avaibleSaves = Directory.GetFiles("src/save").Order().ToList();
            }
            
            if (avaibleSaves.Count >= 1)
            {

                Save_Select_Scene_Option_1.Text = avaibleSaves[0].Substring(avaibleSaves[0].IndexOf("save\\")+5);
            }
            save_index += 1;
        }


        #endregion

        #region Other function & event handlers which used at more places.

        
        private void Load_Save_From_Save_Select(string saveName)
        {
            current_save_name = "save\\" + saveName;
            champ = Envirovment.DeserializeEntity(current_save_name);
            var champ_image = converter.ConvertFromString(champ.normalImage);


            if (champ_image == null) throw new FileMissingException();
            Loaded_Champ_Image.Source = (ImageSource)champ_image;
            Loaded_Champ_Name.Text = champ.name;
        }

       
        private void Load_Champ_In_Champ_Select_Preview()
        {
            champ = Envirovment.DeserializeEntity(allChampionJsonFiles[champ_index]);
            var champ_image = converter.ConvertFromString(champ.normalImage);
            

            if (champ_image == null) throw new FileMissingException();
            Champ_Image_On_Champ_Select.Source = (ImageSource)champ_image;
            Name_Of_The_Champ.Text = champ.basedOn;
        }

        private void Save_Champ()
        {
            if (champ == null) throw new ChampIsNullException();
            Envirovment.SerializeEntity(champ, current_save_name); //After continue TODO load the current_Save_name
        }

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