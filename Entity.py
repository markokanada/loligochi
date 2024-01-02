from PIL import Image

class Entity:
    def __init__(self, deadImgPath, normalImgPath, hungryImgPath, thirstyImgPath):
        self.DeadImg = Image.open(deadImgPath)
        self.NormalImg = Image.open(normalImgPath)
        self.HungryImg = Image.open(hungryImgPath)
        self.ThirstyImg = Image.open(thirstyImgPath)
        