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
    }
}
