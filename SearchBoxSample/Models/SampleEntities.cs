using System.Data.Entity;

namespace SearchBoxSample.Models
{
    public class SampleEntities : DbContext
    {
        public DbSet<Document> Documents { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SampleEntities, Configuration>());
        }
    }
}
