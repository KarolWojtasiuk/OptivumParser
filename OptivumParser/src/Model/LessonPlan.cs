using System;
using System.Collections.Generic;

namespace OptivumParser
{
    public class LessonPlan
    {
        public List<Dictionary<string, string>> Classes { get; }
        public List<Dictionary<string, string>> Teachers { get; }
        public List<Dictionary<string, string>> Rooms { get; }
        public DateTime GeneratedDate { get; }
        public DateTime EffectiveDate { get; }
    }
}