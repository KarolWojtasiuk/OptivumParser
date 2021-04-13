using System;
using System.Text;

namespace OptivumParser
{
    public record Lesson
    {
        public int Number { get; init; }
        public Period Period { get; init; }
        public int DayOfWeek { get; init; }
        public string Name { get; init; }
        public string ClassId { get; init; }
        public string TeacherId { get; init; }
        public string RoomId { get; init; }

        public override int GetHashCode()
        {
            return Number ^ DayOfWeek ^ BitConverter.ToInt32(Encoding.UTF8.GetBytes(Name), 0);
        }
    }
}