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
    /// Interaction logic for SettingsPage.xaml
    /// </summary>
    public partial class SettingsPage : Page
    {
        MainWindow wdw;

        public SettingsPage()
        {
            InitializeComponent();
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

        private void Settings_Loaded(object sender, RoutedEventArgs e)
        {
            wdw = (MainWindow)Window.GetWindow(this);
        }

        private void MazeSizeUpDown_Click(object sender, EventArgs e)
        {
            if ((sender as Button).Content.ToString().Equals("+"))
            {
                wdw.MazeSize++;

                if (wdw.MazeSize >= 64) (sender as Button).IsEnabled = false;
                if (wdw.MazeSize == 9) mazeSizeDownButton.IsEnabled = true;
                mazeSizeTextbox.Text = wdw.MazeSize.ToString();
            }
            else
            {
                wdw.MazeSize--;

                if (wdw.MazeSize <= 8) (sender as Button).IsEnabled = false;
                if (wdw.MazeSize == 63) mazeSizeUpButton.IsEnabled = true;
                mazeSizeTextbox.Text = wdw.MazeSize.ToString();
            }
        }

        private void MazeSizeTextbox_Changed(object sender, TextChangedEventArgs e)
        {
            if ((sender as TextBox).Text.Length < 2) return;

            int value;

            if (!Int32.TryParse((sender as TextBox).Text, out value)) value = 12;

            mazeSizeDownButton.IsEnabled = mazeSizeUpButton.IsEnabled = true;

            if (value <= 1)
            {
                value = 1;
                mazeSizeDownButton.IsEnabled = false;
            }
            else if (value >= 64)
            {
                value = 64;
                mazeSizeUpButton.IsEnabled = false;
            }

            try
            {
                if (value >= 8) wdw.MazeSize = value;
            }
            catch (NullReferenceException) { }
        }
    }
}
