using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Media.Imaging;

namespace loligochi_classlib
{
    internal class Entity
    {
        public BitmapImage DeadImg { get; private set; }
        public BitmapImage NormalImg { get; private set; }
        public BitmapImage HungryImg { get; private set; }
        public BitmapImage ThirstyImg { get; private set; }
        public BitmapImage AngryImg { get; private set; }

        // Audio handling placeholders
        public string DeadVoicePath { get; private set; }
        public string NormalVoicePath { get; private set; }
        public string SickVoicePath { get; private set; }
        public string HungryVoicePath { get; private set; }
        public string ThirstyVoicePath { get; private set; }
        public string AngryVoicePath { get; private set; }

        public string CurrentStatus { get; set; }
        public string Name { get; set; }
        public double Level { get; set; }
        public double Age { get; set; }
        public double Hp { get; set; }
        public double HungerLevel { get; set; }
        public double ThirstLevel { get; set; }
        public bool IsTheEntitySick { get; set; }
        public double EntitySicknessLevel { get; set; }

        public Entity(string deadImgPath, string normalImgPath, string hungryImgPath,
                      string thirstyImgPath, string angryImgPath, string deadVoicePath,
                      string normalVoicePath, string sickVoicePath, string hungryVoicePath,
                      string thirstyVoicePath, string angryVoicePath, string currentStatus,
                      string name, int level, int age, int hp, int hungerLevel,
                      int thirstLevel, bool isTheEntitySick, int entitySicknessLevel)
        {
            DeadImg = LoadImage(deadImgPath);
            NormalImg = LoadImage(normalImgPath);
            HungryImg = LoadImage(hungryImgPath);
            ThirstyImg = LoadImage(thirstyImgPath);
            AngryImg = LoadImage(angryImgPath);

            DeadVoicePath = deadVoicePath;
            NormalVoicePath = normalVoicePath;
            SickVoicePath = sickVoicePath;
            HungryVoicePath = hungryVoicePath;
            ThirstyVoicePath = thirstyVoicePath;
            AngryVoicePath = angryVoicePath;

            CurrentStatus = currentStatus;
            Name = name;
            Level = level;
            Age = age;
            Hp = hp;
            HungerLevel = hungerLevel;
            ThirstLevel = thirstLevel;
            IsTheEntitySick = isTheEntitySick;
            EntitySicknessLevel = entitySicknessLevel;
        }

        private BitmapImage LoadImage(string path)
        {
            return new BitmapImage(new Uri(path, UriKind.RelativeOrAbsolute));
        }
    }
}
