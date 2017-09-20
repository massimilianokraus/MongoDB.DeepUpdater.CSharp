using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class DeferredExecutionTest : BaseTestClass
    {
        [TestMethod]
        public void DeferredExecution_Set()
        {
            var univ = FindByToken("Complete");

            var fluent = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.MacroArea == "Science")
                .Select(x => x.Area);

            univ.Departments.RemoveAt(0);

            var update = fluent.Set("Changed");

            CheckOperator(update, PRIVATE_FIELD_UPDATES, getOperatorNameByPrivateField, "$set", 2);
        }
    }
}
