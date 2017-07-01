using System;
using Xunit;
using shopping_kart;

namespace test
{
    public class UnitTest1
    {
        [Fact]
        public void Should_have_the_answer_to_life_the_universe_and_everything()
        {
            Assert.Equal(42, Class1.answerToLifeTheUniverseAndEverything());
        }
    }
}
