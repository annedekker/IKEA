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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public enum Page
        {
            MainMenu, GameView, GameWon, HighScores, HowTo, Settings
        };

        public Highscores HighScores = new Highscores();

        public int MazeSize { get; set; }
        public int LastGameScore { get; set; }
        public int LastGameTime { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            MazeSize = 12;
            LastGameScore = 12;

            SetPage(Page.MainMenu);
        }

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            (sender as Window).WindowState = WindowState.Maximized;
        }

        public void SetPage(Page page)
        {
            switch (page)
            {
                case Page.MainMenu:
                    mainFrame.Source = new Uri("pack://application:,,,/pages/MainMenuPage.xaml");
                    break;
                case Page.GameView:
                    mainFrame.Source = new Uri("pack://application:,,,/pages/GameViewPage.xaml");
                    break;
                case Page.GameWon:
                    mainFrame.Source = new Uri("pack://application:,,,/pages/GameWonPage.xaml");
                    break;
                case Page.HighScores:
                    mainFrame.Source = new Uri("pack://application:,,,/pages/HighScorePage.xaml");
                    break;
                case Page.HowTo:
                    mainFrame.Source = new Uri("pack://application:,,,/pages/HowToPage.xaml");
                    break;
                case Page.Settings:
                    mainFrame.Source = new Uri("pack://application:,,,/pages/SettingsPage.xaml");
                    break;
            }
        }
        
    }
}
