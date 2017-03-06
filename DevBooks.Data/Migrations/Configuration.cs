namespace DevBooks.Data.Migrations
{
    using Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Data.Entity.Validation;
    using System.Linq;
    using System.Text;

    internal sealed class Configuration : DbMigrationsConfiguration<DevBooks.Data.DevBooksDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(DevBooks.Data.DevBooksDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            context.Books.AddOrUpdate(
              p => p.Title,
              new Book
              {
                  Title = "Modern Web Development: Understanding domains, technologies, and user experience",
                  Author = "Dino Esposito",
                  Isbn = "978-1509300013",
                  Price = 34,
                  Pages = 448,
                  Publisher = "Microsoft Press; 1 edition (March 12, 2016)",
                  Description = "This book presents a pragmatic, problem-driven, user-focused approach to planning, designing, and building dynamic web solutions. You’ll learn how to gain maximum value from Domain-Driven Design (DDD), define optimal supporting architecture, and succeed with modern UX-first design approaches. The author guides you through choosing and implementing specific technologies and addresses key user-experience topics, including mobile-friendly and responsive design.",
                  ImageName = "modern-web-development.jpg"
              }
              ,
              new Book
              {
                  Title = "ASP.NET Core and Angular 2",
                  Author = "Valerio De Sanctis",
                  Isbn = "978-1786465689",
                  Price = 44,
                  Pages = 484,
                  Publisher = "Packt Publishing (October 12, 2016)",
                  Description = "Writing code is about striking a balance between maintainability and productivity—how quickly you can write it against how much more you have to write in the future. This is a guide to doing just that by combining the impressive capabilities of ASP.NET Core and Angular 2. It shows you how to successfully manage an API and use it to support and power a dynamic single-page application.",
                  ImageName = "aspnet-core-ng2.jpg"
              },
              new Book
              {
                  Title = "Eloquent JavaScript: A Modern Introduction to Programming",
                  Author = "Marijn Haverbeke",
                  Isbn = "978-1593275846",
                  Price = 30,
                  Pages = 472,
                  Publisher = "No Starch Press; 2 edition (December 14, 2014)",
                  Description = "Eloquent JavaScript, 2nd Edition dives deep into the JavaScript language to show you how to write beautiful, effective code. Author Marijn Haverbeke immerses you in example code from the start, while exercises and full-chapter projects give you hands-on experience with writing your own programs. As you build projects such as an artificial life simulation, a simple programming language, and a paint program",
                  ImageName = "eloquent-javascript.jpg"
              }

            );
            //context.SaveChanges();
            SaveChanges(context);

        }


        



        /// <summary>
        /// Wrapper for SaveChanges adding the Validation Messages to the generated exception
        /// </summary>
        /// <param name="context">The context.</param>
        private void SaveChanges(DbContext context)
        {
            try
            {
                context.SaveChanges();
            }
            catch (DbEntityValidationException ex)
            {
                StringBuilder sb = new StringBuilder();

                foreach (var failure in ex.EntityValidationErrors)
                {
                    sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
                    foreach (var error in failure.ValidationErrors)
                    {
                        sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
                        sb.AppendLine();
                    }
                }

                throw new DbEntityValidationException(
                    "Entity Validation Failed - errors follow:\n" +
                    sb.ToString(), ex
                ); // Add the original exception as the innerException
            }
        }
    }
}
