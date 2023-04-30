using Doctor_Appointment.Data;
using Doctor_Appointment.Data.Migrations;
using Doctor_Appointment.Models;
using Doctor_Appointment.Repo;
using Doctor_Appointment.Repo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using static System.Formats.Asn1.AsnWriter;
using PayPal.Api;
using Doctor_Appointment.Clients;

namespace Doctor_Appointment
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            var connection2 = builder.Configuration.GetConnectionString("myconn");
            builder.Services.AddDbContext<MedcareDbContext>(op =>
             op.UseSqlServer(connection2));
           

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
          
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            builder.Services.AddDatabaseDeveloperPageExceptionFilter();

            builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false).AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            builder.Services.AddAuthentication().AddGoogle(options =>
            {
                IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");
                options.ClientId = googleAuthSection["ClientId"];
                options.ClientSecret = googleAuthSection["ClientSecret"];
            })

           .AddFacebook(options =>
            {
                IConfigurationSection facebookAuthSection = builder.Configuration.GetSection("Authentication:Facebook");
                options.ClientId = facebookAuthSection["ClientId"];
                options.ClientSecret = facebookAuthSection["ClientSecret"];
            });

            builder.Services.AddSingleton(x => new PaypalClient(
            builder.Configuration["Paypal:ClientId"],
            builder.Configuration["Paypal:ClientSecret"],
            builder.Configuration["Paypal:Mode"]
      )
  );

            builder.Services.AddControllersWithViews();

            //our own services

            builder.Services.AddScoped<IDoctorRepo, DoctorRepoServices>();
            builder.Services.AddScoped<IPatientRepo,PatientRepoServices>();
            builder.Services.AddScoped<IAppointmentRepo, AppointementRepoServices>();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            //Create Roles
           using (var scope =app.Services.CreateScope())
            {
                var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
                var roles = new[] { "Doctor", "Patient" };
                foreach(var role in roles)
                {
                    if(!await roleManager.RoleExistsAsync(role)) 
                        await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

          //Craete User
           using (var scope = app.Services.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                string email = "doctor@gmail.com";
                string password = "Doctor@123";

                if(await userManager.FindByEmailAsync(email) == null)
                {
                    var user=new IdentityUser();
                    user.UserName = email;
                    user.Email = email;

                    await userManager.CreateAsync(user,password);
                    await userManager.AddToRoleAsync(user, "Doctor");
                }
            }
          
            app.Run();
        }
    }
}