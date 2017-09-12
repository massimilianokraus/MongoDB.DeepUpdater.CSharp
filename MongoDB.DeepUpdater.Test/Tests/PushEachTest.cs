using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System.Linq;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class PushEachTest : BaseTestClass
    {
        [TestMethod]
        public void PushEach_CheckOperator()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area.Contains('i'))
                .SelectArray(x => x.Programs)
                .PushEach(
                    new[] {
                        new Program { Name = "MongoDB" },
                        new Program { Name = "MongoDB for C#" },
                        new Program { Name = "MongoDB for C# with DeepUpdater" },
                    },
                    slice: 3,
                    position: 1);

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByType, "PushUpdateDefinition", 3);
        }

        [TestMethod]
        public void PushEach_CheckResult()
        {
            var univ = FindByToken("Complete");
            Assert.AreEqual(3, univ.Departments[0].Programs.Count);
            Assert.AreEqual(1, univ.Departments[1].Programs.Count);
            Assert.AreEqual(0, univ.Departments[2].Programs.Count);
            Assert.AreEqual(2, univ.Departments[3].Programs.Count);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area.Contains('i'))
                .SelectArray(x => x.Programs)
                .PushEach(
                    new[] {
                        new Program { Name = "MongoDB for C#" },
                        new Program { Name = "MongoDB for C# with DeepUpdater" },
                    },
                    slice: 4,
                    position: 1);

            _mongoCollection.UpdateOne(x => x.Id == univ.Id, update);

            univ = FindByToken("Complete");

            Assert.AreEqual(4, univ.Departments[0].Programs.Count);
            Assert.AreEqual(3, univ.Departments[1].Programs.Count);
            Assert.AreEqual(0, univ.Departments[2].Programs.Count);
            Assert.AreEqual(4, univ.Departments[3].Programs.Count);

            Assert.AreEqual("Civil Engineering", univ.Departments[0].Programs[0].Name);
            Assert.AreEqual("MongoDB for C#", univ.Departments[0].Programs[1].Name);
            Assert.AreEqual("MongoDB for C# with DeepUpdater", univ.Departments[0].Programs[2].Name);
            Assert.AreEqual("Marine Engineering", univ.Departments[0].Programs[3].Name);

            Assert.AreEqual("Basic of Dynamics", univ.Departments[1].Programs[0].Name);
            Assert.AreEqual("MongoDB for C#", univ.Departments[1].Programs[1].Name);
            Assert.AreEqual("MongoDB for C# with DeepUpdater", univ.Departments[1].Programs[2].Name);

            Assert.AreEqual("Ancient Literature", univ.Departments[3].Programs[0].Name);
            Assert.AreEqual("MongoDB for C#", univ.Departments[3].Programs[1].Name);
            Assert.AreEqual("MongoDB for C# with DeepUpdater", univ.Departments[3].Programs[2].Name);
            Assert.AreEqual("Modern Literature", univ.Departments[3].Programs[3].Name);
        }
    }
}