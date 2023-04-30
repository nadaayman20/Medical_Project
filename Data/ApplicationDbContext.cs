using Doctor_Appointment.Clients;
using Doctor_Appointment.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Doctor_Appointment.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<PaypalClient>()
        //        .HasKey(e => new {e.});
        //}
        //public DbSet<PaypalClient> paypals { get; set; }
    }
}