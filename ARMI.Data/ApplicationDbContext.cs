using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Linq;

namespace ARMI.Data
{
    public class ApplicationDbContext : DbContext
    {

        public DbSet<Models.Client> Clients { get; set; }
        public DbSet<Models.System> Systems { get; set; }
        public DbSet<Models.Emulator> Emulators { get; set; }
        public DbSet<Models.Rom> Roms { get; set; }
        public DbSet<Models.RomList> RomLists { get; set; }
        public DbSet<Models.RomListRoms> RomListRoms { get; set; }
        public DbSet<Models.Job> Jobs { get; set; }
        private DbContextOptions<ApplicationDbContext> Options;


        private readonly IConfiguration cfg;
        public ApplicationDbContext(IConfiguration configuration)
        {
            cfg = configuration;
        }

        //public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        //{
        //    Options = options;
        //}

        public ApplicationDbContext()
        {
            //var connctionStrings = Configuration.GetSection("ConnectionStrings");
            //var Config = Configuration.GetSection("ConnectionStrings");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var config = GetConfiguration();
            if (config.Config.ConnectionType.Equals("Sql"))
            {
                optionsBuilder.UseSqlServer(config.ConnectionStrings.SqlDatabase, x =>
                {
                    x.MigrationsAssembly("ARMI.SqlServer");
                });
            }
            else
            {
                optionsBuilder.UseSqlite(config.ConnectionStrings.SqlLiteDatabase, x =>
                {
                    x.MigrationsAssembly("ARMI.SqlLiteServer");
                });
            }            
        }

        private Configuration GetConfiguration()
        {
            Configuration configuration = new Configuration
            {
                Config = cfg.GetSection("Config").Get<Config>(),
                ConnectionStrings = cfg.GetSection("ConnectionStrings").Get<ConnectionStrings>()
            };
            return configuration;
        }

        private DbContextOptionsBuilder<ApplicationDbContext> GetOptions()
        {
            var config = GetConfiguration();
            var options = new DbContextOptionsBuilder<ApplicationDbContext>();
            switch (config.Config.ConnectionType)
            {
                case "Sql":
                    {
                        options.UseSqlServer(config.ConnectionStrings.SqlDatabase, x =>
                        {
                            x.MigrationsAssembly("ARMI.SqlServer");
                        });
                        break;
                    }

                case "SqlLite":
                default:
                    {
                        options.UseSqlite(config.ConnectionStrings.SqlLiteDatabase, x => {
                            x.MigrationsAssembly("ARMI.SqlLiteServer");
                        });
                        break;
                    }
            }
            return options;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Models.RomList>()
              .HasMany(rl => rl.SubRomLists)
              .WithOne(rl => rl.ParentRomList)
              .HasForeignKey(rl => rl.ParentRomListId);

            modelBuilder.Entity<Models.RomListRoms>().HasKey(rlr => new { rlr.RomListId, rlr.RomId });
            modelBuilder.Entity<Models.RomListRoms>()
                .HasOne(rlr => rlr.RomList)
                .WithMany(rl => rl.RomListRoms)
                .HasForeignKey(rlr => rlr.RomListId);
            modelBuilder.Entity<Models.RomListRoms>()
                .HasOne(rlr => rlr.Rom)
                .WithMany(r => r.RomListRoms)
                .HasForeignKey(rlr => rlr.RomId);
        }

        public override int SaveChanges()
        {
            ChangeTracker.DetectChanges();

            var entries = ChangeTracker.Entries<Models.Rom>()
                .Where(e => e.State == EntityState.Added || e.State == EntityState.Modified);

            foreach (var entry in entries)
            {
                entry.Property("ModifiedDate").CurrentValue = DateTime.UtcNow;
            }

            return base.SaveChanges();
        }
    }
}
