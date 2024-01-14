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
    }
}