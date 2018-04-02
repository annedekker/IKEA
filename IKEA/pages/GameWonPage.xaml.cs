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

namespace IKEA
{
    /// <summary>
    /// Interaction logic for GameWonPage.xaml
    /// </summary>
    public partial class GameWonPage : Page
    {
        MainWindow window;

        SolidColorBrush ikeaBlue = new SolidColorBrush(Color.FromRgb(0, 51, 153));
        SolidColorBrush scoreBoardGrey = new SolidColorBrush(Color.FromRgb(232, 232, 232));

        int playerScore;
        int mazeSize;
        int timeTaken;

        public GameWonPage()
        {
            InitializeComponent();
        }

        private void GameWon_Loaded(object sender, RoutedEventArgs e)
        {
            window = (MainWindow)(Window.GetWindow(this));
            DrawHighScores();

            playerScore = window.LastGameScore;
            mazeSize = window.MazeSize;
            timeTaken = window.LastGameTime;

            playerScoreLabel.Content = playerScore.ToString();
            mazeSizeLabel.Content = IkeaifyString("Maze Size : " + mazeSize.ToString());
            timeTakenLabel.Content = IkeaifyString("Time Taken : " + Timeify(timeTaken));
        }

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            window.SetPage(MainWindow.Page.MainMenu);
        }
        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            window.SetPage(MainWindow.Page.GameView);
        }

        private void SaveScore_Click(object sender, RoutedEventArgs e)
        {
            string name = playerNameTextbox.Text.Trim();

            if (name.Length < 1) return;

            window.HighScores.AddScore(name, playerScore, mazeSize, timeTaken);

            (sender as Button).IsEnabled = false;
        }


        // Drawing

        private void DrawHighScores()
        {
            var scores = (window as MainWindow).HighScores.Scores;

            for (int i = 0; i < scores.Count; i++)
            {
                RowDefinition rdef = new RowDefinition();
                rdef.Height = new GridLength(32);
                highScoreGrid.RowDefinitions.Add(rdef);

                if (i % 2 == 0)
                {
                    Rectangle rect = new Rectangle();
                    rect.Fill = scoreBoardGrey;
                    Grid.SetColumnSpan(rect, 5);
                    Grid.SetRow(rect, i + 2);
                    highScoreGrid.Children.Add(rect);
                }

                // Rank
                Label lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = (i + 1).ToString();
                Grid.SetColumn(lbl, 0);
                Grid.SetRow(lbl, i + 2);
                highScoreGrid.Children.Add(lbl);
                // Name
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = scores[i].PlayerName;
                Grid.SetColumn(lbl, 1);
                Grid.SetRow(lbl, i + 2);
                highScoreGrid.Children.Add(lbl);
                // Score
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = scores[i].PlayerScore.ToString();
                Grid.SetColumn(lbl, 2);
                Grid.SetRow(lbl, i + 2);
                highScoreGrid.Children.Add(lbl);
                // Size
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = scores[i].MazeSize;
                Grid.SetColumn(lbl, 3);
                Grid.SetRow(lbl, i + 2);
                highScoreGrid.Children.Add(lbl);
                // Time
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = Timeify(scores[i].TimeTaken);
                Grid.SetColumn(lbl, 4);
                Grid.SetRow(lbl, i + 2);
                highScoreGrid.Children.Add(lbl);
            }
        }

        private string Timeify(int time)
        {
            int min = time / 60;
            int sec = time % 60;

            string s = min.ToString() + ".";
            if (sec < 10) s += "0";
            s += sec.ToString();
            return s;
        }

        private string IkeaifyString(string s)
        {
            string newstr = "";

            foreach (char c in s)
            {
                newstr += c;
                newstr += ' ';
            }

            return newstr.ToUpper();
        }
        
    }
}
