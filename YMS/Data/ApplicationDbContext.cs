
using Microsoft.EntityFrameworkCore;
using YMS.MESModels;
using YMS.Models;

namespace YMS.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RolePermission> RolePermissions { get; set; }
        public DbSet<Coil> Coils { get; set; }
        public DbSet<StoragePlaceholder> StoragePlaceholders { get; set; }
        public DbSet<Yard> Yards { get; set; }
        public DbSet<Inspection> Inspections { get; set; }
        public DbSet<CoilMovement> CoilMovements { get; set; }
        public DbSet<AuditTrail> AuditTrails { get; set; }
        public DbSet<OutgoingDispatch> OutgoingDispatches { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Coil and StoragePlaceholder One-to-One Relationship
            modelBuilder.Entity<Coil>()
                .HasOne(c => c.CurrentLocation)  // Coil has a relationship with StoragePlaceholder
                .WithOne(sp => sp.OccupyingCoil) // StoragePlaceholder has a relationship with Coil
                .HasForeignKey<StoragePlaceholder>(sp => sp.OccupyingCoilID)  // Specify the foreign key on StoragePlaceholder
                .OnDelete(DeleteBehavior.SetNull); // Specify delete behavior (optional)
            modelBuilder.Entity<CoilMovement>()
               .HasOne(c => c.ToPlaceHolder) // Foreign Key: ToPlaceHolderID
               .WithMany()
               .HasForeignKey(c => c.ToPlaceHolderID)
               .OnDelete(DeleteBehavior.NoAction); // Prevent any action on delete

            modelBuilder.Entity<CoilMovement>()
                .HasOne(c => c.FromPlaceHolder) // Foreign Key: FromPlaceHolderID
                .WithMany()
                .HasForeignKey(c => c.FromPlaceHolderID)
                .OnDelete(DeleteBehavior.NoAction); // Prevent any action on delete

            modelBuilder.Entity<Role>().HasData(
       new Role { RoleID = 1, RoleName = "Admin", Description = "Administrator with full access" },
       new Role { RoleID = 2, RoleName = "Manager", Description = "Manager with limited access" },
       new Role { RoleID = 3, RoleName = "Supervisor", Description = "Supervisor with operational access" },
       new Role { RoleID = 4, RoleName = "Operator", Description = "Operator with basic access" }
   );
            base.OnModelCreating(modelBuilder);
        }


    }


    public class MesDbContext : DbContext
    {
        public MesDbContext(DbContextOptions<MesDbContext> options) : base(options) { }

        public DbSet<MESCoil> MESCoils { get; set; }
        public DbSet<MESIncomingCoil> MESIncomingCoils { get; set; }
       
    }
}
