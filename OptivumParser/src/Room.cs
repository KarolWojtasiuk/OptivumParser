using System.Collections.Generic;

namespace OptivumParser
{
    public class Room
    {
        public string Id { get; }
        public string Name { get; }
        public List<Lesson> Lessons { get; }
    }
}