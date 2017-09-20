using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;

namespace MongoDB.DeepUpdater.Test.Models
{
    public class University
    {
        public static List<University> CreateMock()
        {
            return new List<University>
            {
                new University
                {
                    TokenForTest = "Complete",
                    Location = "Trieste",
                    Administration = new InstitutionAdministration
                    {
                        Chancellor = new Person
                        {
                            Name = "Anders Hejlsberg",
                            Age = 57,
                        },
                        Employees = new List<Person>
                        {
                            new Person { Name = "Al Middlewest" },
                            new Person { Name = "Phil Morris", Hired = new DateTime(2010, 09, 5, 00, 00, 00, DateTimeKind.Utc) },
                            new Person { Name = "Bob Marley" },
                            new Person { Name = "Alicia Boys", Hired = new DateTime(2015, 11, 30, 00, 00, 00, DateTimeKind.Utc) },
                            new Person { Name = "Alan Silvestri" },
                        }
                    },
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Area = "Engineering",
                            MacroArea = "Science",
                            Programs = new List<Program>
                            {
                                new Program
                                {
                                    Name = "Civil Engineering",
                                    Cost = 20000,
                                    Years = new List<Year>()
                                },
                                new Program
                                {
                                    Name = "Marine Engineering",
                                    Cost = 25000,
                                    Years = new List<Year>
                                    {
                                        new Year
                                        {
                                            Order = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Ship Buildings", Credits = 12 },
                                                new Class { Name = "Building Science", Credits = 9 },
                                            },
                                        },
                                    },
                                },
                                new Program
                                {
                                    Name = "Informatic Engineering",
                                    Cost = 15000,
                                    Years = new List<Year>
                                    {
                                        new Year
                                        {
                                            Order = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Analysis 1", Credits = 12 },
                                                new Class { Name = "Physics 1", Credits = 9 },
                                                new Class { Name = "Geometry", Credits = 3 },
                                            },
                                        },
                                        new Year
                                        {
                                            Order = 2,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Signals Theory", Credits = 6 },
                                                new Class { Name = "Automation 1", Credits = 6 },
                                                new Class { Name = "Physics 2", Credits = 6 },
                                                new Class { Name = "Analysis 2", Credits = 6 },
                                            },
                                        },
                                        new Year
                                        {
                                            Order = 3,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Databases", Credits = 9 },
                                                new Class { Name = "Operating Systems", Credits = 9 },
                                                new Class { Name = "Networks", Credits = 6 },
                                            },
                                        },
                                    },
                                },
                            },
                        },
                        new Department
                        {
                            Area = "Physics",
                            MacroArea = "Science",
                            Programs = new List<Program>
                            {
                                new Program
                                {
                                    Name = "Basic of Dynamics",
                                    Cost = 20000,
                                    Years = new List<Year>
                                    {
                                        new Year
                                        {
                                            Order = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Base Physics", Credits = 12 },
                                                new Class { Name = "Analysis 1", Credits = 12 },
                                            },
                                        },
                                        new Year
                                        {
                                            Order = 2,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Advanced Physics", Credits = 12 },
                                                new Class { Name = "Analysis 2", Credits = 9 },
                                            },
                                        },
                                    },
                                },
                            },
                        },
                        new Department
                        {
                            Area = "EmptyArea",
                            MacroArea = "Science",
                            Programs = new List<Program>(),
                        },
                        new Department
                        {
                            Area = "Literature",
                            MacroArea = "Humanism",
                            Programs = new List<Program>
                            {
                                new Program
                                {
                                    Name = "Ancient Literature",
                                    Cost = 17000,
                                },
                                new Program
                                {
                                    Name = "Modern Literature",
                                    Cost = 14000,
                                    Years = new List<Year>
                                    {
                                        new Year
                                        {
                                            Order = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Italian Grammar 1", Credits = 6 },
                                                new Class { Name = "Dante Alighieri", Credits = 3 },
                                                new Class { Name = "Poetry in the Middle Age 1", Credits = 6 }
                                            },
                                        },
                                    },
                                },
                            },
                        },
                    },
                },
                new University
                {
                    TokenForTest = "NullLists",
                    Location = "Narnya",
                    Administration = null,
                    Departments = null,
                },
                new University
                {
                    TokenForTest = "EmptyLists",
                    Location = "Narnya",
                    Administration = new InstitutionAdministration
                    {
                        Employees = new List<Person>(),
                    },
                    Departments = new List<Department>(),
                }
            };
        }

        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string TokenForTest { get; set; }
        public InstitutionAdministration Administration { get; set; }
        public string Location { get; set; }
        public List<Department> Departments { get; set; }
    }

    public class Department
    {
        public string Area { get; set; }
        public string MacroArea { get; set; }
        public List<Program> Programs { get; set; }
    }

    public class Program
    {
        public string Name { get; set; }
        public double Cost { get; set; }
        public List<Year> Years { get; set; }
    }

    public class Year
    {
        public int Order { get; set; }
        public List<Class> Classes { get; set; }
    }

    public class Class
    {
        public string Name { get; set; }
        public int Credits { get; set; }
    }

    public class InstitutionAdministration
    {
        public Person Chancellor { get; set; }
        public List<Person> Employees { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public DateTime Hired { get; set; }
    }
}
