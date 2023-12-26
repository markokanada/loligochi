from abc import ABC, abstractmethod

class Viewer(ABC):
    def __init__(self, title):
        self.title = title
        super().__init__()

    @abstractmethod
    def UILoader(self, sceneID):
        """
        Load the different UI scenes by the given argument, which is an ID of a static collection.
        Implementation is in progress.
        """
        pass
    
    @abstractmethod
    def MenuLoader(self, menuID):
        """
        Load the different menus by the given argument, which is an ID of a static collection.
        Implementation is in progress.
        """
        pass  
      
    @abstractmethod
    def ImageLoader(self, image):
        """
        Load the different menus by the given argument, which is an image in a specified raw format.
        Implementation is in progress.
        """
        pass
