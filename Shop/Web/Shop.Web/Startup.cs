using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudinaryDotNet;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Shop.CloudinaryService;
using Shop.Data;
using Shop.Data.Models;
using Shop.Services;

namespace Shop.Web
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
            services.AddDbContext<ShopDbContext>(option =>
                option.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")
                    
                    )
            );
            services.AddIdentity<ShopUser, IdentityRole>()
                .AddEntityFrameworkStores<ShopDbContext>()
                .AddDefaultTokenProviders();
            services.AddTransient<IProductService, ProductService>();
            services.AddTransient<ICloudinaryService, CloudinaryService.CloudinaryService>();

            services.Configure<IdentityOptions>
                (

                    options =>
                            {
                                options.Password.RequiredLength = 3;
                                options.Password.RequireLowercase = false;
                                options.Password.RequireNonAlphanumeric = false;
                                options.Password.RequireUppercase = false;
                                options.Password.RequiredUniqueChars = 0;
                                options.Password.RequireDigit = false;

                                options.User.RequireUniqueEmail = true;

                            }
                );
            services.AddAuthorization(options =>
            {

                options.AddPolicy("Admin",
                    authBuilder =>
                    {
                        authBuilder.RequireRole("Administrators");
                    });

            });
            

            Account account = new Account(Configuration["Cloudinary:Name"], Configuration["Cloudinary:ApiKey"], Configuration["Cloudinary:ApiSecret"]);

            Cloudinary cloudinary = new Cloudinary(account);

            services.AddSingleton(cloudinary);
            

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetRequiredService<ShopDbContext>())
                {
                    context.Database.EnsureCreated();
                    if(!context.Roles.Any())
                    {
                        context.Roles.Add(new IdentityRole {
                            Name = "Admin",
                            NormalizedName = "ADMIN"
                        });
                        context.Roles.Add(new IdentityRole {
                            Name = "User",
                            NormalizedName = "USER"
                           
                        });
                        context.SaveChanges();
                    }
                }
            }

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
            app.UseHsts();
            app.UseDatabaseErrorPage();
            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
              
            });
        }
    }
}
