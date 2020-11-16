using ApiDesign.Api.Common;
using ApiDesign.Api.Swagger;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace ApiDesign.Api
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
            services.AddControllers();
            services.AddApiVersioning(
                            options =>
                            {
                                // response içerisinde api version bilgisini de döner
                                options.ReportApiVersions = true;
                                // options.DefaultApiVersion = new ApiVersion(1, 0);
                            });
            services.AddVersionedApiExplorer(
            options =>
            {
                //version formatını belirtmektedir v'den sonra major.minör.status formatıdır
                options.GroupNameFormat = "'v'VVV";
                //version'un url içerisinde olacağını belirtir 
                options.SubstituteApiVersionInUrl = true;
            });
            services.AddHttpContextAccessor();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            //services.AddScoped<IUrlHelper, UrlHelper>(implementationFactory =>
            //{
            //    var actionContext = implementationFactory.GetService<IActionContextAccessor>().ActionContext;
            //    return new UrlHelper(actionContext);

            //});
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    // methodlar için default value filtresi
                    options.OperationFilter<SwaggerDefaultValues>();
                });
            
            //services.AddScoped<IUrlHelper>(x =>
            //{
            //    var actionContext = x.GetRequiredService<IActionContextAccessor>().ActionContext;
            //    var factory = x.GetRequiredService<IUrlHelperFactory>();
            //    return factory.GetUrlHelper(actionContext);
            //});
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                builder =>
                {
                    builder.AllowAnyHeader()
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .Build();
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider provider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors("AllowAll");
            app.UseRouting();
            app.UseAuthorization();
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    foreach (var description in provider.ApiVersionDescriptions)
                    {
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
                    }
                });

            app.UseErrorHandlingMiddleware();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }

    public static class StartUpExtensions
    {
        public static IApplicationBuilder UseErrorHandlingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ErrorHandlingMiddleware>();
        }
    }
}
