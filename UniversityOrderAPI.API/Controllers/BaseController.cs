using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using UniversityOrderAPI.BLL;
using UniversityOrderAPI.DAL;
using UniversityOrderAPI.Middleware.Auth;

namespace UniversityOrderAPI.Controllers;

public abstract class BaseApiController: ControllerBase
{
    protected readonly UniversityOrderAPIDbContext Db;
    protected readonly IOptions<Config> Config;

    public BaseApiController(UniversityOrderAPIDbContext db)
    {
        Db = db;
    }
    
    public BaseApiController(UniversityOrderAPIDbContext db, IOptions<Config> config) : this(db)
    {
        Config = config;
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