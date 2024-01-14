using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_testlib
{
    [TestClass]
    public class TimeManagerTests
    {
        [TestMethod]
        public void TimeManager_Constructor_SetsStartTimeToCurrentTime()
        {
            var beforeCreation = DateTime.Now;

            var timeManager = new TimeManager();
            var afterCreation = DateTime.Now;

            Assert.IsTrue(timeManager.StartTime >= beforeCreation && timeManager.StartTime <= afterCreation);
        }

        [TestMethod]
        public void TimeManager_GetCurrentTime_ReturnsCurrentTime()
        {
            var timeManager = new TimeManager();
            var beforeCall = DateTime.Now;

            var currentTime = timeManager.GetCurrentTime();
            var afterCall = DateTime.Now;

            Assert.IsTrue(currentTime >= beforeCall && currentTime <= afterCall);
        }
    }
}
