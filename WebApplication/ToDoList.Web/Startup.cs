using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoList.Business.Data;
using ToDoList.Business.Models.ToDoList;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Web.Models;

namespace ToDoList.Web
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
            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews();
            //services.AddSingleton<IGenericProvider<Category>>(new InFileGenericProvider<Category>("category.json"));
            //services.AddSingleton<IGenericProvider<ToDoItem>>(new InFileGenericProvider<ToDoItem>("toDoItem.json"));
            //services.AddSingleton<IToDoItemProvider, InFileToDoItemProvider>();
            //services.AddSingleton<ICategoryProvider, InFileCategoryProvider>();
            //services.AddSingleton<IToDoItemProvider, InMemoryToDoItemProvider>();
            //services.AddSingleton<ICategoryProvider, InMemoryCategoryProvider>();
            //services.AddSingleton<IGenericProvider<Category>, GenericProvider<Category>>();
            //services.AddSingleton<IGenericProvider<ToDoItem>, GenericProvider<ToDoItem>>();
            services.AddTransient<IProviderAsync<CategoryDao>, CategoryEntityProvider>();
            services.AddTransient<IProviderAsync<ToDoItemDao>, ToDoItemEntityProvider>();
            services.AddTransient<IProviderAsync<TagDao>, TagEntityProvider>();
            services.AddDbContext<WebApplicationContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("WebApplicationContext")));
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
