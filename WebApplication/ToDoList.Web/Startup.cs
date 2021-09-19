using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ToDoList.Business.Models;
using ToDoList.Business.Services.ToDoList;
using ToDoList.Data.Data;
using ToDoList.Data.Models.ToDoList;
using ToDoList.ProjectManage.ApiClient;
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
            var configuration = new MapperConfiguration(cfg =>
                cfg.AddMaps(new[] {
                    "ToDoList.Web",
                    "ToDoList.Business"
                })
            );
            IMapper mapper = configuration.CreateMapper();
            services.AddSingleton(mapper);

            services.AddAutoMapper(typeof(Startup));
            services.AddControllersWithViews().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore
            ); 
            //services.AddSingleton<IGenericProvider<Category>>(new InFileGenericProvider<Category>("category.json"));
            //services.AddSingleton<IGenericProvider<ToDoItem>>(new InFileGenericProvider<ToDoItem>("toDoItem.json"));
            //services.AddSingleton<IToDoItemProvider, InFileToDoItemProvider>();
            //services.AddSingleton<ICategoryProvider, InFileCategoryProvider>();
            //services.AddSingleton<IToDoItemProvider, InMemoryToDoItemProvider>();
            //services.AddSingleton<ICategoryProvider, InMemoryCategoryProvider>();
            //services.AddSingleton<IGenericProvider<Category>, GenericProvider<Category>>();
            //services.AddSingleton<IGenericProvider<ToDoItem>, GenericProvider<ToDoItem>>();
            services.AddTransient<IProviderAsync<Category>, CategoryEntityProvider>();
            services.AddTransient<IProviderAsync<ToDoItem>, ToDoItemEntityProvider>();
            services.AddTransient<IProviderAsync<Tag>, TagEntityProvider>();
            services.AddSingleton(new ApiClient("https://localhost:44327"));
                
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
