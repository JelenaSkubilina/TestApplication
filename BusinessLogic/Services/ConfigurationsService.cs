using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ConfigurationsService : BaseService<Configuration>, IConfigurationsService
    {
        public ConfigurationsService(DataContext dataContext)
            : base(dataContext)
        {
        }

        public Configuration GetConfiguration(int id)
        {
            return dataContext.Configurations.Include(c => c.ConfigurationType).FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Configuration> GetAllConfigurations()
        {
            return dataContext.Configurations.Include(c => c.ConfigurationType);
        }
    }

    public interface IConfigurationsService : IService<Configuration>
    {
        IQueryable<Configuration> GetAllConfigurations();

        Configuration GetConfiguration(int id);
    }
}
