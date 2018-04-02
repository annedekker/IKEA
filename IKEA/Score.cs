using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IKEA
{
    [Serializable]
    public class Score
    {
        public string PlayerName;
        public int PlayerScore;
        public int MazeSize;
        public int TimeTaken; // in seconds
        public DateTime Date;

        public Score()
        {
            PlayerName = "URK";
            PlayerScore = -5;
            MazeSize = 8;
            TimeTaken = 360;
            Date = DateTime.Now;
        }

        public Score(string name, int score, int size, int time)
        {
            PlayerName = name;
            PlayerScore = score;
            MazeSize = size;
            TimeTaken = time;
            Date = DateTime.Now;
        }
    }
}
