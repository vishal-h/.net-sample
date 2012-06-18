using System.Data.Entity;
namespace SearchBoxSample.Models
{
    public class SampleEntities : DbContext
    {
        public DbSet<Document> Documents { get; set; }
    }
}

