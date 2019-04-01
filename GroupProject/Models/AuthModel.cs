using Microsoft.AspNet.Identity.EntityFramework;

namespace GroupProject.Models
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class AuthModel : IdentityDbContext<IdentityUser>
    {
        // Your context has been configured to use a 'AuthModel' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'GroupProject.Models.AuthModel' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'AuthModel' 
        // connection string in the application configuration file.
        public AuthModel()
            : base("name=AuthModel")
        {
        }

        public static AuthModel Create()
        {
            return new AuthModel();
        }
        public virtual DbSet<BlogPost> Posts { get; set; }
    }
}