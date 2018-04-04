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

namespace IKEA.pages
{
    /// <summary>
    /// Interaction logic for HighScorePage.xaml
    /// </summary>
    public partial class HighScorePage : Page
    {
        SolidColorBrush ikeaBlue = new SolidColorBrush(Color.FromRgb(0, 51, 153));
        SolidColorBrush scoreBoardGrey = new SolidColorBrush(Color.FromRgb(232, 232, 232));

        public HighScorePage()
        {
            InitializeComponent();
        }

        private void HighScores_Loaded(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);

            var scores = wdw.HighScores.Scores;
            DrawHighscores(scores);
        }

        private void DrawHighscores(List<Score> scores)
        {
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
                    Grid.SetRow(rect, i + 1);
                    highScoreGrid.Children.Add(rect);
                }

                // Rank
                Label lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.FontSize = 14;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = (i + 1).ToString();
                Grid.SetColumn(lbl, 0);
                Grid.SetRow(lbl, i + 1);
                highScoreGrid.Children.Add(lbl);
                // Name
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.FontSize = 14;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = scores[i].PlayerName;
                Grid.SetColumn(lbl, 1);
                Grid.SetRow(lbl, i + 1);
                highScoreGrid.Children.Add(lbl);
                // Score
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.FontSize = 14;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = scores[i].PlayerScore.ToString();
                Grid.SetColumn(lbl, 2);
                Grid.SetRow(lbl, i + 1);
                highScoreGrid.Children.Add(lbl);
                // Size
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.FontSize = 14;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = scores[i].MazeSize;
                Grid.SetColumn(lbl, 3);
                Grid.SetRow(lbl, i + 1);
                highScoreGrid.Children.Add(lbl);
                // Time
                lbl = new Label();
                lbl.Foreground = ikeaBlue;
                lbl.FontSize = 14;
                lbl.HorizontalContentAlignment = HorizontalAlignment.Center;
                lbl.Content = Timeify(scores[i].TimeTaken);
                Grid.SetColumn(lbl, 4);
                Grid.SetRow(lbl, i + 1);
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

        private void MainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);
            wdw.SetPage(MainWindow.Page.MainMenu);
        }

        private void NewGame_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);
            wdw.SetPage(MainWindow.Page.GameView);
        }
    }
}
