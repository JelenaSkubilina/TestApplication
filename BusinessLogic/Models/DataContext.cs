using Microsoft.EntityFrameworkCore;


namespace BusinessLogic.Models
{
    public class DataContext : DbContext
    {
        public DbSet<Data> Datas { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<ConfigurationType> ConfigurationTypes { get; set; }
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

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role userRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id };
            User user = new User { Id = 2, Email = userEmail, Password = userPassword, RoleId = userRole.Id };

            ConfigurationType maxDataSize = new ConfigurationType { Id = 1, Name = "MaxDataSize" };
            ConfigurationType extension = new ConfigurationType { Id = 2, Name = "Extension" };
            ConfigurationType maxImgHeight = new ConfigurationType { Id = 3, Name = "MaxImgHeight" };
            ConfigurationType maxImgWidth = new ConfigurationType { Id = 4, Name = "MaxImgWidth" };
            ConfigurationType minImgHeight = new ConfigurationType { Id = 5, Name = "MinImgHeight" };
            ConfigurationType minImgWidth = new ConfigurationType { Id = 6, Name = "MinImgWidth" };

            builder.Entity<Configuration>().HasData
                (new Configuration[]
                {
                    new Configuration { Id = 1, ConfigurationTypeId = 1,  Value = "50"},
                    new Configuration { Id = 2, ConfigurationTypeId = 2,  Value = ".jpeg"},
                    new Configuration { Id = 3, ConfigurationTypeId = 3,  Value = "500"},
                    new Configuration { Id = 4, ConfigurationTypeId = 4,  Value = "500"},
                    new Configuration { Id = 5, ConfigurationTypeId = 5,  Value = "100"},
                    new Configuration { Id = 6, ConfigurationTypeId = 6,  Value = "100"},
                    new Configuration { Id = 7, ConfigurationTypeId = 2,  Value = ".mp4"}
                });

            builder.Entity<ConfigurationType>().HasData
                (new ConfigurationType[] { maxDataSize, extension, maxImgHeight, maxImgWidth, minImgHeight, minImgWidth });

            builder.Entity<Role>().HasData(new Role[] { adminRole, userRole });
            builder.Entity<User>().HasData(new User[] { adminUser, user });

            base.OnModelCreating(builder);
        }
    }
}