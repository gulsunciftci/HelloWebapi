using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using MiddlewarePracties.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MiddlewarePracties
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "MiddlewarePracties", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //middlewareler use ile baþlar
                //asenkron çalýþýrlar
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MiddlewarePracties v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            //app.Run();

            //Run metodu kýsa devre yaptýrýr
            //Ýlk rundan sonraki çalýþmaz
            //app.Run(async context => Console.WriteLine("MiddleWare 1"));
            //app.Run(async context => Console.WriteLine("MiddleWare 2"));

            ////app.Use()
            //app.Use(async(context,next)=>{
            //    Console.WriteLine("middleware 1 baþladý");
            //    await next.Invoke();
            //    Console.WriteLine("middleware 1 sona erdi");
            //});
            ////app.Use()
            //app.Use(async (context, next) => {
            //    Console.WriteLine("middleware 2 baþladý");
            //    await next.Invoke();
            //    Console.WriteLine("middleware 2 sona erdi");
            //});
            ////app.Use()
            //app.Use(async (context, next) => {
            //    Console.WriteLine("middleware 3 baþladý");
            //    await next.Invoke();
            //    Console.WriteLine("middleware 3 sona erdi");
            //});
            app.UseHello();

            app.Use(async (context, next) =>
            {
                Console.WriteLine("use middleware tetiklendi");
                await next.Invoke();
            });

            app.Map("/example",internalApp=>
            internalApp.Run(async context=>{
                Console.WriteLine("/example middleware tetiklendi");
                await context.Response.WriteAsync("/example middleware tetiklendi");
            
            }));


            app.MapWhen(x => x.Request.Method == "GET", internalApp =>
            {
                internalApp.Run(async context =>
                {
                    Console.WriteLine("MapWen Middlevare tetiklendi");
                    await context.Response.WriteAsync("MapWen Middlevare tetiklendi");

                });
            });


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
