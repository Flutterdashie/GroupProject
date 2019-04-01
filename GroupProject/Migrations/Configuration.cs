using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace GroupProject.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<GroupProject.Models.AuthModel>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(GroupProject.Models.AuthModel context)
        {
            var userMgr = new UserManager<IdentityUser>(new UserStore<IdentityUser>(context));
            var roleMgr = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleMgr.RoleExists("Admin"))
            {
                roleMgr.Create(new IdentityRole("Admin"));
            }

            if (userMgr.FindByName("Admin") == null)
            {
                if (!userMgr.Create(new IdentityUser("Admin"), "testing123").Succeeded)
                {
                    throw new Exception("Everything is on fire please help user could not be created");
                }
            }

            IdentityUser target = userMgr.FindByName("Admin");
            if (target == null)
            {
                throw new Exception("I just made that user why can I not find it?");
            }

            if (!userMgr.IsInRole(target.Id, "Admin"))
            {
                if (!userMgr.AddToRole(target.Id, "Admin").Succeeded)
                {
                    throw new Exception("Please not this again. User created but not added to role.");
                }
            }

        }
    }
}
