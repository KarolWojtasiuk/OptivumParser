using System;
using System.Text;

namespace OptivumParser
{
    public class Lesson
    {
        public int Number { get; }
        public (TimeSpan start, TimeSpan end) Period { get; }
        public int DayOfWeek { get; }
        public string Name { get; }
        public string ClassId { get; }
        public string TeacherId { get; }
        public string RoomId { get; }

        public Lesson(int number, (TimeSpan start, TimeSpan end) period, int dayOfWeek, string name, string classId, string teacherId, string roomId)
        {
            Number = number;
            Period = period;
            DayOfWeek = dayOfWeek;
            Name = name;
            ClassId = classId;
            TeacherId = teacherId;
            RoomId = roomId;
        }

        public override bool Equals(object obj)
        {
            var other = (Lesson)obj;

            if (Number != other.Number) return false;
            if (Period != other.Period) return false;
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