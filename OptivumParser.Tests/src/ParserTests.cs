using Xunit;

namespace OptivumParser.Tests
{
    public class ParserTests
    {
        [Fact]
        public void PlanValidatingTest()
        {
            var parser = new Parser();
            Assert.True(parser.IsValidPlan("https://plan.ekonomikzamosc.pl/").value);
            Assert.False(parser.IsValidPlan("https://google.pl/").value);
        }
    }
}