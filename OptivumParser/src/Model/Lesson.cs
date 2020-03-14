using System;
using System.Collections.Generic;

namespace OptivumParser
{
    public class Lesson
    {
        public int Number { get; }
        public (TimeSpan start, TimeSpan end) Periods { get; }
        public int DayOfWeek { get; }
        public string Name { get; }
        public string ClassId { get; }
        public string TeacherId { get; }
        public string RoomId { get; }

        public Lesson(int number, (TimeSpan start, TimeSpan end) periods, int dayOfWeek, string name, string classId, string teacherId, string roomId)
        {
            Number = number;
            Periods = periods;
            DayOfWeek = dayOfWeek;
            Name = name;
            ClassId = classId;
            TeacherId = teacherId;
            RoomId = roomId;
        }
    }
}