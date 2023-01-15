using Microsoft.EntityFrameworkCore;
using UniversityOrderAPI.DAL;

namespace UniversityOrderAPI.BLL.Command;

public class Command<T> where T:DbContext
{
    protected readonly T DbContext;

    // ReSharper disable once MemberCanBeProtected.Global
    public Command(T dbContext)
    {
        DbContext = dbContext;
    }
}