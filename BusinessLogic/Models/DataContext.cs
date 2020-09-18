using Microsoft.EntityFrameworkCore;


namespace BusinessLogic.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Data> Datas { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DataContext(DbContextOptions<DataContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";

            string adminEmail = "admin@gmail.com";
            string adminPassword = "123456";

            string userEmail = "user@gmail.com";
            string userPassword = "123456";

            //Configuration configuration = new Configuration
            //{
            //    MaxDataSize = 50,
            //    MaxImgHeight = 2000,
            //    MaxImgWidth = 2000,
            //    MinImgHeight = 100,
            //    MinImgWidth = 100
            //};

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            User user = new User { Id = 2, Email = userEmail, Password = userPassword, RoleId = userRole.Id};

            builder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            builder.Entity<User>().HasData(new User[] { adminUser, user });

            builder.Entity<Configuration>().HasNoKey();
           // builder.Entity<Configuration>().HasData(new Configuration[] { configuration });
            base.OnModelCreating(builder);
        }
    }
}