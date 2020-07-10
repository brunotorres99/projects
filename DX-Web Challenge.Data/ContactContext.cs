using DX_Web_Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace DX_Web_Challenge.Data
{
    public class ContactContext : DbContext
    {
        public ContactContext(DbContextOptions<ContactContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Group> Groups { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Name).IsRequired();
                entity.HasMany(c => c.ContactGroups).WithOne(c => c.Contact).HasForeignKey(c => c.ContactId);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey( g=> g.Id);
                entity.Property(g => g.Name).IsRequired();
                entity.HasMany(g => g.ContactGroups).WithOne(g => g.Group).HasForeignKey(g => g.GroupId);
            });

            modelBuilder.Entity<ContactGroup>().HasKey(cg => new { cg.ContactId, cg.GroupId });


            // todo
            modelBuilder.Entity<Contact>().HasData(
                //Id definido manualmente pois vamos usar o  provedor in-memory
                new Contact { Id = 1, Name = "Antonio" },
                new Contact { Id = 2, Name = "Vitor" }
            );
        }
    }
}