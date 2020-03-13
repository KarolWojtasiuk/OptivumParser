using System;

namespace OptivumParser
{
    public class Lesson
    {
        public int Number { get; }
        public Tuple<string, string> Periods { get; }
        public string Name { get; }
        public Class Class { get; }
        public Teacher Teacher { get; }
        public Room Room { get; }
    }
}