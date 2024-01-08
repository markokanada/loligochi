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
                    Pet.currentStatus = (Pet.thirstLevel > 50) || (Pet.hungerLevel > 50)? "Grumpy" : "Happy";
                }
                Pet.currentStatus = Pet.hp == 0 ? "Dead" : "Alive";

            }

            public void RandomSicknessEvent()
            {
                // Randomly set sickness status
                Pet.isTheEntitySick = RandomGenerator.Next(2) == 0;
            }


        public static string SerializeEntity(Entity entity, string fileName)
            {
                string filePath = Path.Combine("src/save", fileName);

                string jsonString = JsonSerializer.Serialize(entity, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(filePath, jsonString);

                //Console.WriteLine($"Az állat adatai kiírva a következő fájlba: {fileName}");
                return filePath;
            }

        public static Entity DeserializeEntity(string filePath)
            {
                string jsonString = File.ReadAllText(filePath);
                var entity = JsonSerializer.Deserialize<Entity>(jsonString);

                return entity!;
            }
        }


    }

