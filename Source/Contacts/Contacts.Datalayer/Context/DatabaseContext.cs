using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Contacts.DataLayer.Entities;

namespace Contacts.DataLayer.Context
{
    public class DatabaseContext : DbContext
    {
        private static string _connectionString = $@"Data Source=.;Integrated Security = true;" + "Initial Catalog=ContactDBUn;" + "MultipleActiveResultSets=True";

        public DatabaseContext() : base(_connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, DataLayer.Migrations.Configuration>());

        }
        // --> Tables 
        public DbSet<Contact> Contacts { get; set; }

    }
}
