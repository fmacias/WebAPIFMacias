using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using WebAPIFMacias.Models;

namespace WebAPIFMacias
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
            AssociateDataSourceAdapter(services);
            services.AddScoped<ISympleFactory<Person>, PersonsFactory>();
            services.AddScoped<IPersonsRepository, PersonsRepository>();
            services.AddControllers();
        }

        private static void AssociateDataSourceAdapter(IServiceCollection services)
        {
            services.AddDbContext<PersonsContext>(opt =>
                opt.UseInMemoryDatabase("Persons")
            );
            services.AddScoped<IPersonsDataSourceAdapter>(serviceProvider =>
                ActivatorUtilities.CreateInstance<DBPersonsDataSourceAdapter>(serviceProvider)
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
