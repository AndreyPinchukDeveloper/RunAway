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

            backgroundSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/background.gif"));
            Background.Fill = backgroundSprite;
            Background2.Fill = backgroundSprite;

            StartGame();

        }

        private void GameEngine(object sender, EventArgs e)
        {
            Canvas.SetLeft(Background, Canvas.GetLeft(Background) - 4);
            Canvas.SetLeft(Background2, Canvas.GetLeft(Background2) - 4);

            if (Canvas.GetLeft(Background)<-1262)
            {
                Canvas.SetLeft(Background, Canvas.GetLeft(Background2) + Background2.Width);
            }

            if (Canvas.GetLeft(Background2) < -1262)
            {
                Canvas.SetLeft(Background2, Canvas.GetLeft(Background) + Background.Width);
            }

            Canvas.SetTop(Player, Canvas.GetTop(Player) + speed);
            Canvas.SetLeft(obstacle, Canvas.GetLeft(obstacle) - 12);
            scoreText.Content = "Score: " + score;

            playerHitBox = new Rect(Canvas.GetLeft(Player), Canvas.GetTop(Player), Player.Width - 15, Player.Height);
            obstacleHitBox = new Rect(Canvas.GetLeft(obstacle), Canvas.GetTop(obstacle), obstacle.Width - 15, obstacle.Height - 10);
            groundHitBox = new Rect(Canvas.GetLeft(ground), Canvas.GetTop(ground), ground.Width - 15, ground.Height - 10);

            if (playerHitBox.IntersectsWith(groundHitBox))
            {
                speed = 0;
                Canvas.SetTop(Player, Canvas.GetTop(ground) - Player.Height);
                isJumping = false;
                spriteIndex += .5;

                if (spriteIndex>4)
                {
                    spriteIndex = 1;
                }
                RunSprite(spriteIndex);
            }

            if (isJumping==true)
            {
                speed = -9;
                force -= 1;
            }
            else
            {
                speed = 12;
            }

            if (force<0)
            {
                isJumping = false;
            }

            if (Canvas.GetLeft(obstacle)<-50)
            {
                Canvas.SetLeft(obstacle, 950);
                Canvas.SetTop(obstacle, obstaclePosition[random.Next(0, obstaclePosition.Length)]);

                score++;
            }

            if (playerHitBox.IntersectsWith(obstacleHitBox))
            {
                gameOver = true;
                gameTimer.Stop();
                scoreText.Content = "Score: " + score + " Press Enter to play again !";
            }

            if (gameOver == true)
            {
                obstacle.Stroke = Brushes.Black;
                obstacle.StrokeThickness = 1;

                Player.Stroke = Brushes.Red;
                Player.StrokeThickness = 1;
            }
            else
            {
                Player.StrokeThickness = 0;
                obstacle.StrokeThickness = 0;
            }
        }

        private void KeyIsDown(object sender, KeyEventArgs e)
        {
            if (e.Key==Key.Enter&&gameOver==true)
            {
                StartGame();
            }

            if (e.Key == Key.Space && isJumping == false && Canvas.GetTop(Player) > 260)
            {
                isJumping = true;
                force = 15;
                speed = -12;
                playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BlackRun 3.png"));
            }
        }

        private void KeyIsUp(object sender, KeyEventArgs e)
        {
            
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

            obstacleSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/Images/obstacle.png"));
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
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BlackRun 1.png"));
                    break;
                case 2:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BlackRun 2.png"));
                    break;
                case 3:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BlackRun 3.png"));
                    break;
                case 4:
                    playerSprite.ImageSource = new BitmapImage(new Uri("pack://application:,,,/images/BlackRun 4.png"));
                    break;
            }
            Player.Fill = playerSprite;
        }
    }
}
