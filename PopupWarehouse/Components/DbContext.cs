using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

public class AppDbContext : DbContext
{
    // List models here:
    //public DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        var connectionString = App.Configuration.GetConnectionString("TestConnection");
        optionsBuilder.UseSqlServer(connectionString);
    }
}
