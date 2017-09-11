using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
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
                        Rector = new Person { Name = "Massimiliano Kraus" },
                        Employees = new List<Person>
                        {
                            new Person { Name = "Phil Morris" },
                            new Person { Name = "Bob Marley" },
                        }
                    },
                    Departments = new List<Department>
                    {
                        new Department
                        {
                            Area = "Engineering",
                            MacroArea = "Science",
                            DegreeCourses = new List<Course>
                            {
                                new Course { Name = "Civil Engineering", CourseYears = new List<CourseYear>() },
                                new Course
                                {
                                    Name = "Marine Engineering",
                                    CourseYears = new List<CourseYear>
                                    {
                                        new CourseYear
                                        {
                                            Number = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Ship Buildings", Credits = 12 },
                                                new Class { Name = "Building Science", Credits = 9 },
                                            },
                                        },
                                    },
                                },
                                new Course
                                {
                                    Name = "Informatic Engineering",
                                    CourseYears = new List<CourseYear>
                                    {
                                        new CourseYear
                                        {
                                            Number = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Analysis 1", Credits = 12 },
                                                new Class { Name = "Physics 1", Credits = 9 },
                                                new Class { Name = "Geometry", Credits = 3 },
                                            },
                                        },
                                        new CourseYear
                                        {
                                            Number = 2,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Signals Theory", Credits = 6 },
                                                new Class { Name = "Automation 1", Credits = 6 },
                                                new Class { Name = "Physics 2", Credits = 6 },
                                                new Class { Name = "Analysis 2", Credits = 6 },
                                            },
                                        },
                                        new CourseYear
                                        {
                                            Number = 3,
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
                            DegreeCourses = new List<Course>
                            {
                                new Course
                                {
                                    Name = "Material Physics",
                                    CourseYears = new List<CourseYear>
                                    {
                                        new CourseYear
                                        {
                                            Number = 1,
                                            Classes = new List<Class>
                                            {
                                                new Class { Name = "Base Physics", Credits = 12 },
                                                new Class { Name = "Analysis 1", Credits = 12 },
                                            },
                                        },
                                        new CourseYear
                                        {
                                            Number = 2,
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
                            DegreeCourses = new List<Course>(),
                        },
                        new Department
                        {
                            Area = "Literature",
                            MacroArea = "Humanism",
                            DegreeCourses = new List<Course>
                            {
                                new Course { Name = "Ancient Literature" },
                                new Course
                                {
                                    Name = "Modern Literature",
                                    CourseYears = new List<CourseYear>
                                    {
                                        new CourseYear
                                        {
                                            Number = 1,
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
                    Administration = new InstitutionAdministration(),
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
        public List<Course> DegreeCourses { get; set; }
    }

    public class Course
    {
        public string Name { get; set; }
        public List<CourseYear> CourseYears { get; set; }
    }

    public class CourseYear
    {
        public int Number { get; set; }
        public List<Class> Classes { get; set; }
    }

    public class Class
    {
        public string Name { get; set; }
        public int Credits { get; set; }
        public string Program { get; set; }
    }

    public class InstitutionAdministration
    {
        public Person Rector { get; set; }
        public List<Person> Employees { get; set; }
    }

    public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }
}
