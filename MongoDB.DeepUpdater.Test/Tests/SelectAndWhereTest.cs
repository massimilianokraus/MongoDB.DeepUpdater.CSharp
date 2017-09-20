using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.DeepUpdater.Test.Models;
using MongoDB.Driver;
using System;
using System.Linq;

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
        [ExpectedException(typeof(ArgumentNullException))]
        public void SelectArray_Expression_Null()
        {
            var univ = FindByToken("Complete");
            Assert.IsNotNull(univ);

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray<Department>(null);
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Select_SingleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");
            Assert.IsNotNull(univ);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Chancellor);

            update.GetFieldDefinitions().ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void Select_DoubleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");
            Assert.IsNotNull(univ);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration)
                .Select(x => x.Chancellor);

            update.GetFieldDefinitions().ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectArray_SingleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");
            Assert.IsNotNull(univ);

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration.Employees);

            update.GetFieldDefinitions().ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(NullReferenceException))]
        public void SelectArray_DoubleThroughNullProperty()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration)
                .Select(x => x.Employees);

            update.GetFieldDefinitions().ToList();
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void SelectArray_Null()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments);

            update.GetFieldDefinitions().ToList();
        }

        [TestMethod]
        public void Select_NullArray()
        {
            var univ = FindByToken("NullLists");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Departments);

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fd = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Departments", fd);
        }

        [TestMethod]
        public void Select_Simple()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .Select(x => x.Administration);

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

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
                .Select(x => x.Administration.Chancellor.Name);

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fdString = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Administration.Chancellor.Name", fdString);
        }

        [TestMethod]
        public void SelectArray_Single()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments);

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fdString = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Departments", fdString);
        }

        [TestMethod]
        public void DeepSelect1()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.MacroArea == "Science")
                .SelectArray(x => x.Programs)
                .Where(x => x.Years.Count > 1)
                .Select(x => x.Name);

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(2, fieldDefinitions.Count);

            var fdString1 = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Departments.0.Programs.2.Name", fdString1);

            var fdString2 = RenderFieldDef(fieldDefinitions[1]);
            Assert.AreEqual("Departments.1.Programs.0.Name", fdString2);
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

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(3, fieldDefinitions.Count);

            var fdString1 = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Administration.Employees.0", fdString1);

            var fdString2 = RenderFieldDef(fieldDefinitions[1]);
            Assert.AreEqual("Administration.Employees.3", fdString2);

            var fdString3 = RenderFieldDef(fieldDefinitions[2]);
            Assert.AreEqual("Administration.Employees.4", fdString3);
        }

        [TestMethod]
        public void DeepSelect3()
        {
            var univ = FindByToken("Complete");

            var update = Builders<University>.Update
                .Deep(univ)
                .SelectArray(x => x.Departments)
                .Where(x => x.Area == "Engineering")
                .SelectArray(x => x.Programs)
                .Where(x => x.Name == "Informatic Engineering")
                .SelectArray(x => x.Years)
                .Where(x => x.Order == 3)
                .SelectArray(x => x.Classes)
                .Where(x => x.Name == "Networks");

            var fieldDefinitions = update.GetFieldDefinitions().ToList();

            Assert.IsNotNull(fieldDefinitions);
            Assert.AreEqual(1, fieldDefinitions.Count);

            var fd = RenderFieldDef(fieldDefinitions[0]);
            Assert.AreEqual("Departments.0.Programs.2.Years.2.Classes.2", fd);
        }
    }
}
