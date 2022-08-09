using MHQuestGenerator.Controllers;

namespace MHQuestTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void DateArrayHandleTwoDigit()
        {
            String[] result = QuestsController.genDateArray("10-11-12");
            Assert.AreEqual("10", result[0]);
            Assert.AreEqual("11", result[1]);
            Assert.AreEqual("12", result[2]);
        }

        public void DateArrayHandleZero()
        {
            String[] result = QuestsController.genDateArray("01-02-03");
            Assert.AreEqual("1", result[0]);
            Assert.AreEqual("2", result[1]);
            Assert.AreEqual("3", result[2]);
        }

    }
}