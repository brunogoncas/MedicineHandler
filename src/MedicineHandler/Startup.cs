namespace MedicineHandler
{
    using System;
    using System.IO;
    using MedicineHandler.Application.Configuration;
    using MedicineHandler.Application.DependencyInjection;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Rewrite;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.OpenApi.Models;
    using Swashbuckle.AspNetCore.SwaggerUI;

    public sealed class Startup
    {
        private readonly ISettings settings;

        public Startup(IConfiguration configuration)
        {
            this.settings = configuration.Get<Settings>();
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(this.settings);

            services.AddApplication(this.settings);

            services.AddMvc();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Medicine Handler", Version = "v1" });

                c.IncludeXmlComments(
                    Path.Combine(
                        AppContext.BaseDirectory,
                        "MedicineHandler.xml"));
            });

            services.AddSwaggerGenNewtonsoftSupport();

            services.AddHttpContextAccessor();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (!env.IsDevelopment())
            {
                app.UseHsts();
            }

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Medicine Handler v1");
                c.DocExpansion(DocExpansion.List);
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseRewriter(new RewriteOptions().AddRedirect("^$", "swagger"));

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
