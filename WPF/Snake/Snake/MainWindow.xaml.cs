using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Snake
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class SnakeGame : Window
    {
        // ObservableCollection:
        public static ObservableCollection<SnakeHighscore> listHighscore
        {
            get; set;
        } = new ObservableCollection<SnakeHighscore>();
        const int MaxHighscores = 5;

        #region Fields, etc.
        const int SnakeSquareSize = 20;
        const int SnakeStartLength = 3;
        const int SnakeStartSpeed = 400;
        const int SnakeSpeedThreshold = 100;
        private SolidColorBrush snakeBodyBrush = Brushes.Green;
        private SolidColorBrush snakeHeadBrush = Brushes.YellowGreen;
        private List<SnakePart> snakeParts = new List<SnakePart>();

        public enum SnakeDirection { Left, Right, Up, Down }
        private SnakeDirection sDirect = SnakeDirection.Right;
        private int sLength;
        private int currentScore = 0;

        DispatcherTimer gTimer = new DispatcherTimer();
        bool gPause = false;

        private Random rnd = new Random();

        // Food
        private UIElement sFood = null;
        private SolidColorBrush fBrush = Brushes.Red;
        #endregion

        // Constructor
        public SnakeGame()
        {
            InitializeComponent();
            gTimer.Tick += GameTickTimer_Tick;
            SnakeHighscore.LoadHighscoreList();
            
        }

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            DrawGameArea();
            wWelcomeMessage.Visibility = Visibility.Visible;
            gPause = true;
            StartNewGame();
        }

        private void GameTickTimer_Tick(object sender, EventArgs e)
        {
            if (gPause)
            {
                return;
            }
                
            MoveSnake();
        }

        private void PauseGame()
        {
            wPause.Visibility = Visibility.Visible;
            if (soundbite.CanPause) soundbite.Stop();
            PlayMenuMusic();
            gPause = true;
        }

        private void StartNewGame()
        {
            //wWelcomeMessage.Visibility = Visibility.Collapsed;
            wHighscores.Visibility = Visibility.Collapsed;
            wEnd.Visibility = Visibility.Collapsed;



            foreach(SnakePart sBodyPart in snakeParts)
            {
                if(sBodyPart.UiElement != null)
                    GameArea.Children.Remove(sBodyPart.UiElement);
            }
            snakeParts.Clear();

            if (sFood != null)
                GameArea.Children.Remove(sFood);

            currentScore = 0;
            sLength = SnakeStartLength;
            sDirect = SnakeDirection.Right;
            snakeParts.Add(new SnakePart() { Position = new Point(SnakeSquareSize * 5, SnakeSquareSize * 5) });
            gTimer.Interval = TimeSpan.FromMilliseconds(SnakeStartSpeed);

            
            DrawSnake();
            DrawSnakeFood();
            UpdateGameStatus();

            gTimer.IsEnabled = true;
        }

        #region Draw Game
        private void DrawGameArea()
        {
            bool doneDrawingBg = false, nIsOdd = false, showGrid = false;
            int nX = 0, nY = 0, rowCounter = 0;

            while (doneDrawingBg == false)
            {
                Rectangle rect = new Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = nIsOdd ? Brushes.White : Brushes.Black,
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nY);
                Canvas.SetLeft(rect, nX);

                nIsOdd = !nIsOdd;
                nX += SnakeSquareSize;

                if (nX >= GameArea.ActualWidth)
                {
                    nX = 0;
                    nY += SnakeSquareSize;
                    rowCounter++;
                    nIsOdd = (rowCounter % 2 != 0);
                }

                if (nY >= GameArea.ActualHeight)
                {
                    doneDrawingBg = true;
                }
            }
            //StartNewGame();

        }

        private void DrawGameAreaTwo()
        {
            
            bool doneDrawingBg = false, nIsOdd = false, showGrid = false;
            int nX = 0, nY = 0, rowCounter = 0;
            fBrush = Brushes.BlanchedAlmond;
            snakeBodyBrush = Brushes.LightGoldenrodYellow;
            snakeHeadBrush = Brushes.PapayaWhip;

            while (doneDrawingBg == false)
            {
                Rectangle rect = new Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = nIsOdd ? Brushes.Red : Brushes.Green,
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nY);
                Canvas.SetLeft(rect, nX);

                nIsOdd = !nIsOdd;
                nX += SnakeSquareSize;

                if (nX >= GameArea.ActualWidth)
                {
                    nX = 0;
                    nY += SnakeSquareSize;
                    rowCounter++;
                    nIsOdd = (rowCounter % 2 != 0);
                }

                if (nY >= GameArea.ActualHeight)
                {
                    doneDrawingBg = true;
                }
            }
            //StartNewGame();

        }

        private void DrawGameAreaThree()
        {
            bool doneDrawingBg = false, nIsOdd = false, showGrid = false;
            int nX = 0, nY = 0, rowCounter = 0, colCounter = 0;
            int xCount = 0, yCount = 0;
            fBrush = Brushes.Gray;
            snakeBodyBrush = Brushes.Black;
            snakeHeadBrush = Brushes.DimGray;

            while (doneDrawingBg == false)
            {
                nIsOdd = (xCount % 2 == 0) | (yCount % 2 == 0);
                Rectangle rect = new Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = nIsOdd ? Brushes.White : Brushes.Black,
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nY);
                Canvas.SetLeft(rect, nX);
                
                nX += SnakeSquareSize;
                xCount++;

                if (nX >= GameArea.ActualWidth)
                {
                    nX = 0;
                    rowCounter++;
                    nY += SnakeSquareSize;
                    yCount++;
                    xCount = 0;
                }

                if (nY >= GameArea.ActualHeight)
                {
                    doneDrawingBg = true;
                }
            }
            //StartNewGame();

        }
        private void DrawGameAreaFour()
        {
            bool doneDrawingBg = false, nIsOdd = false, showGrid = false;
            int nX = 0, nY = 0, rowCounter = 0, colCounter = 0;
            int xCount = 0, yCount = 0;
            fBrush = Brushes.Gray;

            while (doneDrawingBg == false)
            {
                nIsOdd = ((xCount - yCount) % 3 == 0);
                Rectangle rect = new Rectangle
                {
                    Width = SnakeSquareSize,
                    Height = SnakeSquareSize,
                    Fill = nIsOdd ? Brushes.White : Brushes.Black,
                };
                GameArea.Children.Add(rect);
                Canvas.SetTop(rect, nY);
                Canvas.SetLeft(rect, nX);

                nX += SnakeSquareSize;
                xCount++;
                if (nX >= GameArea.ActualWidth)
                {
                    nX = 0;
                    rowCounter++;
                    nY += SnakeSquareSize;
                    yCount++;
                    xCount = 0;
                }

                if (nY >= GameArea.ActualHeight)
                {
                    doneDrawingBg = true;
                }
            }
            //StartNewGame();

        }
        #endregion

        #region Draw Snake
        private void DrawSnake()
        {
            foreach (SnakePart p in snakeParts)
            {
                if (p.UiElement == null)
                {
                    p.UiElement = new Rectangle()
                    {
                        Width = SnakeSquareSize,
                        Height = SnakeSquareSize,
                        Fill = (p.IsHead ? snakeHeadBrush : snakeBodyBrush)
                    };
                    GameArea.Children.Add(p.UiElement);
                    Canvas.SetTop(p.UiElement, p.Position.Y);
                    Canvas.SetLeft(p.UiElement, p.Position.X);
                }
            }
        }
        #endregion

        #region Draw Food
        private void DrawSnakeFood()
        {
            Point fPosition = GetNextFoodPosition();
            sFood = new Ellipse()
            {
                Width = SnakeSquareSize,
                Height = SnakeSquareSize,
                Fill = fBrush
            };
            GameArea.Children.Add(sFood);
            Canvas.SetTop(sFood, fPosition.Y);
            Canvas.SetLeft(sFood, fPosition.X);
        }
        #endregion

        private async Task MoveSnake()
        { 
        while (snakeParts.Count >= sLength)
            {
                GameArea.Children.Remove(snakeParts[0].UiElement);
                snakeParts.RemoveAt(0);
            }

        foreach (SnakePart p in snakeParts)
            {
                (p.UiElement as Rectangle).Fill = snakeBodyBrush;
                p.IsHead = false;
            }

            SnakePart h = snakeParts[snakeParts.Count - 1];
            double nX = h.Position.X;
            double nY = h.Position.Y;

            switch (sDirect)
            {
                case SnakeDirection.Left:
                    nX -= SnakeSquareSize;
                    break;
                case SnakeDirection.Right:
                    nX += SnakeSquareSize;
                    break;
                case SnakeDirection.Up:
                    nY -= SnakeSquareSize;
                    break;
                case SnakeDirection.Down:
                    nY += SnakeSquareSize;
                    break;
            }

            snakeParts.Add(new SnakePart()
            {
                Position = new Point(nX, nY),
                IsHead = true,
            });

            DrawSnake();
            DoCollissionCheck();
        }

        private Point GetNextFoodPosition()
        {
            int maxX = (int)(GameArea.ActualWidth / SnakeSquareSize);
            int maxY = (int)(GameArea.ActualHeight / SnakeSquareSize);
            int fX = rnd.Next(0, maxX) * SnakeSquareSize;
            int fY = rnd.Next(0, maxY) * SnakeSquareSize;

            foreach (SnakePart sPart in snakeParts)
            {
                if((sPart.Position.X == fX) && (sPart.Position.Y == fY))
                    return GetNextFoodPosition();
            }

            return new Point(fX, fY);
        }

        private async void EatSnakeFood()
        {
            sLength++;
            currentScore++;
            int tInterval = Math.Max(SnakeSpeedThreshold, (int)gTimer.Interval.TotalMilliseconds - (currentScore * 2));
            gTimer.Interval = TimeSpan.FromMilliseconds(tInterval);
            await PlaySoundBite();
            GameArea.Children.Remove(sFood);
            DrawSnakeFood();
            UpdateGameStatus();
        }

        private MediaPlayer soundbite = new();
        private TimeSpan soundbitePos = TimeSpan.Zero;
        private MediaPlayer menu = new();

        private async Task PlaySoundBite()
        {
            Uri uri = new(@"C:\Users\Santi\Desktop\EUC\H2\H2-Programs\WPF\Snake\Snake\omnom.mp3");
            soundbite.Open(uri);
            soundbite.Play();
        }

        private async Task PlayMenuMusic()
        {
            Uri uri = new(@"C:\Users\Santi\Desktop\EUC\H2\H2-Programs\WPF\Snake\Snake\Elevator-music.mp3");
            menu.Open(uri);
            menu.Play();
        }

        private void UpdateGameStatus()
        {
            //this.Title = $"Snake - Score: {currentScore} - Game speed: {gTimer.Interval.TotalMilliseconds}";
            this.statusScore.Text = currentScore.ToString();
            this.statusSpeed.Text = gTimer.Interval.TotalMilliseconds.ToString();
            if (currentScore % (incrementRange.Text != "" ? int.Parse(incrementRange.Text) : 5 ) == 0 && currentScore != 0) ChangeGameArea();
        }

        private void DoCollissionCheck()
        {
            SnakePart sHead = snakeParts[snakeParts.Count - 1];
            if ((sHead.Position.X == Canvas.GetLeft(sFood)) && (sHead.Position.Y == Canvas.GetTop(sFood)))
            {
                EatSnakeFood();
                return;
            }

            if((sHead.Position.Y < 0) || (sHead.Position.Y >= GameArea.ActualHeight) || (sHead.Position.X < 0) || (sHead.Position.X >= GameArea.ActualWidth))
            {
                EndGame();
            }

            foreach (SnakePart sBodyPart in snakeParts.Take(snakeParts.Count - 1))
            {
                if ((sHead.Position.X == sBodyPart.Position.X) && (sHead.Position.Y == sBodyPart.Position.Y)) 
                    EndGame();
                
            }
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if (incrementRange.IsFocused)
            {
                switch (e.Key)
                {
                    case Key.Enter:
                        StartNewGame();
                        wWelcomeMessage.Visibility = Visibility.Collapsed;
                        //incrementRange.MoveFocus(new TraversalRequest(FocusNavigationDirection.Next));
                        gPause = false;
                        FocusManager.SetFocusedElement(FocusManager.GetFocusScope(incrementRange), null); // Kills logical focus.
                        break;

                }
            }
            if (!incrementRange.IsFocused)
            {
                SnakeDirection originalDirect = sDirect;
                if (gPause)
                {
                    gPause = false;
                    wPause.Visibility = Visibility.Collapsed;
                    wWelcomeMessage.Visibility = Visibility.Collapsed;
                    menu.IsMuted = true;
                    switch (e.Key)
                    {
                        case Key.Escape:
                            Close();
                            break;
                        case Key.Enter:
                            gPause = true;
                            wPause.Visibility = Visibility.Collapsed;
                            wWelcomeMessage.Visibility = Visibility.Visible;
                            break;
                        default:
                            soundbite.Position = soundbitePos;
                            soundbite.Play();
                            break;
                    }
                    return;
                }
                switch (e.Key)
                {
                    case Key.Up:
                        if (sDirect != SnakeDirection.Down) sDirect = SnakeDirection.Up;
                        break;
                    case Key.Down:
                        if (sDirect != SnakeDirection.Up) sDirect = SnakeDirection.Down;
                        break;
                    case Key.Right:
                        if (sDirect != SnakeDirection.Left) sDirect = SnakeDirection.Right;
                        break;
                    case Key.Left:
                        if (sDirect != SnakeDirection.Right) sDirect = SnakeDirection.Left;
                        break;
                    case Key.B:
                        ChangeGameArea();
                        break;
                    case Key.P:
                        if (soundbite.CanPause)
                        {
                            soundbite.Pause();
                            soundbitePos = soundbite.Position;
                        }
                        PauseGame();
                        menu.IsMuted = false;
                        break;
                    case Key.Space:
                        StartNewGame();
                        break;
                }
                wWelcomeMessage.Visibility = Visibility.Collapsed;

                if (sDirect != originalDirect)
                    MoveSnake();
            }
        }

        
        private int gameArea = 0;

        private void ChangeGameArea(bool countIncrement = true)
        {
            if (countIncrement)
            {
                gameArea++;
            }
            if (gameArea == 0) DrawGameArea();
            if (gameArea == 1) DrawGameAreaTwo();
            if (gameArea == 2) DrawGameAreaThree();
            if (gameArea == 3) DrawGameAreaFour();
            if (gameArea > 3) gameArea = 0;
            GameArea.Children.Remove(sFood);
            DrawSnakeFood();
        }

        private void EndGame()
        {
            bool newHigh = false;
            if(currentScore > 0)
            {
                int lowestHigh = (listHighscore.Count > 0) ? listHighscore.Min(x => x.Score) : 0;
                if ((currentScore > lowestHigh) || (listHighscore.Count < MaxHighscores))
                {
                    wNewHighscore.Visibility = Visibility.Visible;
                    txtPlayerName.Focus();
                    newHigh = true;
                }
            }
            if (!newHigh)
            {
                tbFinalScore.Text = currentScore.ToString();
                wEnd.Visibility = Visibility.Visible;
                
            }
            gTimer.IsEnabled = false;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.DragMove();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnShowHighscore_Click(object sender, RoutedEventArgs e)
        {
            wWelcomeMessage.Visibility = Visibility.Collapsed;
            wHighscores.Visibility = Visibility.Visible;
        }

        private void BtnAddToHighscoreList_Click(object sender, RoutedEventArgs e)
        {
            int nIndex = 0;

            if((listHighscore.Count > 0) && (currentScore < listHighscore.Max(x => x.Score)))
            {
                SnakeHighscore justAbove = listHighscore.OrderByDescending(x => x.Score).First(x => x.Score >= currentScore);
                if(justAbove != null)
                    nIndex = listHighscore.IndexOf(justAbove) + 1;
            }

            listHighscore.Insert(nIndex, new SnakeHighscore()
            {
                Player = txtPlayerName.Text,
                Score = currentScore,
            });

            while (listHighscore.Count > MaxHighscores)
                listHighscore.RemoveAt(MaxHighscores);

            SnakeHighscore.SaveHighscoreList();

            wNewHighscore.Visibility = Visibility.Collapsed;
            wHighscores.Visibility = Visibility.Visible;


        }

        private void incrementRange_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex numberCheck = new("[^0-9]+");
            e.Handled = numberCheck.IsMatch(e.Text);
        }

        private void clearHighscore_Click(object sender, RoutedEventArgs e)
        {
            SnakeHighscore.DeleteHighscoreList();
        }

        private void recoverHighscore_Click(object sender, RoutedEventArgs e)
        {
            SnakeHighscore.RecoverHighscoreList();
        }

        private void returnToMain_Click(object sender, RoutedEventArgs e)
        {
            wHighscores.Visibility = Visibility.Collapsed;
            wWelcomeMessage.Visibility = Visibility.Visible;
        }

        private void DockPanel_MouseLeave(object sender, MouseEventArgs e)
        {
            gPause = true;
            if (wWelcomeMessage.Visibility == Visibility.Collapsed)
                wPause.Visibility = Visibility.Visible;
        }

        private void DockPanel_MouseEnter(object sender, MouseEventArgs e)
        {

            if (wPause.Visibility == Visibility.Visible)
                gPause = false;
                wPause.Visibility = Visibility.Collapsed;
        }

        private void GameArea_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {

        }

        private void GameArea_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //double x = Math.Floor(e.NewSize.Width);

            ////double y = Math.Floor(GameArea.Height);
            //bool sizeCompliant = false;

            //if (gameWindow.IsLoaded)
            //{
            //    //if (e.Handled == true)
            //    //{
            //    //    while (!sizeCompliant)
            //    //    {

            //    //        x++;
            //    //        if (x % SnakeSquareSize == 0)
            //    //        {
            //    //            sizeCompliant = true;

            //    //        }


            //    //    }
            //    //}
            //}


            ChangeGameArea(false);
        }

        private void BoardSize_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int sliderValue = (int)BoardSize.Value;
            if (sliderValue % 2 == 0)
            {
                double nSize = sliderValue * SnakeSquareSize;
                GameArea.Width = GameArea.Height = nSize;

                ChangeGameArea(false);
            }

        }
    }
}
