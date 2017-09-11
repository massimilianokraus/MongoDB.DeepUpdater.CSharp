using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace MongoDB.DeepUpdater.Test._Various
{
    [TestClass]
    public class _DesignTests : BaseTestClass
    {
        //[TestMethod]
        public void Design()
        {
            //Expression<Func<University, bool>> filter = x => x.TestReferenceName == "Complete";
            //var univ = _mongoCollection.Find(filter).First();

            //var update = Builders<University>.Update
            //    .Deep(univ)
            //    .SelectSingle(x => x.Administration)
            //    .SelectArray(x => x.Employees)
            //    .Where(x => x.Name == "John")
            //    .Do(Builders<University>.Update.Set)
            //    .With(item);

            //_mongoCollection.UpdateOne(filter, update);
        }
    }
}
