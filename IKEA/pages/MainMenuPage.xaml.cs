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
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public MainMenuPage()
        {
            InitializeComponent();
        }

        private void Play_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);
            wdw.SetPage(MainWindow.Page.GameView);
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);
            wdw.Close();
        }

        private void HighScores_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);
            wdw.SetPage(MainWindow.Page.HighScores);
        }

        private void HowTo_Click(object sender, RoutedEventArgs e)
        {
            MainWindow wdw = (MainWindow)Window.GetWindow(this);
            wdw.SetPage(MainWindow.Page.HowTo);
        }
    }
}
