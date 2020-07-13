using DX_Web_Challenge.Business.Interfaces;
using DX_Web_Challenge.Business.Services.Impl;
using DX_Web_Challenge.Data;
using DX_Web_Challenge.Data.Repository.Impl;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace DX_Web_Challenge.Business.Test
{
    public class BaseTest
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        protected ContactContext ContactContext { get; private set; }

        public BaseTest()
        {
            var services = new ServiceCollection();

            services.AddDbContext<ContactContext>(options => {
                options.UseSqlite("Data Source=contact.db");
            });

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
            services.AddScoped<IContactService, ContactService>();
            services.AddScoped<IGroupService, GroupService>();

            ServiceProvider = services.BuildServiceProvider();
            ContactContext = ServiceProvider.GetService<ContactContext>();

            ContactContext.Database.EnsureDeleted();
            ContactContext.Database.EnsureCreated();
        }
    }
}
