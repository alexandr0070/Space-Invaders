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
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MyProject
{
    /// <summary>
    /// Логика взаимодействия для GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        Random random = new Random();
        Random heightrandom = new Random();
        Random widthrandom = new Random();
        Random randpos = new Random();
        Random randspawn = new Random();

        List<Rectangle> trash = new List<Rectangle>(); // мусорка для объектов(пульки, враги)
        Rect PlayerHitbox; // хитбокс игрока
        DispatcherTimer timer = new DispatcherTimer(); //таймер для логики игры

        bool moveleft, moveright;
        int EnemyColorCounter = 0, EnemyHeightCounter = 0, EnemyWidthCounter = 0;
        int EnemySpawnTime = 100; // время спавна врагов
        int PlayerSpeed = 10, EnemySpeed = 5; // скорость передвижения
        int Damage = 0, Score = 0; // полученный урон и счётчик очков

        public GameWindow()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromMilliseconds(10);
            timer.Tick += Logic; // такты работы логики игры
            timer.Start();
            MyCanvas.Focus(); // пользователь работает только с холстом

            ImageBrush background = new ImageBrush();
            background.ImageSource = new BitmapImage(new Uri("pack://application:,,,/img/background.png")); // фон игрового окна
            background.Viewport = new Rect(0, 0, 1, 1); // положение и размер фона
            MyCanvas.Background = background;
        }

        public void Logic(object sender, EventArgs e) // сама логика игры
        {
            PlayerHitbox = new Rect(Canvas.GetLeft(player), Canvas.GetTop(player), player.Width, player.Height); // создание хитбокса игрока
            EnemySpawnTime -= 1;
            scorestatus.Content = "Очки: " + Score;
            damagestatus.Content = "Ущерб: " + Damage;

            if (EnemySpawnTime < 0)
            {
                EnemiesConstruct();
                EnemySpawnTime = randspawn.Next(30, 75);
            }
            // движение игрока
            if (moveleft == true && Canvas.GetLeft(player) > 0)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) - PlayerSpeed);
            }
            if (moveright == true && Canvas.GetLeft(player) + 70 < 600)
            {
                Canvas.SetLeft(player, Canvas.GetLeft(player) + PlayerSpeed);
            }

            foreach (var i in MyCanvas.Children.OfType<Rectangle>())
            {
                if (i is Rectangle && (string)i.Tag == "bullet") // достаёт объект по тегу bullet
                {
                    Canvas.SetTop(i, Canvas.GetTop(i) - 30); // движение пульки
                    Rect BulletHitbox = new Rect(Canvas.GetLeft(i), Canvas.GetTop(i), i.Width, i.Height); // хитбокс пульки
                    if (Canvas.GetTop(i) < 0) // добавление объекта в мусорку
                    {
                        trash.Add(i);
                    }

                    foreach (var h in MyCanvas.Children.OfType<Rectangle>())
                    {
                        if (h is Rectangle && (string)h.Tag == "enemy") // проверка на попадание. Если пулька касается с объектом, имеющим тег enemy
                        {
                            Rect Hit = new Rect(Canvas.GetLeft(h), Canvas.GetTop(h), h.Width, h.Height);
                            if (BulletHitbox.IntersectsWith(Hit))
                            {
                                trash.Add(i); trash.Add(h);
                                if (EnemyColorCounter == 1) Score++;
                                if (EnemyColorCounter == 2) Score += 2;
                                if (EnemyColorCounter == 3) Score += 3;
                                if (EnemyColorCounter == 4) Score += 4;
                                if (EnemyColorCounter == 5) Score += 5;
                                if (EnemyColorCounter == 6) Score += 6;
                            }
                        }
                    }
                }
                if (i is Rectangle && (string)i.Tag == "enemy") // достаёт объект по тегу enemy
                {
                    Canvas.SetTop(i, Canvas.GetTop(i) + EnemySpeed); // движение врага
                    Rect EnemyHitbox = new Rect(Canvas.GetLeft(i), Canvas.GetTop(i), i.Width, i.Height); // хитбокс врага
                    if (Canvas.GetTop(i) > 750)
                    {
                        trash.Add(i); // добавление объекта в мусорку
                        Damage += 10;
                    }
                    if (PlayerHitbox.IntersectsWith(EnemyHitbox)) // проверяет на касание игрока с противником
                    {
                        trash.Add(i); // добавление объекта в мусорку
                        Damage += 5;
                    }
                }
            }
            foreach (Rectangle t in trash)
            {
                MyCanvas.Children.Remove(t); // удаление объекта из корзины
            }

            if (Score > 50)
            {
                EnemySpeed = 10;
                if (Score > 100)
                {
                    EnemySpeed = 12;
                    PlayerSpeed = 11;
                    if(Score > 150)
                    {
                        EnemySpeed = 14;
                        if(Score > 200)
                        {
                            EnemySpeed = 16;
                            PlayerSpeed = 14;
                        }
                    }
                }
            }
            if (Damage > 150) // прекращение работы игры
            {
                timer.Stop();
                damagestatus.Content = "УНИЧТОЖЕН";
                damagestatus.Foreground = Brushes.Red;
                if (Score > 0) MessageBox.Show("Набрано " + Score + " очков! Вы молодец!", "Статус:");
                else MessageBox.Show("Набрано " + Score + " очков :c Попробуйте ещё раз!", "Статус:");
                Close();
            }
        }

        public void key_up(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveleft = false;
            }
            if (e.Key == Key.Right)
            {
                moveright = false;
            }
        }

        public void key_down(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Left)
            {
                moveleft = true;
            }
            if (e.Key == Key.Right)
            {
                moveright = true;
            }
            if (e.Key == Key.Space)
            {
                Rectangle newbullet = new Rectangle // создаём пульку
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.Red,
                    Stroke = Brushes.Blue // параметры пульки
                };

                Rectangle newbullet2 = new Rectangle // создаём пульку
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.Red,
                    Stroke = Brushes.Blue // параметры пульки
                };

                Rectangle newbullet3 = new Rectangle // создаём пульку
                {
                    Tag = "bullet",
                    Height = 20,
                    Width = 5,
                    Fill = Brushes.Red,
                    Stroke = Brushes.Blue // параметры пульки
                };


                Canvas.SetLeft(newbullet, Canvas.GetLeft(player) + player.Width / 2); // положение пульки
                Canvas.SetTop(newbullet, Canvas.GetTop(player) - newbullet.Height);


                if(Score > 100)
                {
                    Canvas.SetLeft(newbullet2, Canvas.GetLeft(player)); // положение пульки
                    Canvas.SetTop(newbullet2, Canvas.GetTop(player) - newbullet2.Height);

                    Canvas.SetLeft(newbullet3, Canvas.GetLeft(player) + player.Width); // положение пульки
                    Canvas.SetTop(newbullet3, Canvas.GetTop(player) - newbullet3.Height);
                }
                MyCanvas.Children.Add(newbullet);
                MyCanvas.Children.Add(newbullet2);
                MyCanvas.Children.Add(newbullet3);

            }
        }

        public void EnemiesConstruct()
        {
            EnemyColorCounter = random.Next(1, 7); // рандомный цвет для врага
            EnemyHeightCounter = heightrandom.Next(30, 70); // рандомная высота врага
            EnemyWidthCounter = widthrandom.Next(35, 70); // рандомная ширина врага

            SolidColorBrush enemycolor = new SolidColorBrush();
            if (EnemyColorCounter == 1)
            {
                enemycolor = new SolidColorBrush(Colors.LemonChiffon);
            }
            if (EnemyColorCounter == 2)
            {
                enemycolor = new SolidColorBrush(Colors.Red);
            }
            if (EnemyColorCounter == 3)
            {
                enemycolor = new SolidColorBrush(Colors.MediumPurple);
            }
            if (EnemyColorCounter == 4)
            {
                enemycolor = new SolidColorBrush(Colors.LightSlateGray);
            }
            if (EnemyColorCounter == 5)
            {
                enemycolor = new SolidColorBrush(Colors.Aquamarine);
            }
            if(EnemyColorCounter == 6)
            {
                enemycolor = new SolidColorBrush(Colors.Salmon);
            }

            Rectangle newenemy = new Rectangle // создаём врага
            {
                Tag = "enemy",
                Height = EnemyHeightCounter,
                Width = EnemyWidthCounter,
                Fill = enemycolor // параметры врага
            };

            Canvas.SetLeft(newenemy, randpos.Next(35, 555)); // положение врага
            Canvas.SetTop(newenemy, -100);
            MyCanvas.Children.Add(newenemy);
        }
    }
}
