from PIL import Image
from pydub import AudioSegment

class Entity:
    def __init__(self, deadImgPath, 
                 normalImgPath, hungryImgPath, 
                 thirstyImgPath, angryImgPath,
                 deadVoicePath, normalVoicePath,
                 sickVoicePath, hungryVoicePath,
                 thirstyVoicePath, angryVoicePath,
                 currentStatus, name,
                 level, age,
                 hp, hungerLevel,
                 thirstLevel, isTheEntitySick,
                 entitySicknesLevel
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
        self.CurrentStatus = currentStatus
        self.Name = name
        self.Level = level
        self.Age = age
        self.Hp = hp
        self.HungerLevel = hungerLevel
        self.ThirstLevel = thirstLevel
        self.IsTheEntitySick = isTheEntitySick
        self.EntitySicknesLevel = entitySicknesLevel
        @property
        def DeadImg(self):
            return self.DeadImg
        @property
        def NormalImg(self):
            return self.NormalImg
        @property
        def HungryImg(self):
            return self.HungryImg
        @property
        def ThirstImg(self):
            return self.ThirstImg
        @property
        def AngryImg(self):
            return self.AngryImg
        @property
        def DeadVoice(self):
            return self.DeadVoice
        @property
        def NormalVoice(self):
            return self.NormalVoice
        @property
        def SickVoice(self):
            return self.SickVoice
        @property
        def HungryVoice(self):
            return self.HungryVoice
        @property
        def ThirstyVoice(self):
            return self.ThirstyVoice
        @property
        def AngryVoice(self):
            return self.AngryVoice
        @property
        def CurrentStatus(self):
            return self.CurrentStatus
        @property
        def Name(self):
                return self.Name       
        @property
        def Level(self):
            return self.Level
        @property
        def Age(self):
            return self.Age
        @property
        def Hp(self):
            return self.Hp
        @property
        def HungerLevel(self):
            return self.HungerLevel
        @property
        def ThirstLevel(self):
            return self.ThirstLevel
        @property
        def IsTheEntitySick(self):
            return self.IsTheEntitySick
        @property
        def EntitySicknesLevel(self):
            return self.EntitySicknesLevel
        
        @CurrentStatus.setter
        def CurrentStatus(self, value):
            self.CurrentStatus = value
        
        @Name.setter
        def Name(self, value):
            self.Name = value
            
        @Level.setter
        def Level(self, value):
            self.Level = value
        
        @Age.setter
        def Age(self, value):
            self.Age = value
        
        @Hp.setter
        def Hp(self, value):
            self.Hp = value
        
        @HungerLevel.setter
        def HungerLevel(self, value):
            self.HungerLevel = value
            
        @ThirstLevel.setter
        def ThirstLevel(self, value):
            self.ThirstLevel = value
            
        @IsTheEntitySick.setter
        def IsTheEntitySick(self, value):
            self.IsTheEntitySick = value
            
        @EntitySicknesLevel.setter
        def EntitySicknesLevel(self, value):
            self.EntitySicknesLevel = value