namespace DataAccess
{
    using global::DataAccess.IdentiyModels;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class TAD :  IdentityDbContext<ApplicationUser>
    {
        // Your context has been configured to use a 'TAD' connection string from your application's 
        // configuration file (App.config or Web.config). By default, this connection string targets the 
        // 'DataAccess.TAD' database on your LocalDb instance. 
        // 
        // If you wish to target a different database and/or database provider, modify the 'TAD' 
        // connection string in the application configuration file.
        public TAD(string name) : base(name)
        {
        }

        public DbSet<Module> Modules { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<RolePermissionMap> RolePermissionMaps { get; set; }

        // Add a DbSet for each entity type that you want to include in your model. For more information 
        // on configuring and using a Code First model, see http://go.microsoft.com/fwlink/?LinkId=390109.

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            Database.SetInitializer<TAD>(null);
            base.OnModelCreating(modelBuilder);
            modelBuilder.HasDefaultSchema("tadrnd");

            modelBuilder.Entity<ApplicationUser>().ToTable("TAD_T_Users");
            modelBuilder.Entity<IdentityRole>().ToTable("TAD_T_Roles");
            modelBuilder.Entity<ApplicationRole>().ToTable("TAD_T_Roles");
            modelBuilder.Entity<IdentityUserRole>().ToTable("TAD_T_UserRoleMap");
            modelBuilder.Entity<IdentityUserLogin>().ToTable("TAD_T_UserLoginMap");
            modelBuilder.Entity<IdentityUserClaim>().ToTable("TAD_T_UserClaimMap");
            modelBuilder.Entity<Module>().ToTable("TAD_T_Modules");
            modelBuilder.Entity<Permission>().ToTable("TAD_T_Permissions");
            modelBuilder.Entity<RolePermissionMap>().ToTable("TAD_T_RolePermissionMap");
        }
    }

}