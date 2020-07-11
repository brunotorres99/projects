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
            modelBuilder.Entity<Contact>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(c => c.Name).IsRequired();
                entity.HasMany(c => c.ContactGroups).WithOne(c => c.Contact).HasForeignKey(c => c.ContactId);

                // todo
                entity.Ignore(x => x.Telephones);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey( g=> g.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Name).IsRequired();
                entity.HasMany(g => g.ContactGroups).WithOne(g => g.Group).HasForeignKey(g => g.GroupId);
            });

            modelBuilder.Entity<ContactGroup>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            base.OnModelCreating(modelBuilder);
        }
    }
}