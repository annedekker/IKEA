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
    /// Interaction logic for GameViewPage.xaml
    /// </summary>
    public partial class GameViewPage : Page
    {
        IKEAGame game;

        private SolidColorBrush ikeaBlue = new SolidColorBrush(Color.FromRgb(0, 51, 153));

        public GameViewPage()
        {
            InitializeComponent();
        }

        private void GameViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            game = new IKEAGame(24);
            game.InitGame();

            theCanvas.Children.Clear();
            DrawMazeWalls();
            DrawMazeLocations();
            DrawShoppingList();
        }

        // Shopping List

        private void DrawShoppingList()
        {
            shoppingListPanel.Children.Clear();

            Label header = new Label();
            header.Content = "S H O P P I N G   L I S T";
            header.FontSize = 15;
            header.Foreground = ikeaBlue;
            header.FontWeight = FontWeights.SemiBold;
            header.HorizontalContentAlignment = HorizontalAlignment.Center;
            header.Margin = new Thickness(4, 0, 4, 4);
            shoppingListPanel.Children.Add(header);

            foreach (IKEAGame.Item item in game.ShoppingList)
            {
                DrawShoppingItem(item);
            }
        }

        private void DrawShoppingItem(IKEAGame.Item item)
        {
            DockPanel panel = new DockPanel();
            panel.Margin = new Thickness(8, 2, 8, 2);

            Image image = new Image();
            switch (item)
            {
                case IKEAGame.Item.Exit:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/exit_b.png"));
                    break;
                case IKEAGame.Item.Cafe:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/cafe.png"));
                    break;
                case IKEAGame.Item.Sale:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/sale.png"));
                    break;
                case IKEAGame.Item.Bed:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/bed_b.png"));
                    break;
                case IKEAGame.Item.Bench:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/bench_b.png"));
                    break;
                case IKEAGame.Item.Chair:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/chair_b.png"));
                    break;
                case IKEAGame.Item.Clock:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/clock_b.png"));
                    break;
                case IKEAGame.Item.Drawers:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/drawers_b.png"));
                    break;
                case IKEAGame.Item.Lightbulb:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/lightbulb_b.png"));
                    break;
                case IKEAGame.Item.Mirror:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/mirror_b.png"));
                    break;
                case IKEAGame.Item.Plant:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/plant_b.png"));
                    break;
                case IKEAGame.Item.Sofa:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/sofa_b.png"));
                    break;
                case IKEAGame.Item.Table:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/table_b.png"));
                    break;
                case IKEAGame.Item.Vase:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/vase_b.png"));
                    break;
                case IKEAGame.Item.Wardrobe:
                    image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/wardrobe_b.png"));
                    break;
            }
            DockPanel.SetDock(image, Dock.Left);
            panel.Children.Add(image);

            Label label = new Label();
            label.Content = IkeaifyString(item.ToString());
            label.Foreground = ikeaBlue;
            label.FontWeight = FontWeights.SemiBold;
            label.HorizontalContentAlignment = HorizontalAlignment.Center;
            panel.Children.Add(label);

            shoppingListPanel.Children.Add(panel);
        }

        // Drawing Static Maze Objects

        private void DrawMazeWalls()
        {
            double cellSize = 900.0 / game.MazeSize; // 900.0 = canvas size - borders
            double wallSize = cellSize / 10;

            // Borders
            DrawWall(50, 50, 50, 950, wallSize * 1.5); // W
            DrawWall(50 - (wallSize * 1.5 / 2), 50, 950 + (wallSize * 1.5 / 2), 50, wallSize * 1.5); // N
            DrawWall(950, 50, 950, 950, wallSize * 1.5);
            DrawWall(50 - (wallSize * 1.5 / 2), 950, 950 + (wallSize * 1.5 / 2), 950, wallSize * 1.5); // S

            // Vertical (N to S - east wall)
            for (int x = 0; x < game.MazeSize - 1; x++)
            {
                int start = -1;
                int stop = -1;

                for (int y = 0; y < game.MazeSize; y++)
                {
                    if (game.Field[x,y].EastWall)
                    {
                        if (start == -1)
                        {
                            start = stop = y;
                        }
                        else stop = y;
                    }
                    else
                    {
                        if (start != -1)
                        {
                            DrawWall(
                                50 + x * cellSize + cellSize,
                                50 + start * cellSize,
                                50 + x * cellSize + cellSize,
                                50 + stop * cellSize + cellSize,
                                wallSize);
                            start = stop = -1;
                        }
                    }
                    if (y == game.MazeSize - 1 && start != -1)
                    {
                        DrawWall(
                                50 + x * cellSize + cellSize,
                                50 + start * cellSize,
                                50 + x * cellSize + cellSize,
                                50 + stop * cellSize + cellSize,
                                wallSize);
                    }
                }
            }
            // Horizontal (E to W - south wall)
            for (int y = 0; y < game.MazeSize - 1; y++)
            {
                int start = -1;
                int stop = -1;

                for (int x = 0; x < game.MazeSize; x++)
                {
                    if (game.Field[x, y].SouthWall)
                    {
                        if (start == -1)
                        {
                            start = stop = x;
                        }
                        else stop = x;
                    }
                    else
                    {
                        if (start != -1)
                        {
                            DrawWall(
                                50 + start * cellSize - (wallSize / 2),
                                50 + y * cellSize + cellSize,
                                50 + stop * cellSize + cellSize + (wallSize / 2),
                                50 + y * cellSize + cellSize,
                                wallSize);
                            start = stop = -1;
                        }
                    }
                    if (y == game.MazeSize - 1 && start != -1)
                    {
                        DrawWall(
                                50 + start * cellSize - (wallSize / 2),
                                50 + y * cellSize + cellSize,
                                50 + stop * cellSize + cellSize + (wallSize/2),
                                50 + y * cellSize + cellSize,
                                wallSize);
                    }
                }
            }
        }

        private void DrawWall(double x1, double y1, double x2, double y2, double wallSize)
        {
            Line wall = new Line();
            wall.Stroke = ikeaBlue;
            wall.StrokeThickness = wallSize;
            wall.X1 = x1;
            wall.Y1 = y1;
            wall.X2 = x2;
            wall.Y2 = y2;
            theCanvas.Children.Add(wall);
        }
        
        private void DrawMazeLocations()
        {
            double cellSize = 900.0 / game.MazeSize;

            foreach (var item in game.PointsOfInterest)
            {
                Image image = new Image();
                image.Width = image.Height = cellSize - cellSize / 5;

                switch (item.Key)
                {
                    case IKEAGame.Item.Exit:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/exit_b.png"));
                        break;
                    case IKEAGame.Item.Cafe:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/cafe.png"));
                        break;
                    case IKEAGame.Item.Sale:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/sale.png"));
                        break;
                    case IKEAGame.Item.Bed:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/bed_b.png"));
                        break;
                    case IKEAGame.Item.Bench:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/bench_b.png"));
                        break;
                    case IKEAGame.Item.Chair:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/chair_b.png"));
                        break;
                    case IKEAGame.Item.Clock:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/clock_b.png"));
                        break;
                    case IKEAGame.Item.Drawers:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/drawers_b.png"));
                        break;
                    case IKEAGame.Item.Lightbulb:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/lightbulb_b.png"));
                        break;
                    case IKEAGame.Item.Mirror:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/mirror_b.png"));
                        break;
                    case IKEAGame.Item.Plant:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/plant_b.png"));
                        break;
                    case IKEAGame.Item.Sofa:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/sofa_b.png"));
                        break;
                    case IKEAGame.Item.Table:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/table_b.png"));
                        break;
                    case IKEAGame.Item.Vase:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/vase_b.png"));
                        break;
                    case IKEAGame.Item.Wardrobe:
                        image.Source = new BitmapImage(new Uri("pack://application:,,,/resources/items/wardrobe_b.png"));
                        break;
                }

                image.Margin = new Thickness(
                    50 + item.Value.X * cellSize + cellSize / 10,
                    50 + item.Value.Y * cellSize + cellSize / 10,
                    0, 0);

                theCanvas.Children.Add(image);
            }
        }

        // Drawing Utilities

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
