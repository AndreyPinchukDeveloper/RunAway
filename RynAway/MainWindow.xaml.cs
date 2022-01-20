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
using System.Windows.Threading;

namespace RynAway
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer gameTimer = new DispatcherTimer();
        Rect playerHitBox;
        Rect groundHitBox;
        Rect obstacleHitBox;

        bool isJumping;

        int force =20;
        int speed = 5;

        Random random = new Random();

        bool gameOver;

        double spriteIndex = 0;

        ImageBrush playerSprite = new ImageBrush();
        ImageBrush backgroundSprite = new ImageBrush();
        ImageBrush obstacleSprite = new ImageBrush();

        int[] obstaclePosition = { 320,310,300,305,315};

        int score = 0;

        public MainWindow()
        {
            InitializeComponent();

            MyCanvas.Focus();
            gameTimer.Tick += GameEngine;
            gameTimer.Interval = TimeSpan.FromMilliseconds(20);

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.gif"));//set an your own way here

            Background.Fill = backgroundSprite;
            Background2.Fill = backgroundSprite;

            StartGame();

        }

        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetLeft(Background, Canvas.GetLeft(Background) - 7);
            Canvas.SetLeft(Background2, Canvas.GetLeft(Background2) - 7);

            if (Canvas.GetLeft(Background)<-1262)
            {
                Canvas.SetLeft(Background, Canvas.GetLeft(Background2) + Background2.Width);
            }

            if (Canvas.GetLeft(Background) < -1262)
            {
                Canvas.SetLeft(Background2, Canvas.GetLeft(Background) + Background2.Width);
            }

            Canvas.SetTop(Player, Canvas.GetTop(Player) - speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);
            scoreText.Content = "Score: " + score;
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter&&gameOver==true)
            {
                StartGame();
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Space && isJumping==false && Canvas.GetTop(Player)>260)
            {
                isJumping = true;
                force = 15;
                speed = -12;
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/background.gif"));//set an your own way here
            }
        }

        private void StartGame()
        {
            Canvas.SetLeft(Background, 0);
            Canvas.SetLeft(Background2, 1262);

            Canvas.SetLeft(Player, 110);
            Canvas.SetTop(Player, 140);

            Canvas.SetLeft(obstacle, 950);
            Canvas.SetTop(obstacle, 310);

            RunSprite(1);

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/obstacle.png"));//set an your own way here
            obstacle.Fill = obstacleSprite;

            isJumping = false;
            gameOver = false;
            score = 0;

            scoreText.Content = "Score: " + score;
            gameTimer.Start();
        }

        private void RunSprite(double i)
        {
            switch (i)
            {
                case 1:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner01.gif"));//set an your own way here
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner02.gif"));//set an your own way here
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner03.gif"));//set an your own way here
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner04.gif"));//set an your own way here
                    break;
                case 5:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner05.gif"));//set an your own way here
                    break;
                case 6:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner06.gif"));//set an your own way here
                    break;
                case 7:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner07.gif"));//set an your own way here
                    break;
                case 8:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/runner08.gif"));//set an your own way here
                    break;
            }
            Player.Fill = playerSprite;
        }
    }
}
