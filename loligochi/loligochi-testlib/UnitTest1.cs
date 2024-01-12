namespace loligochi_testlib
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            TimeManager tm = new TimeManager();
            Assert.AreEqual(DateTime.Now, tm.StartTime);
        }
    }
}