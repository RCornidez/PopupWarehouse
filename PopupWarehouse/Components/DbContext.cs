using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;

using Models;

namespace Data
{
    public class AppDbContext : DbContext
    {
        private readonly string _connectionString;

        public AppDbContext(string connectionStringName)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory()) // Ensure this path is correct for your environment
                .AddJsonFile("appsettings.json") // Adjust if your configuration file is named or located differently
                .Build();

            _connectionString = configuration.GetConnectionString(connectionStringName);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        // Define DbSets
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<WarehouseLocation> WarehouseLocations { get; set; }
    }

   
}
