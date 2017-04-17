using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace FluffyCRM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }


        [Display(Name = "First Name")]
        [MaxLength(50)]
        public string FirstName { get; set; }

        [Display(Name = "Address")]
        public string Address { get; set; }

        [Display(Name = "City")]
        [MaxLength(50)]
        public string City { get; set; }

        [Display(Name = "State")]
        [MaxLength(50)]
        public string State { get; set; }

        [Display(Name = "Zip")]
        [MaxLength(10)]
        public string Zip { get; set; }

        public int? ClientID { get; set; }

        [DefaultValue(0)]
        public bool NewClient { get; set; }

        [DefaultValue(0)]
        public bool RequestInfo { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
              : base("CCSSLLC_DB", throwIfV1Schema: false)
        //  : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Client>        Clients         { get; set; }
       
        public DbSet<Ticket>        Tickets         { get; set; }
 
        public DbSet<ZipCode>       ZipCodes        { get; set; }
        public DbSet<ContactPhone>  ContactPhones   { get; set; }
        public DbSet<ContactType>   ContactTypes    { get; set; }
        public DbSet<Contact>       Contacts        { get; set; }
        public DbSet<ContactLog>    ContactLogs     { get; set; }
        public DbSet<ClientUser>    ClientUsers     { get; set; }
        public DbSet<Category>      Categories      { get; set; }
        public DbSet<TicketComment> TicketComments  { get; set; }
        

        public DbSet<AuthCode>      AuthCodes       { get; set; }

        public DbSet<ProductSolution> ProductSolutions { get; set; }

        public DbSet<JobTask>       JobTasks { get; set; }

        public DbSet<WorkProject>   WorkProjects { get; set; }

        public DbSet<TaskNote> TaskNotes { get; set; }
        public DbSet<TaskAssignment> TaskAssignments { get; set; }

        public DbSet<Employee> Employees { get; set; }

     //   public System.Data.Entity.DbSet<FluffyCRM.Models.ApplicationUser> ApplicationUsers { get; set; }
    }
}