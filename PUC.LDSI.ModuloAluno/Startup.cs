using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PUC.LDSI.DataBase.Context;
using PUC.LDSI.DataBase.Repository;
using PUC.LDSI.Domain.Repository;
using PUC.LDSI.Domain.Services;
using PUC.LDSI.Domain.Services.Interfaces;

namespace PUC.LDSI.ModuloAluno
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<AppDbContext>(opc =>
                        opc.UseSqlServer(Configuration.GetConnectionString("Conexao"),
                        x => x.MigrationsAssembly("PUC.LDSI.DataBase")));

            services.AddDbContext<SecurityContext>(opc =>
                        opc.UseSqlServer(Configuration.GetConnectionString("Conexao"), x =>
                        x.MigrationsAssembly("PUC.LDSI.DataBase")));


            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
            services.AddScoped<IProfessorRepository, ProfessorRepository>();
            services.AddScoped<IAlunoRepository, AlunoRepository>();
            services.AddScoped<IAvaliacaoRepository, AvaliacaoRepository>();
            services.AddScoped<IAvaliacaoQuestaoRepository, AvaliacaoQuestaoRepository>();
            services.AddScoped<IAvaliacaoOpcaoRepository, AvaliacaoOpcaoRepository>();
            services.AddScoped<IProvaRepository, ProvaRepository>();
            services.AddScoped<IProvaQuestaoRepository, ProvaQuestaoRepository>();
            services.AddScoped<IProvaOpcaoRepository, ProvaOpcaoRepository>();
            services.AddScoped<ITurmaRepository, TurmaRepository>();
            services.AddScoped<IDisciplinaRepository, DisciplinaRepository>();
            services.AddScoped<IPublicacaoRepository, PublicacaoRepository>();

            services.AddScoped<IProfessorService, ProfessorService>();
            services.AddScoped<IAlunoService, AlunoService>();
            services.AddScoped<ITurmaService, TurmaService>();
            services.AddScoped<IDisciplinaService, DisciplinaService>();
            services.AddScoped<IPublicacaoService, PublicacaoService>();
            services.AddScoped<IProvaService, ProvaService>();
            services.AddScoped<IProvaQuestaoService, ProvaQuestaoService>();
            services.AddScoped<IProvaOpcaoService, ProvaOpcaoService>();
            services.AddScoped<IAvaliacaoService, AvaliacaoService>();
            services.AddScoped<IAvaliacaoQuestaoService, AvaliacaoQuestaoService>();
            services.AddScoped<IAvaliacaoOpcaoService, AvaliacaoOpcaoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
