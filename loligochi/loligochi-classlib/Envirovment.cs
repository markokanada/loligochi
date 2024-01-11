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
        private Entity Pet { get; set; }
        private TimeManager TimeManager { get; set; }
        private Random RandomGenerator { get; set; }

        public Envirovment(Entity pet)
        {
            Pet = pet;
            TimeManager = new TimeManager();
            RandomGenerator = new Random();
        }

        public void UpdatePetStatus()
        {
            var elapsedTime = TimeManager.GetElapsedTime().TotalSeconds;

            // Update pet's attributes based on elapsed time
            Pet.ThirstLevel += elapsedTime * 0.02;
            Pet.Age += elapsedTime / 86400;
            Pet.HP -= (Pet.IsTheEntitySick ? 5 : 1) * (elapsedTime * 0.01);

            // Set conditions
            Pet.ThirstLevel = Math.Min(100, Pet.ThirstLevel);
            Pet.HP = Math.Max(0, Pet.HP);
            Pet.CurrentStatus = Pet.IsTheEntitySick ? "Sick" : "Alive";
            if (!Pet.IsTheEntitySick)
            {
                Pet.CurrentStatus = (Pet.ThirstLevel > 50) || (Pet.HungerLevel > 50) ? "Angry" : "Happy";
            }
            Pet.CurrentStatus = Pet.HP == 0 ? "Dead" : "Alive";

        }

        public void RandomSicknessEvent()
        {
            // Randomly set sickness status
            Pet.IsTheEntitySick = RandomGenerator.Next(2) == 0;
        }


        public static bool SerializeEntity(Entity entity, string fileName)
        {
            //string filePath = Path.Combine("src/save", fileName);

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
            File.WriteAllText($"{fileName}.json", jsonString);
            return true;
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


    

