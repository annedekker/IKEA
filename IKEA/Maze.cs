using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA
{
    class Maze
    {
        Random rnd = new Random();

        // Vars -The actual playing field
        Cell[,] field;
        public Cell[,] Field { get { return field; } }
        // Vars - Needed for building
        Stack<XY> visitedCells;
        int size;
        public int Size { get { return size; } }
        
        // Init
        public Maze(int size)
        {
            this.size = size;
        }

        // Building 
        public void BuildMaze()
        {
            field = new Cell[size, size];
            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    field[x, y] = new Cell();
                }
            }
            visitedCells = new Stack<XY>();

            RecurseMaze(new XY(
                rnd.Next(0, size),
                rnd.Next(0, size)));

            for (int i = 0; i < size; i++) DisableRandomWall();
        }

        private void RecurseMaze(XY currentc)
        {
            visitedCells.Push(currentc);

            List<XY> buddies = GetUnvisitedNeighbours(currentc);

            if (buddies.Count < 1)
            {
                // no new paths found, backtrack & try to find a cell with unvisited neighbours
                XY oldc = visitedCells.Pop();

                while (true)
                {
                    if (GetUnvisitedNeighbours(oldc).Count > 0) break;

                    if (visitedCells.Count < 1) return; // All cells checked, no neighbours found, maze done!

                    oldc = visitedCells.Pop();
                }

                RecurseMaze(oldc);
            }
            else
            {
                // paths found, pick one at random & continue recursing
                XY newc = buddies[rnd.Next(0, buddies.Count)];

                DisableConnectingWalls(currentc, newc);

                RecurseMaze(newc);
            }
        }

        private List<XY> GetUnvisitedNeighbours(XY loc)
        {
            List<XY> buddies = new List<XY>();

            if (loc.X - 1 >= 0)
            {
                if (IsUnvisited(loc.X - 1, loc.Y)) buddies.Add(new XY(loc.X - 1, loc.Y));
            }
            if (loc.Y - 1 >= 0)
            {
                if (IsUnvisited(loc.X, loc.Y - 1)) buddies.Add(new XY(loc.X, loc.Y - 1));
            }
            if (loc.X + 1 < size)
            {
                if (IsUnvisited(loc.X + 1, loc.Y)) buddies.Add(new XY(loc.X + 1, loc.Y));
            }
            if (loc.Y + 1 < size)
            {
                if (IsUnvisited(loc.X, loc.Y + 1)) buddies.Add(new XY(loc.X, loc.Y + 1));
            }

            return buddies;
        }

        private bool IsUnvisited(XY loc)
        {
            if (field[loc.X, loc.Y].WestWall && field[loc.X, loc.Y].NorthWall &&
                field[loc.X, loc.Y].EastWall && field[loc.X, loc.Y].SouthWall) return true;
            else return false;
        }

        private bool IsUnvisited(int x, int y)
        {
            if (field[x, y].WestWall && field[x, y].NorthWall &&
                field[x, y].EastWall && field[x, y].SouthWall) return true;
            else return false;
        }

        private void DisableConnectingWalls(XY loc01, XY loc02)
        {
            if (loc01.X - 1 == loc02.X)
            {
                field[loc01.X, loc01.Y].WestWall = false;
                field[loc02.X, loc02.Y].EastWall = false;
            }
            else if (loc01.Y - 1 == loc02.Y)
            {
                field[loc01.X, loc01.Y].NorthWall = false;
                field[loc02.X, loc02.Y].SouthWall = false;
            }
            else if (loc01.X + 1 == loc02.X)
            {
                field[loc01.X, loc01.Y].EastWall = false;
                field[loc02.X, loc02.Y].WestWall = false;
            }
            else if (loc01.Y + 1 == loc02.Y)
            {
                field[loc01.X, loc01.Y].SouthWall = false;
                field[loc02.X, loc02.Y].NorthWall = false;
            }
            else throw new Exception();
        }

        private void DisableRandomWall()
        {
            XY cell;
            List<XY> buddies;

            while (true)
            {
                cell = new XY(rnd.Next(size), rnd.Next(size));
                buddies = GetWalledNeighbours(cell);

                if (buddies.Count > 0) break;
            }

            XY buddy = buddies[rnd.Next(buddies.Count)];

            DisableConnectingWalls(cell, buddy);
        }

        private List<XY> GetWalledNeighbours(XY cell)
        {
            List<XY> buddies = new List<XY>();

            if (cell.X - 1 >= 0)
            {
                if (field[cell.X, cell.Y].WestWall) buddies.Add(new XY(cell.X - 1, cell.Y));
            }
            if (cell.Y - 1 >= 0)
            {
                if (field[cell.X, cell.Y].NorthWall) buddies.Add(new XY(cell.X, cell.Y - 1));
            }
            if (cell.X + 1 < size)
            {
                if (field[cell.X, cell.Y].EastWall) buddies.Add(new XY(cell.X + 1, cell.Y));
            }
            if (cell.Y + 1 < size)
            {
                if (field[cell.X, cell.Y].SouthWall) buddies.Add(new XY(cell.X, cell.Y + 1));
            }

            return buddies;
        }
    }
}
