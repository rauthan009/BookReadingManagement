namespace BookReadingEvent.Data.Migrations
{
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class DbMigrationsConfig : DbMigrationsConfiguration<BookReadingEvent.Data.ApplicationDbContext>
    {
        public DbMigrationsConfig()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

            
        /// <summary>
        /// This method will be called after migrating to the latest version, fills a default hardcoded data into the database
        /// </summary>
        /// <param name="context">context variable for the database</param>
        protected override void Seed(BookReadingEvent.Data.ApplicationDbContext context)
        {
          //Seed initial data only if the user is empty
          if (!context.Users.Any())
            {
                var adminEmail = "myadmin@bookevents.com";
                var adminUserName = adminEmail;
                var adminFullName = "System Administrator";
                var adminPassword = "@newpassword";
                string adminRole = "Administrator";

                CreateAdminUser(context, adminEmail, adminUserName, adminFullName, adminPassword, adminRole);
                CreateSeveralEvents(context);
            }
        }

        private void CreateAdminUser(ApplicationDbContext context, string adminEmail, string adminUserName, string adminFullName, string adminPassword, string adminRole)
        {
            // Create the "admin" user
            var adminUser = new ApplicationUser
            {
                UserName = adminUserName,
                FullName = adminFullName,
                Email = adminEmail
            };
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            userManager.PasswordValidator = new PasswordValidator
            {
                RequiredLength = 5,
                RequireNonLetterOrDigit = true,
                RequireDigit = false,
                RequireLowercase = false,
                RequireUppercase = false,
            };
            var userCreateResult = userManager.Create(adminUser, adminPassword);
            if (!userCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", userCreateResult.Errors));
            }

            // Create the "Administrator" role
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var roleCreateResult = roleManager.Create(new IdentityRole(adminRole));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            roleCreateResult = roleManager.Create(new IdentityRole("Volunteer"));
            if (!roleCreateResult.Succeeded)
            {
                throw new Exception(string.Join("; ", roleCreateResult.Errors));
            }

            // Add the "admin" user to "Administrator" role
            var addAdminRoleResult = userManager.AddToRole(adminUser.Id, adminRole);
            if (!addAdminRoleResult.Succeeded)
            {
                throw new Exception(string.Join("; ", addAdminRoleResult.Errors));
            }
        }


        private void CreateSeveralEvents(ApplicationDbContext context)
        {
            context.Events.Add(new Event()
            {
                Title = "Got Book Reading Event",
                DateAndTime = DateTime.Now.Date.AddDays(5).AddHours(21).AddMinutes(30),
                Description = "This Event is for Game of Thrones enthusiasts ",
                Location = "Gurgaon",
                Comments = new HashSet<Comment>() {
                    new Comment() { Text = "comment" },
                    new Comment() { Text = "another comment", Author = context.Users.First() },
                    new Comment() { Text = "Another another comment", Author = context.Users.First() },
                    new Comment() { Text = "superanother comment" },
                    new Comment() { Text = "User comment", Author = context.Users.First() },
                }

            });

            context.Events.Add(new Event()
            {
                Title = "LOTR ",
                DateAndTime = DateTime.Now.Date.AddDays(7).AddHours(23).AddMinutes(00),
                Location = "Delhi",
            });

            context.Events.Add(new Event()
            {
                Title = "Harry Potter",
                DateAndTime = DateTime.Now.Date.AddDays(8).AddHours(22).AddMinutes(15),
                Location = "IIT Delhi",
            });

            context.Events.Add(new Event()
            {
                Title = "Dan Brown Series",
                DateAndTime = DateTime.Now.Date.AddDays(-2).AddHours(10).AddMinutes(30),
                Location = "IIT Delhi",
                Duration = 1,

              
            });

            context.Events.Add(new Event()
            {
                Title = "GOT",
                DateAndTime = DateTime.Now.Date.AddDays(-10).AddHours(18).AddMinutes(00),
                Location = "Udyog Vihar",
                Duration = 3,

            });

            context.Events.Add(new Event()
            {
                Title = "Chetan Bhagat's Series",
                DateAndTime = DateTime.Now.Date.AddDays(-2).AddHours(12).AddMinutes(0),
                Location = "Udyog Vihar",

            });

         context.SaveChanges();
        }
    }
}
