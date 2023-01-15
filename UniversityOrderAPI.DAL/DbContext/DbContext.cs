using System.Text.RegularExpressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using UniversityOrderAPI.DAL.Models;

namespace UniversityOrderAPI.DAL;

public class UniversityOrderAPIDbContext : DbContext
{
    private static string Connection = ConnectionHelper.ConnectionString;


    public UniversityOrderAPIDbContext()
        : base(new DbContextOptionsBuilder<UniversityOrderAPIDbContext>().UseNpgsql(Connection).Options) { }

    
    public UniversityOrderAPIDbContext(DbContextOptions<UniversityOrderAPIDbContext> options)
        : base(options)
    { }
    
    
    public DbSet<Student> Students { get; set; }
    
    public DbSet<Store> Stores { get; set; }
    
    public DbSet<StudentStore> StudentsStores { get; set; }
    
    public DbSet<Client> Clients { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Manufacturer> Manufacturers { get; set; }

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