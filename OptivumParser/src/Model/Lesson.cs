using System;
using System.Text;

namespace OptivumParser
{
    public class Lesson
    {
        public int Number { get; set; }
        public Period Period { get; set; }
        public int DayOfWeek { get; set; }
        public string Name { get; set; }
        public string ClassId { get; set; }
        public string TeacherId { get; set; }
        public string RoomId { get; set; }

        public override bool Equals(object obj)
        {
            var other = (Lesson)obj;

            if (Number != other.Number) return false;
            if (Period.Start != other.Period.Start) return false;
            if (Period.End != other.Period.End) return false;
            if (DayOfWeek != other.DayOfWeek) return false;
            if (Name != other.Name) return false;
            if (ClassId != other.ClassId) return false;
            if (TeacherId != other.TeacherId) return false;
            if (RoomId != other.RoomId) return false;

            return true;
        }

        public override int GetHashCode()
        {
            return Number ^ DayOfWeek ^ BitConverter.ToInt32(Encoding.UTF8.GetBytes(Name), 0);
        }
    }
}