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
        Maze maze = new Maze(12);

        private SolidColorBrush ikeaBlue = new SolidColorBrush(Color.FromRgb(0, 51, 153));

        public GameViewPage()
        {
            InitializeComponent();
        }

        private void GameViewPage_Loaded(object sender, RoutedEventArgs e)
        {
            maze.BuildMaze();

            theCanvas.Children.Clear();
            DrawMazeWalls();
        }

        // Drawing - Maze
        private void DrawMazeWalls()
        {
            double cellSize = 900.0 / maze.Size; // 900.0 = canvas size - borders
            double wallSize = cellSize / 10;

            // Borders
            DrawWall(50, 50, 50, 950, wallSize * 1.5); // W
            DrawWall(50 - (wallSize * 1.5 / 2), 50, 950 + (wallSize * 1.5 / 2), 50, wallSize * 1.5); // N
            DrawWall(950, 50, 950, 950, wallSize * 1.5);
            DrawWall(50 - (wallSize * 1.5 / 2), 950, 950 + (wallSize * 1.5 / 2), 950, wallSize * 1.5); // S

            // Vertical (N to S - east wall)
            for (int x = 0; x < maze.Size - 1; x++)
            {
                int start = -1;
                int stop = -1;

                for (int y = 0; y < maze.Size; y++)
                {
                    if (maze.Field[x,y].EastWall)
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
                    if (y == maze.Size - 1 && start != -1)
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
            for (int y = 0; y < maze.Size - 1; y++)
            {
                int start = -1;
                int stop = -1;

                for (int x = 0; x < maze.Size; x++)
                {
                    if (maze.Field[x, y].SouthWall)
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
                    if (y == maze.Size - 1 && start != -1)
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
    }
}
