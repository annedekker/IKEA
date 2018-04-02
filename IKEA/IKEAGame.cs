using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Timers;

namespace IKEA
{
    public class ScoreEventArgs
    {
        public int Amount;
        public bool Bonus;
        public string Message;

        public ScoreEventArgs(int amount, bool bonus, string message = "")
        {
            this.Amount = amount;
            this.Bonus = bonus;
            this.Message = message;
        }
    }

    class IKEAGame
    {
        private Random rnd = new Random();
        private Timer timer = new Timer();
        public int TimeTaken { get { return time; } }
        int time = 0;

        public const int ItemCount = 15;
        public enum Item
        {
            Exit, Cafe, Sale, Bed, Bench,
            Chair, Clock, Drawers, Lightbulb, Mirror,
            Plant, Sofa, Table, Vase, Wardrobe
        }

        public enum WNES { West, North, East, South };
        
        public Maze Maze { get { return maze; } }
        public Cell[,] Field { get { return maze.Field; } }
        public int MazeSize { get { return maze.Size; } }
        Maze maze;

        public Dictionary<Item, XY> PointsOfInterest { get { return pointsOfInterest; } }
        Dictionary<Item, XY> pointsOfInterest;

        public List<Item> ShoppingList { get { return shoppingList; } }
        List<Item> shoppingList;

        public XY PlayerLoc { get { return playerLoc; } }
        XY playerLoc;

        public int PlayerScore { get { return playerScore; } }
        int playerScore;
        int scoreDecay;

        bool visitedCafe = false;
        bool visitedSale = false;

        // Events

        public event EventHandler PlayerMoved;
        public event EventHandler ExitReached;
        public event EventHandler<ScoreEventArgs> ScoreChanged;
        public event EventHandler SaleFound;
        public event EventHandler ItemShopped;
        public event EventHandler ScoreDecayed;

        protected virtual void OnPlayerMoved()
        {
            if (PlayerMoved != null) PlayerMoved(this, new EventArgs());
        }
        protected virtual void OnExitReached()
        {
            if (ExitReached != null) ExitReached(this, new EventArgs());
        }
        protected virtual void OnScoreChanged(ScoreEventArgs e)
        {
            if (ScoreChanged != null) ScoreChanged(this, e);
        }
        protected virtual void OnSaleFound()
        {
            if (SaleFound != null) SaleFound(this, new EventArgs());
        }
        protected virtual void OnItemShopped()
        {
            if (ItemShopped != null) ItemShopped(this, new EventArgs());
        }
        protected virtual void OnScoreDecayed()
        {
            if (ScoreDecayed != null) ScoreDecayed(this, new EventArgs()); 
        }


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
            InitTimer();

            playerLoc = new XY(0, 0);
            playerScore = 3333;
            scoreDecay = (Int32)(3333 / ((maze.Size * 4) + (Math.Pow(maze.Size / 10, 2) * 4)));
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

        // Player Input & movement

        public void MovePlayer(WNES direction)
        {
            switch (direction)
            {
                case WNES.West:
                    if (Field[playerLoc.X, playerLoc.Y].WestWall) return;

                    PlayerLoc.X--;
                    break;
                case WNES.North:
                    if (Field[playerLoc.X, playerLoc.Y].NorthWall) return;

                    playerLoc.Y--;
                    break;
                case WNES.East:
                    if (Field[playerLoc.X, playerLoc.Y].EastWall) return;

                    playerLoc.X++;
                    break;
                case WNES.South:
                    if (Field[playerLoc.X, playerLoc.Y].SouthWall) return;

                    playerLoc.Y++;
                    break;
            }
            OnPlayerMoved();
            DoPlayerLocation();
        }

        private void DoPlayerLocation()
        {
            if (!pointsOfInterest.Any(i => i.Value.X == PlayerLoc.X && i.Value.Y == playerLoc.Y)) return;

            Item item = pointsOfInterest.Single(i => i.Value.X == PlayerLoc.X && i.Value.Y == playerLoc.Y).Key;

            switch (item)
            {
                case Item.Exit:
                    OnExitReached();
                    break;
                case Item.Cafe:
                    if (visitedCafe)
                    {
                        playerScore -= 555;
                        OnScoreChanged(new ScoreEventArgs(555, false, "T I M E   D R A I N"));
                    }
                    else
                    {
                        if (rnd.NextDouble() > 0.5)
                        {
                            playerScore += 1111;
                            OnScoreChanged(new ScoreEventArgs(1111, true, "M E A T B A L L S !"));
                            visitedCafe = true;
                        }
                        else
                        {
                            playerScore -= 555;
                            OnScoreChanged(new ScoreEventArgs(555, false, "T I M E   D R A I N"));
                        }
                    }
                    break;
                case Item.Sale:
                    if (!visitedSale)
                    {
                        playerScore += 999;
                        OnScoreChanged(new ScoreEventArgs(999, true, "B A R G A I N !"));
                        OnSaleFound();
                    }
                    break;
                case Item.Bed:
                case Item.Bench:
                case Item.Chair:
                case Item.Clock:
                case Item.Drawers:
                case Item.Lightbulb:
                case Item.Mirror:
                case Item.Plant:
                case Item.Sofa:
                case Item.Table:
                case Item.Vase:
                case Item.Wardrobe:
                    if (shoppingList.Contains(item))
                    {
                        shoppingList.Remove(item);
                        playerScore += 666;
                        OnScoreChanged(new ScoreEventArgs(666, true));
                        OnItemShopped();
                    }
                    else
                    {
                        playerScore -= 333;
                        OnScoreChanged(new ScoreEventArgs(333, false));
                    }
                    break;
            }
        }

        // Timer

        private void InitTimer()
        {
            timer.Interval = 1000;
            timer.Elapsed += Timer_Tick;
            timer.Enabled = true;
            timer.Start();
        }

        public void Stop()
        {
            timer.Stop();
            timer.Enabled = false;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (playerScore - scoreDecay > 0)
            {
                playerScore -= scoreDecay;
                OnScoreDecayed();
            }
            time++;
        }
    }
}
