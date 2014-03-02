using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using MallBuddyApi2.Models;
using System.Data.Entity.Infrastructure;
using System.Configuration;
using System.Data.Entity.Validation;
using System.Text;
using MallBuddyApi2.Models.existing;

namespace MallBuddyApi2.Models
{
    //public class applicationuser : identityuser
    //{
    //}

    public class ApplicationDbContext : DbContext
    {
        public DbSet<Store> Stores { get; set; }
        public DbSet<POI> POIs { get; set; }
        public DbSet<Polygone> Polygones { get; set; }
        public DbSet<Area> Areas { get; set; }
        public DbSet<OpeningHoursSpan> Schedules { get; set; }
        public DbSet<Point3D> Points { get; set; }
        public DbSet<ContactDetails> ContactDetails { get; set; }
        public DbSet<Image> Images { get; set; }

        public DbSet<Connector> Connectors { get; set; }
        //static ApplicationDbContext()
        //{
        //    Database.SetInitializer(new MySqlInitializer());
        //}

        public ApplicationDbContext()
            : base("Indoor")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public ApplicationDbContext(string dbName)
            : base(GetConnectionString(dbName))
        {
            Configuration.ProxyCreationEnabled = false;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Point3D>().Property(x => x.Longitude).HasPrecision(18, 15);
            modelBuilder.Entity<Point3D>().Property(x => x.Latitude).HasPrecision(17, 15);
        }

        public static string GetConnectionString(string dbName)
        {
            // Server=localhost;Database={0};Uid=username;Pwd=password
            var connString =
                ConfigurationManager.ConnectionStrings["MyDatabase"].ConnectionString.ToString();

            return String.Format(connString, dbName);
        }

        //public static int SaveChangesWithErrors(this DbContext context)
        //{
        //    try
        //    {
        //        return context.SaveChanges();
        //    }
        //    catch (DbEntityValidationException ex)
        //    {
        //        StringBuilder sb = new StringBuilder();

        //        foreach (var failure in ex.EntityValidationErrors)
        //        {
        //            sb.AppendFormat("{0} failed validation\n", failure.Entry.Entity.GetType());
        //            foreach (var error in failure.ValidationErrors)
        //            {
        //                sb.AppendFormat("- {0} : {1}", error.PropertyName, error.ErrorMessage);
        //                sb.AppendLine();
        //            }
        //        }

        //        throw new DbEntityValidationException(
        //            "Entity Validation Failed - errors follow:\n" +
        //            sb.ToString(), ex
        //        ); // Add the original exception as the innerException
        //    }
        //}
    }

    public class MigrationsContextFactory : IDbContextFactory<ApplicationDbContext>
    {
        public ApplicationDbContext Create()
        {
            return new ApplicationDbContext("developmentdb");
        }
    }

}