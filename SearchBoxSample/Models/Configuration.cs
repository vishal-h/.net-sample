using System.Data.Entity.Migrations;

namespace SearchBoxSample.Models
{
    public class Configuration : DbMigrationsConfiguration<SampleEntities>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }
    }
}
