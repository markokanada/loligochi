from abc import ABC, abstractmethod

class Viewer(ABC):
    def __init__(self, title):
        self.title = title
        super().__init__()
