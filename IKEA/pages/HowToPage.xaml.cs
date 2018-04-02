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
    /// Interaction logic for HowToPage.xaml
    /// </summary>
    public partial class HowToPage : Page
    {
        Uri image0 = new Uri("pack://application:,,,/resources/runner_r.png");
        Uri image1 = new Uri("pack://application:,,,/resources/items/chair_b.png");
        Uri image2 = new Uri("pack://application:,,,/resources/items/cafe.png");
        Uri image3 = new Uri("pack://application:,,,/resources/items/sale.png");

        const string message0 = "You're the blue and yellow little guy that starts the game in the upper left corner. Your goal is to get to the exit as fast as possible." +
                " Use the WASD or the arrow keys to move.";
        const string message1 = "Try to get items that are on your shopping list, but avoid those that aren't - you'll get a bonus or a penalty to your score.";
        const string message2 = "Try your luck at the cafe. You'll either get meatballs or a toddler's party! If you've gotten meatballs once," +
            " they've ran out now and there will only be more toddlers.";
        const string message3 = "If you're lucky, there will be a sale! Grab it for some free bonus points.";

        int currentMessage = 0;

        public HowToPage()
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

        private void LoadCurrentMessage()
        {
            if (currentMessage < 0) currentMessage = 3;
            else if (currentMessage > 3) currentMessage = 0;

            switch (currentMessage)
            {
                default:
                case 0:
                    infoImage.Source = new BitmapImage(image0);
                    infoTextblock.Text = message0;
                    break;
                case 1:
                    infoImage.Source = new BitmapImage(image1);
                    infoTextblock.Text = message1;
                    break;
                case 2:
                    infoImage.Source = new BitmapImage(image2);
                    infoTextblock.Text = message2;
                    break;
                case 3:
                    infoImage.Source = new BitmapImage(image3);
                    infoTextblock.Text = message3;
                    break;
            }
        }

        private void InfoLeft_Click(object sender, RoutedEventArgs e)
        {
            currentMessage--;
            LoadCurrentMessage();
        }

        private void InfoRight_Click(object sender, RoutedEventArgs e)
        {
            currentMessage++;
            LoadCurrentMessage();
        }
    }
}
