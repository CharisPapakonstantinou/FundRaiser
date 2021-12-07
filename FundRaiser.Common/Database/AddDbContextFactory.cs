using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace FundRaiser.Common.Database
{
    public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath($"{Environment.CurrentDirectory}/../Configs")
                .AddJsonFile("appsettings.json")
                .Build();;

            var dbContextBuilder = new DbContextOptionsBuilder();

            var connectionString = configuration.GetConnectionString("DefaultConnection");
        
            dbContextBuilder.UseSqlServer(connectionString);

            return new AppDbContext(dbContextBuilder.Options, configuration);
        }
    }
}
