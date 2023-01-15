using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.Controllers;

public class SomeTestClass
{
    public int A { get; set; }
    public int D { get; set; }
}

[ApiController]
[Route("[controller]")]
public class TestController:BaseApiController
{
    public TestController(UniversityOrderAPIDbContext db) : base(db)
    {
    }


    [HttpGet("some")]
    public SomeTestClass GetSomeTestClass()
    {
        return new SomeTestClass();
    }
}