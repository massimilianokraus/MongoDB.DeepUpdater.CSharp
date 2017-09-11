using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Text;

namespace MongoDB.DeepUpdater.Test.Tests
{
    [TestClass]
    public class SelectAndWhereTest : BaseTestClass
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void Select_Expression_Null()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select<Department>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Select_SingleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Rector);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Select_DoubleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration)
                .Select(x => x.Rector);
        }

        [TestMethod]
        public void Select_Simple()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration);

            var fieldDefinitions = update.GetFieldDefinitions();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fdString = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Administration", fdString);
        }

        [TestMethod]
        public void Select_Deep()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Rector.Name);

            var fieldDefinitions = update.GetFieldDefinitions();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fdString = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Administration.Rector.Name", fdString);
        }

        [TestMethod]
        public void SelectArray_Single()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments);

            var fieldDefinitions = update.GetFieldDefinitions();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fdString = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Administration.Rector.Name", fdString);
        }

        [TestMethod]
        public void DeepSelect1()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.MacroArea == "Science")
                .SelectArray(x => x.Courses)
                .Where(x => x.CourseYears.Count > 1)
                .Select(x => x.Name);

            var fieldDefinitions = update.GetFieldDefinitions();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(2, fieldDefinitions.Count);

            var fdString1 = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Departments.0.Courses.2.Name", fdString1);

            var fdString2 = RenderFieldDef(fieldDefinitions[1]);
            Assert.AreEqual("Departments.1.Courses.0.Name", fdString2);
        }

        [TestMethod]
        public void DeepSelect2()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration)
                .SelectArray(x => x.Employees)
                .Where(x => x.Name.StartsWith("A"));

            var fieldDefinitions = update.GetFieldDefinitions();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(3, fieldDefinitions.Count);

            var fdString1 = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Administration.Employees.0", fdString1);

            var fdString2 = RenderFieldDef(fieldDefinitions[1]);
            Assert.AreEqual("Administration.Employees.3", fdString1);

            var fdString3 = RenderFieldDef(fieldDefinitions[2]);
            Assert.AreEqual("Administration.Employees.4", fdString3);
        }
    }
}
