using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DevBooks.Models;
using System.Data.Entity;
using System.Configuration;

namespace DevBooks.Data
{
    public class DevBooksDbContext : DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; }

        
        public DevBooksDbContext() : base("name=DefaultConnection") { }


        private void ApplyRules()
        {
            foreach (var entry in ChangeTracker.Entries().Where(
                e => e.Entity is IAuditInfo && (e.State == EntityState.Added) || (e.State == EntityState.Modified)))
            {
                IAuditInfo e = (IAuditInfo)entry.Entity;
                if (entry.State == EntityState.Added)
                {
                    e.CreatedOn = DateTime.Now;
                }

                e.ModifiedOn = DateTime.Now;

            }
        }

        public override int SaveChanges()
        {
            ApplyRules();
            return base.SaveChanges();
        }

        // To customize the model builder configurations
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    modelBuilder.Configurations.Add(new HomeConfiguration());
        //    base.OnModelCreating(modelBuilder);
        //}
    }
}
