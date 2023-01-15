using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Command;

public class Command<T> where T:DbContext
{
    protected T DbContext;

    public Command(T dbContext)
    {
        DbContext = dbContext;
    }
}