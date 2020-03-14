using System;
using System.Linq;
using Xunit;

namespace OptivumParser.Tests
{
    public class LessonParserTests
    {
        const string TestPlanUrl = "https://karolwojtasiuk.github.io/testLessonPlan/";

        [Fact]
        public void GettingLessonsForClass()
        {
            var provider = new PlanProvider(TestPlanUrl);

            var expectedLesson = new Lesson(3, (start: TimeSpan.Parse("9:50"), end: TimeSpan.Parse("10:35")), 1, "zaj. wych.", "24", "13", "49");
            var actualLesson = LessonParser.GetLessonsForClass(TestPlanUrl, "24").Where(l => l.DayOfWeek == 1 && l.Number == 3).First();

            Assert.Equal(expectedLesson, actualLesson);
        }

        [Fact]
        public void GettingLessonsForTeacherTest()
        {
            var provider = new PlanProvider(TestPlanUrl);

            var expectedLesson = new Lesson(3, (start: TimeSpan.Parse("9:50"), end: TimeSpan.Parse("10:35")), 1, "zaj. wych.", "24", "13", "49");
            var actualLesson = LessonParser.GetLessonsForTeacher(TestPlanUrl, "13").Where(l => l.DayOfWeek == 1 && l.Number == 3).First();

            Assert.Equal(expectedLesson, actualLesson);
        }

        [Fact]
        public void GettingLessonsForRoomTest()
        {
            var provider = new PlanProvider(TestPlanUrl);

            var expectedLesson = new Lesson(3, (start: TimeSpan.Parse("9:50"), end: TimeSpan.Parse("10:35")), 1, "zaj. wych.", "24", "13", "49");
            var actualLesson = LessonParser.GetLessonsForRoom(TestPlanUrl, "49").Where(l => l.DayOfWeek == 1 && l.Number == 3).First();

            Assert.Equal(expectedLesson, actualLesson);
        }

        [Fact]
        public void GettingLessonsIntegrationTest()
        {
            var provider = new PlanProvider(TestPlanUrl);

            var lessonForClass = LessonParser.GetLessonsForClass(TestPlanUrl, "24").Where(l => l.DayOfWeek == 1 && l.Number == 6).First();
            var lessonForTeacher = LessonParser.GetLessonsForTeacher(TestPlanUrl, "14").Where(l => l.DayOfWeek == 1 && l.Number == 6).First();
            var lessonForRoom = LessonParser.GetLessonsForRoom(TestPlanUrl, "41").Where(l => l.DayOfWeek == 1 && l.Number == 6).First();

            Assert.Equal(lessonForClass, lessonForTeacher);
            Assert.Equal(lessonForClass, lessonForRoom);
            Assert.Equal(lessonForTeacher, lessonForRoom);
        }
    }
}