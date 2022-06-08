using API.Helpers;
using Core.Interfaces;
using Infrastruce.Data;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

namespace API
{
    public class Startup
    {
       
        public IConfiguration _config { get; }
       
        public Startup(IConfiguration config)
        {
            _config = config;
            
           
        }
        

        // This method gets called by the runtime.  Use this method to add services to the container

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<IProductRepository,ProductRepository>();
            services.AddScoped(typeof(IGenericRepository<>),(typeof(GenericRepository<>)));
            services.AddAutoMapper(
                typeof(MappingProfiles)
            );
            services.AddControllers();
            services.AddDbContext<StoreContext>(x => x.UseSqlite(_config.GetConnectionString("DefaultConnection")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP Request pipeline.

        public void configure(IApplicationBuilder app, IWebHostEnvironment env){
            if(env.IsDevelopment()){
                app.UseDeveloperExceptionPage();
                // app.UseSwagger();
                // app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json","webAPIv5 v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseStaticFiles();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}