import datetime

# időkezelés
class TimeManager:
    def __init__(self):
        self.start_time = datetime.datetime.now()
        self.last_check = self.start_time

    def get_current_time(self):
        return datetime.datetime.now()

    # Visszaadja az eltelt időt a legutóbbi ellenőrzés óta.
    def get_elapsed_time(self):
        now = self.get_current_time()
        elapsed = now - self.last_check
        self.last_check = now
        return elapsed

    # Visszaadja az összes eltelt időt a kezdet óta.
    def get_total_elapsed_time(self):
        return datetime.datetime.now() - self.start_time



# AIO
class VirtualPet:
    def __init__(self, hunger=0, energy=100):
        self.hunger = hunger
        self.energy = energy
        self.last_update = datetime.datetime.now()

    def update(self):
        # Frissíti a kisállat állapotát az utolsó frissítés óta eltelt idő alapján.
        now = datetime.datetime.now()
        elapsed_time = (now - self.last_update).total_seconds()

        # Az eltelt idő alapján frissítjük a kisállat éhségét és energiáját
        self.hunger += elapsed_time * 0.01  
        self.energy -= elapsed_time * 0.01 

        # Korlátok beállítása
        self.hunger = min(100, max(0, self.hunger))
        self.energy = min(100, max(0, self.energy))

        self.last_update = now

    def display_status(self):
        # Kiírja a kisállat jelenlegi állapotát.
        print(f"Hunger: {self.hunger:.2f}, Energy: {self.energy:.2f}")

