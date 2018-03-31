using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA
{
    class IKEAGame
    {
        private Random rnd = new Random();

        public const int ItemCount = 15;
        public enum Item
        {
            Exit, Cafe, Sale, Bed, Bench,
            Chair, Clock, Drawers, Lightbulb, Mirror,
            Plant, Sofa, Table, Vase, Wardrobe
        }

        public Maze Maze { get { return maze; } }
        public Cell[,] Field { get { return maze.Field; } }
        public int MazeSize { get { return maze.Size; } }
        Maze maze;

        public Dictionary<Item, XY> PointsOfInterest { get { return pointsOfInterest; } }
        Dictionary<Item, XY> pointsOfInterest;

        public List<Item> ShoppingList { get { return shoppingList; } }
        List<Item> shoppingList;

        // Init
        public IKEAGame(int mazeSize)
        {
            maze = new Maze(mazeSize);
        }

        public void InitGame()
        {
            maze.BuildMaze();
            BuildPointsOfInterest();
            BuildShoppingList();
        }

        private void BuildPointsOfInterest()
        {
            pointsOfInterest = new Dictionary<Item, XY>();
            pointsOfInterest[Item.Exit] = new XY(maze.Size - 1, maze.Size - 1);
            
            for (int i = 1; i < ItemCount; i++)
            {
                //if (i == 2 && rnd.Next(0,10) < 5) continue; // check if sale

                XY loc;
                while (true)
                {
                    loc = new XY(rnd.Next(maze.Size), rnd.Next(maze.Size));

                    if (loc.X < 2 && loc.Y < 2) continue;
                    if (pointsOfInterest.Any(c =>
                    c.Value.X >= loc.X - 1 && c.Value.X <= loc.X + 1 &&
                    c.Value.Y >= loc.Y - 1 && c.Value.Y <= loc.Y + 1)) continue;

                    break;
                }
                pointsOfInterest[(Item)i] = loc;
            }
        }

        private void BuildShoppingList()
        {
            shoppingList = new List<Item>();
            List<Item> possibleItems = new List<Item>()
            {
                Item.Bed, Item.Bench, Item.Chair, Item.Clock, Item.Drawers,
                Item.Lightbulb, Item.Mirror, Item.Plant, Item.Sofa, Item.Table,
                Item.Vase, Item.Wardrobe
            };

            int count = maze.Size - maze.Size / 2;
            if (count < 4) count = 4;
            else if (count > ItemCount - 3) count = ItemCount - 3;

            while (count > 0 && possibleItems.Count > 0)
            {
                Item item = possibleItems[rnd.Next(possibleItems.Count)];
                possibleItems.Remove(item);
                shoppingList.Add(item);
                count--;
            }
        }
    }
}
