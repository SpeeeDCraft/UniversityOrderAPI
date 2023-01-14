using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;

namespace UniversityOrderAPI.DAL;

public class UniversityOrderAPIDbContext : DbContext
{
    private const string Connection = "Host=localhost;Port=5432;Database=CanStudy;Username=CanStudy;Password=870sdW9!@WsN";


    public UniversityOrderAPIDbContext()
        : base(new DbContextOptionsBuilder<UniversityOrderAPIDbContext>().UseNpgsql(Connection).Options) { }

    public UniversityOrderAPIDbContext(DbContextOptions<UniversityOrderAPIDbContext> options)
        : base(options)
    { }
    

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder
            .UseNpgsql(Connection);
        //.UseSnakeCaseNamingConvention();

    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

    }
}