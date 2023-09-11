using BudgetMaster.Data;
using BudgetMaster.Data.EntityTypes.Identity;
using BudgetMaster.ModelHelpers;
using BudgetMaster.Services;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Azure;

namespace BudgetMaster
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            
            Config config = new();
            builder.Configuration.Bind(config);
            builder.Services.AddSingleton<Config>();

            builder.Configuration.AddUserSecrets("aspnet-BudgetMaster-604df1f7-2da2-4f6d-949a-206ca720efb1");

            
            var connectionString = builder.Configuration.GetConnectionString("DbConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                if (config.Environment == AppEnvironment.Development)
                    options.Password = _devPasswordOptions;
            })
                .AddEntityFrameworkStores<AppDbContext>()
                .AddSignInManager<SignInManager<AppUser>>();

            builder.Services.AddControllersWithViews();

            
            builder.Services.AddScoped<UserService>();
            builder.Services.AddScoped<RecurringEventService>();
            builder.Services.AddScoped<ModelHelperService>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");


            app.Run();
        }

        private static readonly PasswordOptions _devPasswordOptions = new PasswordOptions()
        {
            RequireDigit = false,
            RequireNonAlphanumeric = false,
            RequiredLength = 4,
            RequireLowercase = false,
            RequireUppercase = false
        };
    }
}
