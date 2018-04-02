using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace IKEA
{
    public class Highscores
    {
        public List<Score> Scores { get { return scores; } }
        List<Score> scores;

        public Highscores()
        {
            scores = new List<Score>();

            if (File.Exists("scores.dat"))
            {
                FileStream reader = new FileStream("scores.dat", FileMode.Open, FileAccess.Read);
                BinaryFormatter formatter = new BinaryFormatter();
                scores = (List<Score>)formatter.Deserialize(reader);
                reader.Close();
            }
        }

        private void SaveScores()
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream writer = new FileStream("scores.dat", FileMode.Create, FileAccess.Write);
            formatter.Serialize(writer, scores);
            writer.Close();
        }

        public void AddScore(string name, int score, int size, int time)
        {
            scores.Add(new Score(name, score, size, time));

            scores = scores.OrderByDescending(
                i => i.PlayerScore).ThenBy(
                i => i.PlayerName).ToList<Score>();

            SaveScores();
        }
    }
}
