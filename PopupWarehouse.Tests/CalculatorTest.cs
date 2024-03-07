using NUnit.Framework;

namespace PopupWarehouse.Tests
{
    [TestFixture]
    public class CalculatorTests
    {
        [Test]
        public void Add_WhenCalled_ReturnsSumOfArguments()
        {
            // Arrange
            var calculator = new Calculator(); // Assuming Calculator is a class you want to test
            
            // Act
            int result = calculator.Add(5, 7); // Assuming Add is a method of Calculator
            
            // Assert
            Assert.AreEqual(12, result);
        }
    }
}
