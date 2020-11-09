using System.Collections.Generic;

namespace OptivumParser
{
    public static class LessonPlanParser
    {
        /// <summary>
        /// Experimental function, gets the whole lesson plan and parses, it may take a while.
        /// </summary>
        public static LessonPlan GetEntirePlan(PlanProvider provider)
        {
            var ids = ListParser.GetAll(provider);
            var classes = ids["classes"];
            var teachers = ids["teachers"];
            var rooms = ids["rooms"];

            var lessons = new List<Lesson>();
            foreach (var classId in classes)
            {
                lessons.AddRange(LessonParser.GetLessonsForClass(provider, classId.Value));
            }
            foreach (var teacherId in teachers)
            {
                lessons.AddRange(LessonParser.GetLessonsForTeacher(provider, teacherId.Value));
            }
            foreach (var roomId in rooms)
            {
                lessons.AddRange(LessonParser.GetLessonsForClass(provider, roomId.Value));
            }

            return new LessonPlan()
            {
                Classes = classes,
                Teachers = teachers,
                Rooms = rooms,
                Lessons = lessons
            };
        }
    }
}