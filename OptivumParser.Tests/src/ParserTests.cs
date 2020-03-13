using Xunit;

namespace OptivumParser.Tests
{
    public class ParserTests
    {
        const string TestPlanUrl = "https://plan.ekonomikzamosc.pl/";
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
            Assert.NotEmpty(parser.GetClassIds(TestPlanUrl));
        }
    }
}