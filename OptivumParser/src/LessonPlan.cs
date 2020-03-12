using System;
using System.Collections.Generic;

namespace OptivumParser
{
    public class LessonPlan
    {
        public List<Class> Classes { get; }
        public List<Teacher> Teachers { get; }
        public List<Room> Rooms { get; }
        public DateTime GeneratedDate { get; }
        public DateTime EffectiveDate { get; }
    }
}