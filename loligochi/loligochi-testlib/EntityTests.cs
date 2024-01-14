namespace loligochi_testlib
{
    [TestClass]
    public class EntityTests
    {
        private Entity CreateTestEntity(DateTime? lastSaw = null)
        {
            return new Entity(
                deadImage: "deadImage",
                normalImage: "normalImage",
                hungryImage: "hungryImage",
                thirstyImage: "thirstyImage",
                angryImage: "angryImage",
                sickImage: "sickImage",
                deadVoice: "deadVoice",
                normalVoice: "normalVoice",
                sickVoice: "sickVoice",
                hungryVoice: "hungryVoice",
                thirstyVoice: "thirstyVoice",
                angryVoice: "angryVoice",
                currentStatus: "Normal",
                name: "TestEntity",
                basedOn: "BasedOnEntity",
                level: 1,
                age: 5,
                hp: 50,
                hungerLevel: 50,
                thirstLevel: 50,
                isTheEntitySick: false,
                entitySicknessLevel: 0,
                maximumHP: 100,
                baseHP: 50,
                lastSaw: lastSaw
            );
        }
        [TestMethod]
        public void EntitySicknessLevel_CannotBeSetBelowZero()
        {
            Entity entity = CreateTestEntity();
            double invalidValue = -1;

            entity.EntitySicknessLevel = invalidValue;

            Assert.AreEqual(0, entity.EntitySicknessLevel);
        }

        [TestMethod]
        public void HP_CannotExceedMaximumHP()
        {
            Entity entity = CreateTestEntity();
            entity.MaximumHP = 100;
            double overMaxHP = 150;

            entity.HP = overMaxHP;

            Assert.AreEqual(entity.MaximumHP, entity.HP);
        }

        [TestMethod]
        public void HP_CannotBeNegative()
        {
            Entity entity = CreateTestEntity();
            double negativeValue = -10;

            entity.HP = negativeValue;

            Assert.AreEqual(0, entity.HP);
        }

        [TestMethod]
        public void HP_CanBeSetWithinValidRange()
        {
            Entity entity = CreateTestEntity();
            entity.MaximumHP = 100;
            double validHP = 75;

            entity.HP = validHP;

            Assert.AreEqual(validHP, entity.HP);
        }

        [TestMethod]
        public void HungerLevel_CannotBeSetAbove100()
        {
            Entity entity = CreateTestEntity();
            double overMaxHunger = 110;

            entity.HungerLevel = overMaxHunger;

            Assert.AreEqual(100, entity.HungerLevel);
        }

        [TestMethod]
        public void ThirstLevel_CannotBeNegative()
        {
            Entity entity = CreateTestEntity();
            double negativeValue = -5;

            entity.ThirstLevel = negativeValue;

            Assert.AreEqual(0, entity.ThirstLevel);
        }

        [TestMethod]
        public void ThirstLevel_CanBeSetWithinValidRange()
        {
            Entity entity = CreateTestEntity();
            double validThirstLevel = 50;

            entity.ThirstLevel = validThirstLevel;

            Assert.AreEqual(validThirstLevel, entity.ThirstLevel);
        }

        [TestMethod]
        public void ThirstLevel_CappedAtMaximum()
        {
            Entity entity = CreateTestEntity();
            double overMaxThirstLevel = 120;

            entity.ThirstLevel = overMaxThirstLevel;

            Assert.AreEqual(100, entity.ThirstLevel);
        }

        [TestMethod]
        [ExpectedException(typeof(WrongChampPropertyException))]
        public void CurrentStatus_ThrowsExceptionWhenSettingInvalidValue()
        {
            Entity entity = CreateTestEntity();

            entity.CurrentStatus = "Invalid";
        }

        [TestMethod]
        public void Level_CannotBeLessThanMinimum()
        {
            Entity entity = CreateTestEntity();
            double invalidLevel = 0;

            entity.Level = invalidLevel;

            Assert.AreEqual(1, entity.Level);
        }

        [TestMethod]
        public void GotOlder_IncreasesAge()
        {
            Entity entity = CreateTestEntity();
            double initialAge = entity.Age;

            entity.GotOlder();

            Assert.AreEqual(initialAge + 1, entity.Age);
        }

        [TestMethod]
        public void GotOlder_IncreasesLevel()
        {
            Entity entity = CreateTestEntity();
            double initialLevel = entity.Level;

            entity.GotOlder();

            Assert.AreEqual(initialLevel + 1, entity.Level);
        }

        [TestMethod]
        public void Level_CappedAtEighteen()
        {
            Entity entity = CreateTestEntity();
            double invalidLevel = 20;

            entity.Level = invalidLevel;

            Assert.AreEqual(18, entity.Level);
        }

        [TestMethod]
        public void Constructor_SetsLastSawCorrectly()
        {
            DateTime now = DateTime.Now;
            Entity entity = CreateTestEntity(lastSaw: now);

            Assert.AreEqual(now, entity.LastSaw);
        }

        [TestMethod]
        public void GotOlder_DoesNotIncreaseLevelBeyondEighteen()
        {
            Entity entity = CreateTestEntity();
            entity.Level = 18;

            entity.GotOlder();

            Assert.AreEqual(18, entity.Level);
        }

        [TestMethod]
        public void BaseHP_CanBeSetCorrectly()
        {
            Entity entity = CreateTestEntity();
            int newBaseHP = 60;

            entity.BaseHP = newBaseHP;

            Assert.AreEqual(newBaseHP, entity.BaseHP);
        }

        [TestMethod]
        public void MaximumHP_CanBeSetCorrectly()
        {
            Entity entity = CreateTestEntity();
            int newMaximumHP = 200;

            entity.MaximumHP = newMaximumHP;

            Assert.AreEqual(newMaximumHP, entity.MaximumHP);
        }

        [TestMethod]
        public void HP_IsSetToBaseHPWhenBelow()
        {
            Entity entity = CreateTestEntity();
            entity.BaseHP = 50;
            entity.HP = 10;

            entity.HP = entity.BaseHP;

            Assert.AreEqual(entity.BaseHP, entity.HP);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentOutOfRangeException))]
        public void Age_SettingNegativeValueThrowsException()
        {
            Entity entity = CreateTestEntity();

            entity.Age = -1;
        }

        [TestMethod]
        public void Age_CanBeChanged()
        {
            Entity entity = CreateTestEntity();
            double newAge = 10;

            entity.Age = newAge;

            Assert.AreEqual(newAge, entity.Age);
        }

        [TestMethod]
        public void LastSaw_CanBeChanged()
        {
            Entity entity = CreateTestEntity();
            DateTime newLastSaw = DateTime.Now;

            entity.LastSaw = newLastSaw;

            Assert.AreEqual(newLastSaw, entity.LastSaw);
        }

        [TestMethod]
        public void CurrentStatus_SettingIllegalValueKeepsPrevious()
        {
            Entity entity = CreateTestEntity();
            entity.CurrentStatus = "Normal";
            string initialStatus = entity.CurrentStatus;

            try
            {
                entity.CurrentStatus = "IllegalValue";
            }
            catch (WrongChampPropertyException)
            {
            }

            Assert.AreEqual(initialStatus, entity.CurrentStatus);
        }

        [TestMethod]
        public void IsTheEntitySick_CanBeChanged()
        {
            Entity entity = CreateTestEntity();
            bool sickness = true;

            entity.IsTheEntitySick = sickness;

            Assert.AreEqual(sickness, entity.IsTheEntitySick);
        }

        [TestMethod]
        public void NormalImage_CanBeSetAndRetrieved()
        {
            Entity entity = CreateTestEntity();
            string image = "newNormalImage.png";

            entity.NormalImage = image;

            Assert.AreEqual(image, entity.NormalImage);
        }

    }
}