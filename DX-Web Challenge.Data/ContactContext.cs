using DX_Web_Challenge.Core.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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
                entity.Property(c => c.FirstName).IsRequired().HasMaxLength(100);
                entity.Property(c => c.LastName).IsRequired().HasMaxLength(100);
                entity.Property(p => p.RowVersion).IsConcurrencyToken();
                entity.HasMany(c => c.ContactGroups).WithOne(c => c.Contact).HasForeignKey(c => c.ContactId);

                // todo
                entity.Ignore(x => x.Telephones);
            });

            modelBuilder.Entity<Group>(entity =>
            {
                entity.HasKey( g=> g.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
                entity.Property(g => g.Name).IsRequired().HasMaxLength(100);
                entity.Property(p => p.RowVersion).IsConcurrencyToken();
                entity.HasMany(g => g.ContactGroups).WithOne(g => g.Group).HasForeignKey(g => g.GroupId);
            });

            modelBuilder.Entity<ContactGroup>(entity =>
            {
                entity.HasKey(g => g.Id);
                entity.Property(c => c.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            ChangeTracker.DetectChanges();

            var modified = ChangeTracker.Entries<IConcurrencyEntity>().Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            foreach (var item in modified)
            {
                item.Entity.RowVersion = Guid.NewGuid().ToByteArray();
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }

    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Contact>().HasData(
                new Contact
                {
                    Id = 1,
                    FirstName = "Valter",
                    LastName = "Tavares",
                    Photo = Encoding.ASCII.GetBytes("data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAUAAAAFCAYAAACNbyblAAAAHElEQVQI12P4//8/w38GIAXDIBKE0DHxgljNBAAO9TXL0Y4OHwAAAABJRU5ErkJggg==")
                },
                new Contact
                {
                    Id = 2,
                    FirstName = "Antonio",
                    LastName = "Neves"
                },
                new Contact
                {
                    Id = 3,
                    FirstName = "Vitor",
                    LastName = "Silva"
                },
                new Contact
                {
                    Id = 4,
                    FirstName = "Manuel",
                    LastName = "Marques"
                },
                new Contact
                {
                    Id = 5,
                    FirstName = "Ana",
                    LastName = "Neves"
                },
                new Contact
                {
                    Id = 6,
                    FirstName = "Carlos",
                    LastName = "Dantas"
                },
                new Contact
                {
                    Id = 7,
                    FirstName = "Luis",
                    LastName = "Palma"
                },
                new Contact
                {
                    Id = 8,
                    FirstName = "Nuno",
                    LastName = "Lopes"
                },
                new Contact
                {
                    Id = 9,
                    FirstName = "Nuno",
                    LastName = "Silva"
                },
                new Contact
                {
                    Id = 10,
                    FirstName = "João",
                    LastName = "Tavares"
                },
                new Contact
                {
                    Id = 11,
                    FirstName = "Marisa",
                    LastName = "Pinto"
                },
                new Contact
                {
                    Id = 12,
                    FirstName = "Amelia",
                    LastName = "Silva"
                },
                new Contact
                {
                    Id = 13,
                    FirstName = "Tomas",
                    LastName = "Rego"
                }
            );

            modelBuilder.Entity<Group>().HasData(
                new Group
                {
                    Id = 1,
                    Name = "Motos"
                }
            );

            modelBuilder.Entity<ContactGroup>().HasData(
                new ContactGroup
                {
                    Id = 1,
                    ContactId = 1,
                    GroupId = 1
                },
                new ContactGroup
                {
                    Id = 2,
                    ContactId = 2,
                    GroupId = 1
                }
            );
        }
    }
}