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
            Pet.thirstLevel += elapsedTime * 0.02;
            Pet.age += elapsedTime / 86400;
            Pet.hp -= (Pet.isTheEntitySick ? 5 : 1) * (elapsedTime * 0.01);

            // Set conditions
            Pet.thirstLevel = Math.Min(100, Pet.thirstLevel);
            Pet.hp = Math.Max(0, Pet.hp);
            Pet.currentStatus = Pet.isTheEntitySick ? "Sick" : "Alive";
            if (!Pet.isTheEntitySick)
            {
                Pet.currentStatus = (Pet.thirstLevel > 50) || (Pet.hungerLevel > 50) ? "Grumpy" : "Happy";
            }
            Pet.currentStatus = Pet.hp == 0 ? "Dead" : "Alive";

        }

        public void RandomSicknessEvent()
        {
            // Randomly set sickness status
            Pet.isTheEntitySick = RandomGenerator.Next(2) == 0;
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


    

