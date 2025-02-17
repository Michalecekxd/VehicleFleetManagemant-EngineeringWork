using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using VehicleFleetManagement.Model.DataModels;
using System.Reflection.Emit;
using VehicleFleetManagement.Model.Identity;



namespace VehicleFleetManagement.DAL.EF
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int> // (we can also inherit only by default Identity (default columns provided by Identity without additionals columns or change types for primary key by writing ApplicationDbContext:IdentityDbContext) 
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        
        public DbSet<RigidTruck> RigidTrucks { get; set; }
        public DbSet<SemiTrailer> SemiTrailers { get; set; }
        public DbSet<TractorUnit> TractorUnits { get; set; }
        public DbSet<DeliveryVan> DeliveryVans { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

        //configure additional settings e.g. relationships, primary keys, foreign keys, restrictions etc.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);  // needed to properly create the Identity tables

            //In inheritance we can use TPH, TPT OR TPC strategy

            #region TPH- Table Per Hierarchy
            // TPH- Table Per Hierarchy
            // TPH- default strategy by EF (all types of vehicles (TractorUnit, RigidTruck ...) in one table(Vehicles)) but it is not suitable for our case (large one table and a lot of null values..) so better use TPC or TPT
            //modelBuilder.Entity<Vehicle>()
            //    .ToTable("Vehicles")
            //    .HasDiscriminator<string>("Vehicle_Type") // TPH uses discriminator column to determine the type of vehicle
            //    .HasValue<TractorUnit>("TractorUnit") // when adding new TractorUnit, add "TractorUnit" data to Vehicle_Type column
            //    .HasValue<RigidTruck>("RigidTruck") // when adding new RigidTruck, add "RigidTruck" data to Vehicle_Type column
            //    .HasValue<DeliveryVan>("DeliveryVan"); // when adding new DeliveryVan, add "DeliveryVan" data to Vehicle_Type column
            #endregion

            #region TPT- Table Per Type
            // TPT- Table Per Type (not very efficient)
            // One table for each type of vehicle (so we have 4 separate tables: Vehicles, TractorUnits, RigidTrucks, DeliveryVans)- Database "Vehicles" has all column names from Vehicle class and TractorUnits has only column names from TractorUnit class(0 columns from Vehicle)- common Id in Vehicle table and TractorUnit table
            //modelBuilder.Entity<Vehicle>()
            //    .UseTptMappingStrategy();   
            //    .ToTable("Vehicles");
            //modelBuilder.Entity<TractorUnit>()
            //    .ToTable("TractorUnits");
            //modelBuilder.Entity<RigidTruck>()
            //    .ToTable("RigidTrucks");
            //modelBuilder.Entity<SemiTrailer>()
            //    .ToTable("SemiTrailers");
            //modelBuilder.Entity<DeliveryVan>()
            //    .ToTable("DeliveryVans");
            // instead of calling ToTable for each entity type we can call modelBuilder.Entity<Vehicle>().UseTptMappingStrategy() <--its the same but shorter record
            #endregion

            // TPC- Table Per Concrete Type
            // Each concrete class has his own table (BUT APART from the base parent table)- so we have 3 separate tables: TractorUnits, RigidTrucks, DeliveryVans
            // Each table must have unique Id (we can't have the same Id in two other tables e.g. TractorUnits Id 1 and RigidTrucks Id 1 <--RigidTruck must have Id 2)

            modelBuilder.Entity<Vehicle>()
                .UseTpcMappingStrategy();
        }
    }
}
