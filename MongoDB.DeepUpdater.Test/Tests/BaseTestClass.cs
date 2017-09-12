using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System.Reflection;
using System;

namespace MongoDB.DeepUpdater.Test
{
    [TestClass]
	public abstract class BaseTestClass
	{
		protected FilterDefinition<University> DEFAULT_FILTER;

        protected const string PRIVATE_FIELD_UPDATES = "_updates";
        protected const string PRIVATE_FIELD_VALUES = "_values";

		protected IMongoCollection<University> _mongoCollection;
		protected List<University> _localCollection;

		public BaseTestClass()
		{
			DEFAULT_FILTER = new BsonDocument();

			initMongoCollection();
		}

        private void initMongoCollection()
		{
			_mongoCollection = new MongoClient("mongodb://10.211.55.2:27017")
				.GetDatabase("MongoDB-DeepUpdater-Test")
				.GetCollection<University>("universities");
		}

        [TestInitialize]
        public void TestInitialize()
        {
			_localCollection = University.CreateMock();

			_mongoCollection.DeleteMany(DEFAULT_FILTER);

			_mongoCollection.InsertMany(_localCollection);
        }

        protected University FindByToken(string token)
        {
            return _mongoCollection.Find(x => x.TokenForTest == token).First();
        }

        protected string RenderFieldDef(FieldDefinition<University> fieldDefinition)
        {
            return fieldDefinition
                .Render(_mongoCollection.DocumentSerializer, _mongoCollection.Settings.SerializerRegistry)
                .FieldName;
        }

        protected string RenderFieldDef<T>(FieldDefinition<University, T> fieldDefinition)
        {
            return fieldDefinition
                .Render(_mongoCollection.DocumentSerializer, _mongoCollection.Settings.SerializerRegistry)
                .FieldName;
        }

        protected void CheckOperator<T>(
            UpdateDefinition<T> update,
            string privateFieldToCheck,
            Func<UpdateDefinition<T>, string> getOperatorName,
            string expectedOperatorName,
            int expectedCount)
        {
            var field = update.GetType().GetField(privateFieldToCheck, BindingFlags.NonPublic | BindingFlags.Instance);

            var updateDefinitions = field.GetValue(update) as IEnumerable<UpdateDefinition<T>>;

            int count = 0;

            foreach (var ud in updateDefinitions)
            {
                var operatorName = getOperatorName(ud);

                Assert.AreEqual(expectedOperatorName, operatorName);

                count++;
            }

            Assert.AreEqual(expectedCount, count);
        }

        protected string getOperatorNameByType<T>(UpdateDefinition<T> ud)
        {
            var operatorCompleteName = ud.GetType().Name;

            //avoid generic number in name: "List`1" -> "List"
            var operatorName = operatorCompleteName.Substring(0, operatorCompleteName.IndexOf('`'));

            return operatorName;
        }

        protected string getOperatorNameByPrivateField<T>(UpdateDefinition<T> ud)
        {
            var operatorField = ud.GetType().GetField("_operatorName", BindingFlags.NonPublic | BindingFlags.Instance);

            var operatorName = operatorField.GetValue(ud) as string;

            return operatorName;
        }
    }
}
