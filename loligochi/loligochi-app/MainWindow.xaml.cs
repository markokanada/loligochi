using System.Text;
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

namespace loligochi_app
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer welcome_sound_timer = new DispatcherTimer();
        DispatcherTimer background_music_timer = new DispatcherTimer();

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

        private void Welcome_Sound_Timer_Tick(object sender, EventArgs e)
        {
            welcome_sound_timer.Stop();

            welcome_sound.Play();
        }

        private void Background_Music_Timer_Tick(object sender, EventArgs e)
        {
            background_music_timer.Stop();

            background_music.Play();
        }


        public void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (Welcome_Scene.Visibility == Visibility.Visible && e.Key == Key.E)
            {
                Welcome_Scene.Visibility = Visibility.Hidden;
                Main_Menu_Scene.Visibility = Visibility.Visible;
                
            }
        }


        private void New_Game_Button_Click(object sender, RoutedEventArgs e)
        {

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

        }

        private void Button_Hover_SoundEffect(object sender, MouseEventArgs e)
        {
            button_hover.Stop();
            button_hover.Position = TimeSpan.Zero;
            button_hover.Play();
        }
    }
}