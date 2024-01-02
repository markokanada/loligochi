from PIL import Image
from pydub import AudioSegment

class Entity:
    def __init__(self, deadImgPath, 
                 normalImgPath, hungryImgPath, 
                 thirstyImgPath, angryImgPath,
                 deadVoicePath, normalVoicePath,
                 sickVoicePath, hungryVoicePath,
                 thirstyVoicePath, angryVoicePath,
                 ):
        self.DeadImg = Image.open(deadImgPath)
        self.NormalImg = Image.open(normalImgPath)
        self.HungryImg = Image.open(hungryImgPath)
        self.ThirstyImg = Image.open(thirstyImgPath)
        self.AngryImg = Image.open(angryImgPath)
        self.DeadVoice = AudioSegment.from_file(deadVoicePath)
        self.NormalVoice = AudioSegment.from_file(normalVoicePath)
        self.SickVoice = AudioSegment.from_file(sickVoicePath)
        self.HungryVoice = AudioSegment.from_file(hungryVoicePath)
        self.ThirstyVoice = AudioSegment.from_file(thirstyVoicePath)
        self.AngryVoice = AudioSegment.from_file(angryVoicePath)
        