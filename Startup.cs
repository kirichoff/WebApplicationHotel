using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using HotelBookingApp.Infrastructure.Data;

namespace WebApplication1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddDbContext<MasterDbContext>(options =>
                options.UseMySql("server=localhost;port=4406;user=repl;password=111;database=mydb;",
                    new MySqlServerVersion(new Version(8, 4,0))));

            // services.AddDbContext<SlaveDbContext>(options =>
            //     options.UseMySql(Configuration.GetConnectionString("SlaveConnection"),
            //         new MySqlServerVersion(new Version(8, 0, 21))));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });

            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
