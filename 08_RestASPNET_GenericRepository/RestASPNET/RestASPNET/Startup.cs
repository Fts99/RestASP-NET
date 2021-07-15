using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestASPNET.Business;
using RestASPNET.Business.Implementations;
using RestASPNET.Model.Context;
using RestASPNET.Repository;
using RestASPNET.Repository.Generic;
using Serilog;
using System;
using System.Collections.Generic;

namespace RestASPNET
{
    public class Startup
    {

        public IConfiguration Configuration { get; }
        public IWebHostEnvironment Environment { get; set; }

        public Startup(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            Configuration = configuration;
            Environment = webHostEnvironment;
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers();

            var connection = Configuration["MySQLConnection:ConnectionString"];
            services.AddDbContext<MySQLContext>(options => options.UseMySql(connection));

            if (Environment.IsDevelopment())
            {
                MiggrateDatabae(connection);
            }

            services.AddApiVersioning();

            //Dependency
            services.AddScoped<IPersonBusiness, PersonBusinessImplementation>();
            //services.AddScoped<IPersonRepository, PersonRepositoryImplementation>();
            services.AddScoped<IBookBusiness, BookBusinessImplementation>();
            //services.AddScoped<IBookRepository, BookRepositoryImplementation>();
            services.AddScoped(typeof(IRepository<>), typeof(GenericRepository<>));
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

        private void MiggrateDatabae(string connection)
        {
            try
            {
                var evolvelConnection = new MySql.Data.MySqlClient.MySqlConnection(connection);
                var evolve = new Evolve.Evolve(evolvelConnection, msg => Log.Information(msg))
                {
                    Locations = new List<string> { "db/migrations", "db/dataset" },
                    IsEraseDisabled = true,
                };

                evolve.Migrate();
            }
            catch (Exception ex)
            {
                Log.Error("Databse Migration Failed ", ex);

                throw;
            }
        }
    }
}
