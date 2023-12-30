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