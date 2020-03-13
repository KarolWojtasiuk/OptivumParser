using Xunit;

namespace OptivumParser.Tests
{
    public class ParserTests
    {
        const string TestPlanUrl = "https://karolwojtasiuk.github.io/testLessonPlan/";
        [Fact]
        public void PlanValidatingTest()
        {
            var parser = new Parser();

            Assert.True(parser.IsValidPlan(TestPlanUrl).value);
            Assert.False(parser.IsValidPlan("https://google.pl/").value);
        }

        [Fact]
        public void GettingClassIdsTest()
        {
            var parser = new Parser();
            var classes = parser.GetClassIds(TestPlanUrl);

            Assert.NotEmpty(classes);
            Assert.Equal("1", classes["1a"]);
            Assert.Equal("24", classes["3bt 3"]);
        }

        [Fact]
        public void GettingTeacherIdsTest()
        {
            var parser = new Parser();
            var teachers = parser.GetTeacherIds(TestPlanUrl);

            Assert.NotEmpty(teachers);
            Assert.Equal("13", teachers["M.Filipowski (Fm)"]);
            Assert.Equal("22", teachers["A.Harkot (Ha)"]);
            Assert.Equal("11", teachers["K.Czochra (Cr)"]);
        }

        [Fact]
        public void GettingRoomIdsTest()
        {
            var parser = new Parser();
            var rooms = parser.GetRoomIds(TestPlanUrl);

            Assert.NotEmpty(rooms);
            Assert.Equal("22", rooms["17"]);
            Assert.Equal("33", rooms["sg10"]);
            Assert.Equal("50", rooms["217"]);
        }

        [Fact]
        public void GettingAllIdsTest()
        {
            var parser = new Parser();
            var ids = parser.GetAllIds(TestPlanUrl);

            Assert.Equal("14", ids.classes["1gt"]);
            Assert.Equal("14", ids.teachers["J.Frańczuk (Fr)"]);
            Assert.Equal("39", ids.rooms["oh3"]);
        }
    }
}