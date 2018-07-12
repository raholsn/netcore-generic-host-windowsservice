using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Persistance
{
    public class DataContextFactory : Microsoft.EntityFrameworkCore.Design.IDesignTimeDbContextFactory<DataContext>
    {
        public DataContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
            optionsBuilder.UseSqlServer("Server = (localdb)\\mssqllocaldb; Database = Scheduler; Trusted_Connection = True; MultipleActiveResultSets = true");

            return new DataContext(optionsBuilder.Options);
        }
    }
}
