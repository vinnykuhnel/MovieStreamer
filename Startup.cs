using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using movieapp.Model;
using Microsoft.EntityFrameworkCore;


namespace movieapp
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
            services.AddRazorPages();
            var connStr = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<MovieDbContext>(
                options => options.UseSqlServer(connStr)
            );
            services.AddHttpClient();
            services.Configure<FormOptions>(x =>
            {
                x.ValueLengthLimit = 5000; // Limit on individual form values
                x.MultipartBodyLengthLimit = 3221225472; // Limit on form body size
                x.MultipartHeadersLengthLimit = 737280000; // Limit on form header size
            });
            services.Configure<IISServerOptions>(options =>
            { 
                options.MaxRequestBodySize = 837280000; // Limit on request body size
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });
        }
    }
}
