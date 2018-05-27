namespace AspNetCoreMvcTest
{
    using System;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    using AspNetCoreIdentityAspNetMembershipImplementation;
    using AspNetCoreIdentityAspNetMembershipImplementation.Model;

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
            services.AddIdentity<ApplicationUser, ApplicationRole>(options => {
                options.Lockout.AllowedForNewUsers = false;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromSeconds(5);
                options.Lockout.MaxFailedAccessAttempts = 40000;
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 3;
                options.Password.RequiredUniqueChars = 1;
                options.Password.RequireLowercase = false;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
            });
            string connectionString = "Server=localhost;Database=BioImisWeb;Trusted_Connection=True;MultipleActiveResultSets = true;";
            string applicationName = "/";
            services.AddDbContext<MembershipContext>(options => options.UseSqlServer(connectionString));
            services.AddScoped<IUserStore<ApplicationUser>>(c => new UserStore(c.GetService<MembershipContext>(), applicationName));
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseAuthentication();
            app.UseStaticFiles();
            app.UseMvcWithDefaultRoute();
        }
    }
}
