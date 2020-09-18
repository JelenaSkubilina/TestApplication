using Microsoft.EntityFrameworkCore;


namespace BusinessLogic.Models
{
    public class DataContext : DbContext//: IdentityDbContext<User>
    {
        public DbSet<Data> Datas { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Role> Roles { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Configuration configuration = new Configuration
            {
                MaxDataSize = 50,
                MaxImgHeight = 2000,
                MaxImgWidth = 2000,
                MinImgHeight = 100,
                MinImgWidth = 100
            };

            builder.Entity<Configuration>().HasNoKey();
            builder.Entity<Configuration>().HasData(new Configuration[] { configuration });
            base.OnModelCreating(builder);
        }
    }
}