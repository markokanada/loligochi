import datetime
import random
from time_manager import TimeManager

class Pet:
    def __init__(self, name):
        self.name = name
        self.thirst = 0
        self.age = 0
        self.mood = "Happy"
        self.level = 1
        self.hp = 100
        self.isSick = False
        self.status = "Alive"

class Environment:
    def __init__(self, pet):
        self.pet = pet
        self.time_manager = TimeManager()

    def update_pet_status(self):
        elapsed_time = self.time_manager.get_elapsed_time().total_seconds()

        # Frissítjük a kisállat tulajdonságait az eltelt idő alapján
        self.pet.thirst += elapsed_time * 0.02
        self.pet.age += elapsed_time / 86400 
        self.pet.hp -= (5 if self.pet.isSick else 1) * (elapsed_time * 0.01)

        # állapotok beállítása
        self.pet.thirst = min(100, self.pet.thirst)
        self.pet.hp = max(0, self.pet.hp)
        self.pet.status = "Dead" if self.pet.hp == 0 else "Alive"

        self.pet.mood = "Grumpy" if self.pet.thirst > 50 else "Happy"

    def random_sickness_event(self):
        # Véletlenszerűen betegség állapotának beállítása
        self.pet.isSick = random.choice([True, False])