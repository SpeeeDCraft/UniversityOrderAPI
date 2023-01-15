using Microsoft.AspNetCore.Mvc;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;

namespace UniversityOrderAPI.Controllers;

public abstract class BaseApiController: ControllerBase
{
    protected readonly UniversityOrderAPIDbContext Db;
    
    public BaseApiController(UniversityOrderAPIDbContext db)
    {
        Db = db;
    }

    protected int GetStudentStoreId
    {
        get
        {
            var claims =  HttpContext
                .User
                .Claims;

            return int.Parse(claims.Single(el => el.Type == "StudentStoreId").Value);
        }
    }
    
}