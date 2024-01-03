using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    internal class TimeManager
    {
        private DateTime StartTime { get; set; }
        private DateTime LastCheck { get; set; }

        public TimeManager()
        {
            StartTime = DateTime.Now;
            LastCheck = StartTime;
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        // Returns the elapsed time since the last check.
        public TimeSpan GetElapsedTime()
        {
            DateTime now = GetCurrentTime();
            TimeSpan elapsed = now - LastCheck;
            LastCheck = now;
            return elapsed;
        }

        // Returns the total elapsed time since the start.
        public TimeSpan GetTotalElapsedTime()
        {
            return DateTime.Now - StartTime;
        }
    }
}
