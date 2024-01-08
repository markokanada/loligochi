using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Media.Imaging;

namespace loligochi_classlib
{
    public class Entity
    {
        public string deadImage { get; private set; }
        public string normalImage { get; private set; }
        public string hungryImage { get; private set; }
        public string thirstyImage { get; private set; }
        public string angryImage { get; private set; }
        public string sickImage { get; private set; }

        // Audio handling placeholders
        public string deadVoice { get; private set; }
        public string normalVoice { get; private set; }
        public string sickVoice { get; private set; }
        public string hungryVoice { get; private set; }
        public string thirstyVoice { get; private set; }
        public string angryVoice { get; private set; }

        public string currentStatus { get; set; }
        public string name { get; set; }
        public string basedOn { get; set; }
        public double level { get; set; }
        public double age { get; set; }
        public double hp { get; set; }
        public double hungerLevel { get; set; }
        public double thirstLevel { get; set; }
        public bool isTheEntitySick { get; set; }
        public double entitySicknessLevel { get; set; }

        public Entity(string deadImgPath, string normalImgPath, string hungryImgPath,
                      string thirstyImgPath, string sickImagePath, string angryImgPath, string deadVoicePath,
                      string normalVoicePath, string sickVoicePath, string hungryVoicePath,
                      string thirstyVoicePath, string angryVoicePath, string currentStatus,
                      string name, string basedOn, int level, int age, int hp, int hungerLevel,
                      int thirstLevel, bool isTheEntitySick, int entitySicknessLevel)
        {
            deadImage = deadImgPath;
            sickImage = sickImagePath;
            normalImage = normalImgPath;
            hungryImage = hungryImgPath;
            thirstyImage = thirstyImgPath;
            angryImage = angryImgPath;

            deadVoice = deadVoicePath;
            normalVoice = normalVoicePath;
            sickVoice = sickVoicePath;
            hungryVoice = hungryVoicePath;
            thirstyVoice = thirstyVoicePath;
            angryVoice = angryVoicePath;
            this.basedOn = basedOn;
            this.currentStatus = currentStatus;
            this.name = name;
            this.level = level;
            this.age = age;
            this.hp = hp;
            this.hungerLevel = hungerLevel;
            this.thirstLevel = thirstLevel;
            this.isTheEntitySick = isTheEntitySick;
            this.entitySicknessLevel = entitySicknessLevel;
        }

        public void aging()
        {
            this.age++;
            this.level++;
        }
    }
}
