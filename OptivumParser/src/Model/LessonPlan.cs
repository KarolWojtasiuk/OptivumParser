using System;
using System.Collections.Generic;

namespace OptivumParser
{
    public class LessonPlan
    {
        public Dictionary<string, string> Classes { get; set; }
        public Dictionary<string, string> Teachers { get; set; }
        public Dictionary<string, string> Rooms { get; set; }
        public List<Lesson> Lessons { get; set; }
    }
}