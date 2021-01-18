using System;
using Xunit;
using MyClassLibrary;

namespace MyClassLibrary.UnitTests
{
    public class CalculatorUnitTests
    {
        [Fact]
        public void CanAdd() {
            ICalculator calc = new Calculator();

            // Tests that the expected value equals the actual value when adding 3 and 4.
            Assert.Equal(
                7,
                calc.Add(3,4));
        }
    }
}
