using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace loligochi_classlib
{
    public class Envirovment
    {
       
        private TimeManager TimeManager { get; set; }
        private Random RandomGenerator { get; set; }

        public Envirovment()
        {
            
            TimeManager = new TimeManager();
            RandomGenerator = new Random();
        }
        public Entity UpdatePetStatus(Entity Champion)
        {

            double elapsedTimeInMinutes = TimeManager.GetTheTimeThatElapsedTillWeStartedTheGame().TotalSeconds/60;
            if(elapsedTimeInMinutes > 5)
            {
                Champion.HungerLevel += elapsedTimeInMinutes / 5;
            }
            if (elapsedTimeInMinutes > 15)
            {
                Champion.ThirstLevel += elapsedTimeInMinutes / 15;
            }
            if (Champion.HungerLevel > 30 && Champion.ThirstLevel > 30 && elapsedTimeInMinutes > 10)
            {
                Champion.EntitySicknessLevel += elapsedTimeInMinutes / 10;
            }
            if(elapsedTimeInMinutes > 240)
            {
                Champion.Age += elapsedTimeInMinutes / 240;
                Champion.HP = ((Champion.Level + (elapsedTimeInMinutes / 240)) - Champion.Level) * 150;
                Champion.Level += elapsedTimeInMinutes / 240;
                Champion.MaximumHP = int.Parse(Champion.Level.ToString()) * 150 + Champion.BaseHP;
            }
            double HPLoss = elapsedTimeInMinutes / (double)(1 * (200 / (double)(Champion.HungerLevel == 0 || Champion.ThirstLevel == 0 ? 1 : (Champion.HungerLevel + Champion.ThirstLevel)))) / (double)Champion.EntitySicknessLevel == 0 ? 1 : Champion.EntitySicknessLevel;
            Champion.HP -= HPLoss;

            if (Champion.EntitySicknessLevel > 10)
            {
                Champion.CurrentStatus = "Sick";
            }
            else if (Champion.ThirstLevel > 30)
            {
                Champion.CurrentStatus = "Thirsty";
            }
            else if (Champion.HungerLevel > 30)
            {
                Champion.CurrentStatus = "Hungry";
            }
            else
            {
                Champion.CurrentStatus = "Normal";
            }
            
            return Champion;
        }

        public Entity UpdatePetStatusByElapsedTime(Entity Champion)
        {
            double elapsedTimeInMinutes = TimeManager.GetElapsedTime().TotalSeconds/60;
            Champion.HungerLevel += elapsedTimeInMinutes / (double) 5;
            Champion.ThirstLevel += elapsedTimeInMinutes / (double)15;
            Champion.Age += elapsedTimeInMinutes / 240;
            Champion.HP = ((Champion.Level + (elapsedTimeInMinutes / 240)) - Champion.Level) * 150;
            Champion.Level += elapsedTimeInMinutes / 240;
            Champion.MaximumHP = int.Parse(Champion.Level.ToString()) * 150 + Champion.BaseHP;

            if (Champion.HungerLevel> 30 && Champion.ThirstLevel > 30)
            {
                Champion.EntitySicknessLevel += elapsedTimeInMinutes / (double)10;
            }
            double HPLoss = elapsedTimeInMinutes / (double)(1 * (200 / (double)(Champion.HungerLevel == 0 || Champion.ThirstLevel == 0 ? 1 :(Champion.HungerLevel + Champion.ThirstLevel)))) / (double)Champion.EntitySicknessLevel == 0 ? 1 : Champion.EntitySicknessLevel;
            Champion.HP -= HPLoss;
            if(Champion.EntitySicknessLevel > 10)
            {
                Champion.CurrentStatus = "Sick";
            }
            else if(Champion.ThirstLevel > 30)
            {
                Champion.CurrentStatus = "Thirsty";
            }
            else if (Champion.HungerLevel > 30)
            {
                Champion.CurrentStatus = "Hungry";
            }
            else if (elapsedTimeInMinutes > 300)
            {
                Champion.CurrentStatus = "Angry";
            }
            else
            {
                Champion.CurrentStatus = "Normal";
            }

            
            return Champion;
        }


        public static bool SerializeEntity(Entity entity, string fileName)
        {
            if (!fileName.Contains("src/save") && !fileName.Contains("src\\save"))
            {
                fileName = Path.Combine("src/save", fileName);
            }
            if (!fileName.EndsWith(".json"))
            {
                fileName += ".json";
            }
            string jsonString = JsonSerializer.Serialize(entity, new JsonSerializerOptions { WriteIndented = true });
            if (!Directory.Exists("src"))
            {
                throw new FileMissingException();
            }
            else if (!Directory.Exists("src/save"))
            {
                Directory.SetCurrentDirectory("src");
                Directory.CreateDirectory("save");
                Directory.SetCurrentDirectory("../");
                
            }
            File.WriteAllText($"{fileName}", jsonString);
            return true;
        }

        public static List<String> GetAvaibleSaves()
        {
            Trace.WriteLine(Directory.GetCurrentDirectory());

            List<String> avaibleSaves = new List<String>();

            if (!Directory.Exists("src"))
            {
                throw new FileMissingException();
            }
            else if (!Directory.Exists("src/save"))
            {
                avaibleSaves = [];
            }
            else
            {
                avaibleSaves = Directory.GetFiles("src/save").Order().ToList();
            }

            return avaibleSaves;
        }

        public static Entity? DeserializeEntity(string filePath)
        {
            if (!File.Exists(filePath))
            {
                throw new FileMissingException();

            }

            string jsonString = File.ReadAllText(filePath);
            try
            {
                var entity = JsonSerializer.Deserialize<Entity>(jsonString);
                return entity;
            }
            catch (JsonException e)
            {
                Trace.WriteLine(e);
                throw new SerializationErrorException();
            }
            catch (Exception e)
            {
                Trace.WriteLine(e);
                throw new SerializationErrorException();

            }
        }
    }
}


    

