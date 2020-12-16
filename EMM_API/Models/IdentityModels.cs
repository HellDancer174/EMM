using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.Common;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace EMM_API.Models
{
    // В профиль пользователя можно добавить дополнительные данные, если указать больше свойств для класса ApplicationUser. Подробности см. на странице https://go.microsoft.com/fwlink/?LinkID=317594.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {

        }
        public ApplicationUser(string username, string email, double rate, string position, string qualification )
        {
            var qualificator = new Qualificator();
            UserName = username;
            Email = email;
            Rate = rate;
            Position = position;
            QualificationClass = qualificator.CreateID(qualification, position);
        }
        public double Rate { get; set; }

        public string Position { get; set; }

        public int QualificationClass { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Обратите внимание, что authenticationType должен совпадать с типом, определенным в CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Здесь добавьте настраиваемые утверждения пользователя
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
        }


        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //    modelBuilder.Entity<ApplicationUser>().ToTable("dbo.AspNetUsers");
        //    modelBuilder.Entity<IdentityUserRole>().ToTable("dbo.AspNetUserRoles");
        //    modelBuilder.Entity<IdentityUserLogin>().ToTable("dbo.AspNetUserLogins");
        //    modelBuilder.Entity<IdentityUserClaim>().ToTable("dbo.AspNetUserClaims");

        //}
    }
}