using System.Collections.Generic;

namespace OptivumParser
{
    public class Teacher
    {
        public string Id { get; }
        public string Name { get; }
        public List<Lesson> Lessons { get; }
    }
}