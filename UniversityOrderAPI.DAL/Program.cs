// See https://aka.ms/new-console-template for more information

using UniversityOrderAPI.DAL;
using UniversityOrderAPI.DAL.Models;

Console.WriteLine("Hello, World!");

using var db = new UniversityOrderAPIDbContext();

db.Students.Add(new Student
{
    FirstName = "Николай",
    LastName = "Алексеевич",
    MiddleName = "Крючков",
    Group = "teacher",
    Year = 2022,
    Identifier = "EC7B1046-947C-4198-AF5F-6C7B46D16179",
});

db.SaveChanges();