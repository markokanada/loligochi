﻿using System;
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
        #pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Entity(string deadImage, string normalImage, string hungryImage, string thirstyImage, string angryImage, string sickImage, string deadVoice, string normalVoice, string sickVoice, string hungryVoice, string thirstyVoice, string angryVoice, string currentStatus, string name, string basedOn, double level, double age, double hp, double hungerLevel, double thirstLevel, bool isTheEntitySick, double entitySicknessLevel, int maximumHP, int baseHP, DateTime? lastSaw)
        #pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
            this.DeadImage = deadImage ?? throw new ArgumentNullException(nameof(deadImage));
            this.NormalImage = normalImage ?? throw new ArgumentNullException(nameof(normalImage));
            this.HungryImage = hungryImage ?? throw new ArgumentNullException(nameof(hungryImage));
            this.ThirstyImage = thirstyImage ?? throw new ArgumentNullException(nameof(thirstyImage));
            this.AngryImage = angryImage ?? throw new ArgumentNullException(nameof(angryImage));
            this.SickImage = sickImage ?? throw new ArgumentNullException(nameof(sickImage));
            this.DeadVoice = deadVoice ?? throw new ArgumentNullException(nameof(deadVoice));
            this.NormalVoice = normalVoice ?? throw new ArgumentNullException(nameof(normalVoice));
            this.SickVoice = sickVoice ?? throw new ArgumentNullException(nameof(sickVoice));
            this.HungryVoice = hungryVoice ?? throw new ArgumentNullException(nameof(hungryVoice));
            this.ThirstyVoice = thirstyVoice ?? throw new ArgumentNullException(nameof(thirstyVoice));
            this.AngryVoice = angryVoice ?? throw new ArgumentNullException(nameof(angryVoice));
            this.CurrentStatus = currentStatus ?? throw new ArgumentNullException(nameof(currentStatus));
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.BasedOn = basedOn ?? throw new ArgumentNullException(nameof(basedOn));
            this.Level = level;
            this.Age = age;
            this.HungerLevel = hungerLevel;
            this.ThirstLevel = thirstLevel;
            this.IsTheEntitySick = isTheEntitySick;
            this.EntitySicknessLevel = entitySicknessLevel;
            this.MaximumHP = maximumHP; this.HP = hp;
            this.BaseHP = baseHP;
            LastSaw = lastSaw;



        }
        public int BaseHP { get; set; }
        public int MaximumHP { get; set; }
        public string DeadImage { get;  set; }
        public string NormalImage { get;  set; }
        public string HungryImage { get;  set; }
        public string ThirstyImage { get;  set; }
        public string AngryImage { get;  set; }
        public string SickImage { get;  set; }
        public string DeadVoice { get;  set; }
        public string NormalVoice { get;  set; }
        public string SickVoice { get;  set; }
        public string HungryVoice { get;  set; }
        public string ThirstyVoice { get;  set; }
        public string AngryVoice { get;  set; }
        private string _CurrentStatus;

        public DateTime? LastSaw { get; set; }

        public string CurrentStatus 
        {
            get
            {
                return _CurrentStatus;
            } 
            set 
            {
                if (value == "Angry" || value == "Sick" || value == "Thirsty" || value == "Hungry" || value == "Dead" || value == "Normal")
                {
                    _CurrentStatus = value;
                }
                else throw new WrongChampPropertyException();
            } 
        }
        public string Name { get; set; }
        public string BasedOn { get; set; }
        private double _Level;
        public double Level 
        {
            get
            {
                return _Level;
            }
            set
            {
                if (value >= 1 && value <= 18)
                {
                    _Level = value;
                }
                else if(value > 18)
                {
                    _Level = 18;
                }
                else
                {
                    throw new WrongChampPropertyException();
                }
                
            } 
        }
        private double _Age;

        public double Age
        {
            get
            {
                return _Age;
            }
            set
            {
                if (value > 0)
                {
                    _Age = value;
                }
                else
                {
                    throw new WrongChampPropertyException();
                }

            }
        }

        private double _HP;
        public double HP 
        {
            get
            {
                return _HP;
            }
            set
            {
                if (value >= 0 && value <= MaximumHP)
                {
                    _HP = value;
                }
                else if(value >= 0)
                {
                    _HP = MaximumHP;
                }
                
            } 
        }

        private double _HungerLevel;

        public double HungerLevel 
        {
            get
            {
                return _HungerLevel;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _HungerLevel = value;
                }
                else if(value >= 0)
                {
                    _HungerLevel = 100;
                }
                else if(value < 0)
                {
                    _HungerLevel = 0;
                }
                else throw new WrongChampPropertyException() ;
            }
        }
        private double _ThirstLevel;
        public double ThirstLevel
        {
            get
            {
                return _ThirstLevel;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _ThirstLevel = value;
                }
                else if (value >= 0)
                {
                    _ThirstLevel = 100;
                }
                else if (value < 0)
                {
                    _ThirstLevel = 0;
                }
                else throw new WrongChampPropertyException();
            } 
        }
		public bool IsTheEntitySick { get; set; }

        private double _EntitySicknessLevel;

        public double EntitySicknessLevel
        {
            get
            {
                return _EntitySicknessLevel;
            }
            set
            {
                if (value >= 0 && value <= 100)
                {
                    _EntitySicknessLevel = value;
                }
                else if (value >= 0)
                {
                    _EntitySicknessLevel = 100;
                }
                else if (value < 0)
                {
                    _EntitySicknessLevel = 0;
                }
                else throw new WrongChampPropertyException();
            }
        }

        

        public void GotHappy()
        {
            if (this.CurrentStatus == "Angry") 
            {
                CurrentStatus = "Normal";
            }
        }

        public void SetCurrentStatus()
        {
            if (HP == 0)
            {
                CurrentStatus = "Dead";

            }
            else if (EntitySicknessLevel > 10)
            {
                CurrentStatus = "Sick";
            }
            else if (ThirstLevel > 30)
            {
                CurrentStatus = "Thirsty";
            }
            else if (HungerLevel > 30)
            {
               CurrentStatus = "Hungry";
            }
            else
            {
                CurrentStatus = "Normal";
            }
        }
        public void GotOlder()
        {
            this.Age++;
            this.Level++;
        }
    }
}
