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
    }
}