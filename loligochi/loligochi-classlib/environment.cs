using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    internal class environment
    {
        public class Pet
        {
            public string Name { get; set; }
            public double Thirst { get; set; }
            public double Age { get; set; }
            public string Mood { get; set; }
            public int Level { get; set; }
            public double Hp { get; set; }
            public bool IsSick { get; set; }
            public string Status { get; set; }

            public Pet(string name)
            {
                Name = name;
                Thirst = 0;
                Age = 0;
                Mood = "Happy";
                Level = 1;
                Hp = 100;
                IsSick = false;
                Status = "Alive";
            }
        }

        public class Environment
        {
            private Pet Pet { get; set; }
            private time_manager TimeManager { get; set; }
            private Random RandomGenerator { get; set; }

            public Environment(Pet pet)
            {
                Pet = pet;
                TimeManager = new time_manager();
                RandomGenerator = new Random();
            }

            public void UpdatePetStatus()
            {
                var elapsedTime = TimeManager.GetElapsedTime().TotalSeconds;

                // Update pet's attributes based on elapsed time
                Pet.Thirst += elapsedTime * 0.02;
                Pet.Age += elapsedTime / 86400;
                Pet.Hp -= (Pet.IsSick ? 5 : 1) * (elapsedTime * 0.01);

                // Set conditions
                Pet.Thirst = Math.Min(100, Pet.Thirst);
                Pet.Hp = Math.Max(0, Pet.Hp);
                Pet.Status = Pet.Hp == 0 ? "Dead" : "Alive";

                Pet.Mood = Pet.Thirst > 50 ? "Grumpy" : "Happy";
            }

            public void RandomSicknessEvent()
            {
                // Randomly set sickness status
                Pet.IsSick = RandomGenerator.Next(2) == 0;
            }
        }
    }
}
