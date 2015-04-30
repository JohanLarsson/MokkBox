using NUnit.Framework;

namespace MokkBox
{
    public class NunitStuff
    {
        [TestCase("1", true, 1)]
        [TestCase("5", true, 5)]
        [TestCase("jomenvisst", false, 0)]
        public void TestCase(string s, bool canparse, int expected)
        {
            int actual;
            var tryParse = int.TryParse(s, out actual);
            Assert.AreEqual(canparse, tryParse);
            Assert.AreEqual(expected, actual);
        }
    }
}
