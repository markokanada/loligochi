﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace loligochi_classlib
{
    public class TimeManager
    {
        public DateTime StartTime { get; set; }
        public DateTime LastCheck { get; set; }

        public TimeManager()
        {
            StartTime = DateTime.Now;
            LastCheck = StartTime;
        }

        public DateTime GetCurrentTime()
        {
            return DateTime.Now;
        }

        public TimeSpan GetTheTimeThatElapsedTillWeStartedTheGame()
        {
            DateTime now = GetCurrentTime();
            TimeSpan elapsed = now - StartTime;
            return elapsed;
        }

        // Returns the total elapsed time since the start.
        public TimeSpan GetTotalElapsedTime()
        {
            return DateTime.Now - StartTime;
        }
    }
}
