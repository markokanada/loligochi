using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Windows.Media.Imaging;
using System.Text.Json.Serialization;
using System.Diagnostics;

namespace loligochi_classlib
{
    public class Entity
    {
        [JsonConstructor]
        public Entity(string deadImage, string normalImage, string hungryImage, string thirstyImage, string angryImage, string sickImage, string deadVoice, string normalVoice, string sickVoice, string hungryVoice, string thirstyVoice, string angryVoice, string currentStatus, string name, string basedOn, double level, double age, double hp, double hungerLevel, double thirstLevel, bool isTheEntitySick, double entitySicknessLevel)
        {
            this.deadImage = deadImage ?? throw new ArgumentNullException(nameof(deadImage));
            this.normalImage = normalImage ?? throw new ArgumentNullException(nameof(normalImage));
            this.hungryImage = hungryImage ?? throw new ArgumentNullException(nameof(hungryImage));
            this.thirstyImage = thirstyImage ?? throw new ArgumentNullException(nameof(thirstyImage));
            this.angryImage = angryImage ?? throw new ArgumentNullException(nameof(angryImage));
            this.sickImage = sickImage ?? throw new ArgumentNullException(nameof(sickImage));
            this.deadVoice = deadVoice ?? throw new ArgumentNullException(nameof(deadVoice));
            this.normalVoice = normalVoice ?? throw new ArgumentNullException(nameof(normalVoice));
            this.sickVoice = sickVoice ?? throw new ArgumentNullException(nameof(sickVoice));
            this.hungryVoice = hungryVoice ?? throw new ArgumentNullException(nameof(hungryVoice));
            this.thirstyVoice = thirstyVoice ?? throw new ArgumentNullException(nameof(thirstyVoice));
            this.angryVoice = angryVoice ?? throw new ArgumentNullException(nameof(angryVoice));
            this.currentStatus = currentStatus ?? throw new ArgumentNullException(nameof(currentStatus));
            this.name = name ?? throw new ArgumentNullException(nameof(name));
            this.basedOn = basedOn ?? throw new ArgumentNullException(nameof(basedOn));
            this.level = level;
            this.age = age;
            this.hp = hp;
            this.hungerLevel = hungerLevel;
            this.thirstLevel = thirstLevel;
            this.isTheEntitySick = isTheEntitySick;
            this.entitySicknessLevel = entitySicknessLevel;
        }

        public string deadImage { get;  set; }
        public string normalImage { get;  set; }
        public string hungryImage { get;  set; }
        public string thirstyImage { get;  set; }
        public string angryImage { get;  set; }
        public string sickImage { get;  set; }

        // Audio handling placeholders
        public string deadVoice { get;  set; }
        public string normalVoice { get;  set; }
        public string sickVoice { get;  set; }
        public string hungryVoice { get;  set; }
        public string thirstyVoice { get;  set; }
        public string angryVoice { get;  set; }
        private string _currentStatus;

        public string currentStatus 
        {
            get
            {
                return _currentStatus;
            } 
            set 
            {
                Trace.WriteLine(value);
                if (value == "angry" || value == "sick" || value == "thirsty" || value == "hungry" || value == "dead" || value == "normal")
                {
                    _currentStatus = value;
                }
                else throw new WrongChampPropertyException();
            } 
        }
        public string name { get; set; }
        public string basedOn { get; set; }
        private double _level;
        public double level 
        {
            get
            {
                return _level;
            }
            set
            {
                if (value >= 1 && value <= 18)
                {
                    _level = value;
                }
                
            } 
        } 
        public double age { get; set; }

        private double _hp;
        public double hp 
        {
            get
            {
                return _hp;
            }
            set
            {
                if (value >= 0)
                {
                    _hp = value;
                }
                
            } 
        }

        private double _hungerLevel;

        public double hungerLevel 
        {
            get
            {
                return _hungerLevel;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _hungerLevel = value;
                }
                else throw new WrongChampPropertyException() ;
            }
        } 
        public double thirstLevel
        {
            get
            {
                return thirstLevel;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    hungerLevel = value;
                }
                else throw new WrongChampPropertyException();
            } //TODO 0-100 közötti érték maximum
        }
		public bool isTheEntitySick { get; set; }
        public double entitySicknessLevel { get; set; }

        public void gotHappy()
        {
            if (this.currentStatus == "angry") 
            {
                currentStatus = "normal";
            }
        }
        public void aging()
        {
            this.age++;
            this.level++;
        }
    }
}
