using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;

namespace MongoDB.DeepUpdater.Test
{
    [TestClass]
	public abstract class BaseTestClass
	{
		protected FilterDefinition<University> DEFAULT_FILTER;

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

        protected string RenderFieldDef(FieldDefinition<University> fd)
        {
            return fd.Render(_mongoCollection.DocumentSerializer, _mongoCollection.Settings.SerializerRegistry).ToString();
        }

        protected string RenderFieldDef<T>(FieldDefinition<University, T> fd)
        {
            return fd.Render(_mongoCollection.DocumentSerializer, _mongoCollection.Settings.SerializerRegistry).ToString();
        }
    }
}
