namespace WebApiProject.DAL.Infrastructure
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using WebApiProject.DAL.Models;

    public class WebApiProjectDbContext : DbContext
    {
        // Your context has been configured to use a 'WebApiProjectDbContext' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'WebApiProject.DAL.Infrastructure.WebApiProjectDbContext' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'WebApiProjectDbContext' 
        // connection string in the application configuration file.
        public WebApiProjectDbContext() : base("DefaultConnection")
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public WebApiProjectDbContext(string Constring)
            : base(Constring)
        {
            Configuration.ProxyCreationEnabled = false;
            Configuration.LazyLoadingEnabled = false;
        }

        public static WebApiProjectDbContext Create(string Constring)
        {
            return new WebApiProjectDbContext(Constring);
        }

        public DbSet<PersonalDetail> PersonalDetails { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        // public virtual DbSet<MyEntity> MyEntities { get; set; }
    }
}