using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace apiTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {

            var calculator = new Calculator();

            int result = calculator.Add(4, 3);

            Assert.AreEqual(7, result);

        }

        [TestMethod]
        public void TestMethod2()
        {

            var calculator = new Calculator();

            int result = calculator.Add(4, 3);

            Assert.AreEqual(7, result);

        }

    }
}
