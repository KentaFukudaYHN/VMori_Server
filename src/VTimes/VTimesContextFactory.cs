using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace VMori
{
    /// <summary>
    /// Migrationするのに必要...別プロジェクトにDbContextを置くとMigrationができず、こちらを実装する必要があった
    /// </summary>
    public class VMoriContextFactory : IDesignTimeDbContextFactory<VMoriContext>
    {
        public VMoriContext CreateDbContext(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

            var optionsBuilder = new DbContextOptionsBuilder<VMoriContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));
            return new VMoriContext(optionsBuilder.Options);
        }
    }
}
