using MD.ExpressionPredicateBuilder.XUnitTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MD.ExpressionPredicateBuilder.XUnitTest.DbContext
{
    public class DbContextTest : Microsoft.EntityFrameworkCore.DbContext
    {
        public const string ConnectionString = "Server=MDCVW146; Database=SoftwareHistory; MultipleActiveResultSets=True; Integrated Security=True; Application Name=MD.ExpressionPredicateBuilder.XUnitTest;";

        public DbContextTest(DbContextOptions<DbContextTest> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CrmServiceLayerHistory>()
                .ToTable("CrmServiceLayerHistory")
                .HasKey(x => x.Id);

            modelBuilder.Entity<CrmServiceLayerHistory>().Property(x => x.RequestDateTime).HasColumnType("datetime");
            modelBuilder.Entity<CrmServiceLayerHistory>().Property(x => x.ResponseDateTime).HasColumnType("datetime");

            base.OnModelCreating(modelBuilder);
        }

        public DbSet<CrmServiceLayerHistory> CrmServiceLayerHistory { get; set; }
    }
}
