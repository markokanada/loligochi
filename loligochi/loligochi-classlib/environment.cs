using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml;

namespace loligochi_classlib
{
    internal class environment
    {
        //////////public class Pet
        //////////{
        //////////    public string Name { get; set; }
        //////////    public double Thirst { get; set; }
        //////////    public double Age { get; set; }
        //////////    public string Mood { get; set; }
        //////////    public int Level { get; set; }
        //////////    public double Hp { get; set; }
        //////////    public bool IsSick { get; set; }
        //////////    public string Status { get; set; }

        //////////    public Pet(string name)
        //////////    {
        //////////        Name = name;
        //////////        Thirst = 0;
        //////////        Age = 0;
        //////////        Mood = "Happy";
        //////////        Level = 1;
        //////////        Hp = 100;
        //////////        IsSick = false;
        //////////        Status = "Alive";
        //////////    }
        //////////}

        public class Environment
        {
            private Entity Pet { get; set; }
            private time_manager TimeManager { get; set; }
            private Random RandomGenerator { get; set; }

            public Environment(Entity pet)
            {
                Pet = pet;
                TimeManager = new time_manager();
                RandomGenerator = new Random();
            }

            public void UpdatePetStatus()
            {
                var elapsedTime = TimeManager.GetElapsedTime().TotalSeconds;

                // Update pet's attributes based on elapsed time
                Pet.ThirstLevel += elapsedTime * 0.02;
                Pet.Age += elapsedTime / 86400;
                Pet.Hp -= (Pet.IsTheEntitySick ? 5 : 1) * (elapsedTime * 0.01);

                // Set conditions
                Pet.ThirstLevel = Math.Min(100, Pet.ThirstLevel);
                Pet.Hp = Math.Max(0, Pet.Hp);
                Pet.CurrentStatus = Pet.IsTheEntitySick ? "Sick" : "Alive";
                if (!Pet.IsTheEntitySick)
                {
                    Pet.CurrentStatus = (Pet.ThirstLevel > 50) || (Pet.HungerLevel > 50)? "Grumpy" : "Happy";
                }
                Pet.CurrentStatus = Pet.Hp == 0 ? "Dead" : "Alive";

            }

            public void RandomSicknessEvent()
            {
                // Randomly set sickness status
                Pet.IsTheEntitySick = RandomGenerator.Next(2) == 0;
            }


            private static string SerializePet(Entity pet)
            {
                string fileName = $"{DateTime.Now:yyyy-MM-dd_HH-mm-ss}_pet.json";
                string filePath = Path.Combine("src/save", fileName);

                string jsonString = JsonSerializer.Serialize(pet, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);

                Console.WriteLine($"Az állat adatai kiírva a következő fájlba: {fileName}");
                return filePath;
            }

            private static Entity DeserializePet(string filePath)
            {
                string jsonString = File.ReadAllText(filePath);
                var pet = JsonSerializer.Deserialize<Entity>(jsonString);

                return pet;
            }
        }


    }
}
