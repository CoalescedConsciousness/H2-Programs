using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using Snake;
using System.Collections.ObjectModel;

namespace Snake
{
    public class SnakeHighscore
    {
        public string Player { get; set; }
        public int Score { get; set; }

        public static void LoadHighscoreList()
        {
            if (File.Exists("snake_highscorelist.xml"))
            {


                XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>));
                using (Stream reader = new FileStream("snake_highscorelist.xml", FileMode.Open))
                {
                    List<SnakeHighscore> tempList = (List<SnakeHighscore>)serializer.Deserialize(reader);
                    SnakeGame.listHighscore.Clear();

                    foreach (var item in tempList.OrderByDescending(x => x.Score))
                        SnakeGame.listHighscore.Add(item);
                }
            }
        }

        public static void SaveHighscoreList()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<SnakeHighscore>));
            using (Stream writer = new FileStream("snake_highscorelist.xml", FileMode.Create))
            {
                ser.Serialize(writer, SnakeGame.listHighscore);
            }
        }
    }
}
