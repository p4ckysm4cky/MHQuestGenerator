using MHQuestGenerator.Controllers;
using Microsoft.EntityFrameworkCore;
using MHQuestGenerator.Models;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            // Basic test

            String[] result = QuestsController.genDateArray("10-11-12");
            Assert.AreEqual("10", result[0]);
            Assert.AreEqual("11", result[1]);
            Assert.AreEqual("12", result[2]);
        }

        [Test]
        public void DateArrayHandleZero()
        {
            // Our helper function should remove leading 0's

            String[] result = QuestsController.genDateArray("01-02-03");
            Assert.AreEqual("1", result[0]);
            Assert.AreEqual("2", result[1]);
            Assert.AreEqual("3", result[2]);
        }

        [Test]
        public async Task QuestControllerTest()
        {
            // When we are removing an invalid ID
            // we need should return a 404 status code

            var mockFactory = new Mock<IHttpClientFactory>();
            var loggerFactory = new LoggerFactory();
            var logger = new Logger<QuestsController>(loggerFactory);


            var options = new DbContextOptionsBuilder<QuestContext>()
            .UseInMemoryDatabase(databaseName: "Quest")
            .Options;
            using (var context = new QuestContext(options))
            {
                QuestsController controller = new QuestsController(mockFactory.Object, context, logger);
                var result = await controller.DeleteQuest(2) as NotFoundResult;
                Assert.NotNull(result);
                if (result != null)
                    Assert.IsTrue(result.StatusCode == 404);
            }

        }

    }
}