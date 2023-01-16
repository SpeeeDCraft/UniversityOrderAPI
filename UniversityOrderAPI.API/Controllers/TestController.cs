using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.Controllers;

public class SomeTestClass
{
    public int A { get; set; }
    public int D { get; set; }

    public SomeTestClass(int A, int D)
    {
        this.A = A;
        this.D = D;
    }
}

[ApiController]
[Route("[controller]")]
public class TestController:BaseApiController
{
    public TestController(UniversityOrderAPIDbContext db) : base(db)
    {
        
    }
    
    [HttpGet("{id:int}")]
    public SomeTestClass GetSomeTestClass(int id)
    {
        if (id == 1)
            return new SomeTestClass(2, 3);
        if (id == 2)
            return new SomeTestClass(5, 6);
        return new SomeTestClass(11, 11);
    }
}