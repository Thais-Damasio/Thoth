using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.Domain.Entities;

[assembly: HostingStartup(typeof(PUC.LDSI.ModuloProfessor.Areas.Identity.IdentityHostingStartup))]
namespace PUC.LDSI.ModuloProfessor.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) =>
            {
                services.AddDbContext<SecurityContext>(opc =>
               opc.UseSqlServer(context.Configuration.GetConnectionString("Conexao"),
               x => x.MigrationsAssembly("PUC.LDSI.DataBase")));
                services.AddDefaultIdentity<Usuario>()
               .AddEntityFrameworkStores<SecurityContext>();
            });
        }
    }
}