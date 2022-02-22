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

        const string primarySource = "snake_highscorelist.xml";
        const string secondarySource = "snake_highscorelist_backup.xml";
        const string temporarySource = "snake_highscorelist_temporary.xml";

        public static void LoadHighscoreList()
        {
            if (File.Exists("snake_highscorelist.xml"))
            {


                XmlSerializer serializer = new XmlSerializer(typeof(List<SnakeHighscore>));
                using (Stream reader = new FileStream(primarySource, FileMode.Open))
                
                {
                    if (reader.Length > 0)
                    {
                        List<SnakeHighscore> tempList = (List<SnakeHighscore>)serializer.Deserialize(reader);
                        SnakeGame.listHighscore.Clear();

                        foreach (var item in tempList.OrderByDescending(x => x.Score))
                            SnakeGame.listHighscore.Add(item);
                    }
                }
            }
        }

        public static void SaveHighscoreList()
        {
            XmlSerializer ser = new XmlSerializer(typeof(ObservableCollection<SnakeHighscore>));
            using (Stream writer = new FileStream(primarySource, FileMode.Create))
            {
                ser.Serialize(writer, SnakeGame.listHighscore);
            }
        }

        public static void DeleteHighscoreList()
        {
            File.Delete(secondarySource);
            File.Copy(primarySource, secondarySource);
            File.Delete(primarySource);
            SnakeGame.listHighscore.Clear();

            using (File.Create(primarySource)); // Ensures file is closed after creation.
            
            
        }

        public static void RecoverHighscoreList()
        {   
       
            File.Copy(primarySource, temporarySource);
            File.Delete(primarySource);
            SnakeGame.listHighscore.Clear();
            File.Copy(secondarySource, primarySource);
            File.Delete(secondarySource);
            File.Copy(temporarySource, secondarySource);
            File.Delete(temporarySource);
            LoadHighscoreList();
            
        }
    }
}
