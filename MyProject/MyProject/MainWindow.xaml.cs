using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace MyProject
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MediaPlayer player;
        public MainWindow()
        {
            InitializeComponent();
            player = new MediaPlayer();
            player.Volume = slider_volume.Value / 100;

            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/mainback.png")); // фон окна
            background.Viewport = new Rect(0, 0, 1, 1); // положение и размер фона
            this.Background = background;
        }

        private void btn_game_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
        }

        private void btn_close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void btn_start_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            player.Open(new Uri("doom_11. Bfg Division.mp3", UriKind.Relative));
            player.Play();
        }

        private void btn_pause_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            player.Pause();
        }

        private void slider_volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (player != null)
            {
                player.Volume = slider_volume.Value / 100;
            }
        }
    }
}
