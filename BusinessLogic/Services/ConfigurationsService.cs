using BusinessLogic.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ConfigurationsService : BaseService<Configuration>, IConfigurationsService
    {
        private readonly IMemoryCache memoryCache;

        public ConfigurationsService(DataContext dataContext,
            IMemoryCache memoryCache)
            : base(dataContext)
        {
            this.memoryCache = memoryCache;
        }

        public void UpdateConfigurations(Configuration configuration)
        {
            base.Update(configuration);

            memoryCache.Set(1, configuration, new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)
            });
        }

        public Configuration GetConfiguration(int id)
        {
            Configuration configuration = null;
            if (!memoryCache.TryGetValue(id, out configuration))
            {
                configuration = dataContext.Configurations.Include(c => c.ConfigurationType).FirstOrDefault();
                if (configuration != null)
                {
                    memoryCache.Set(1, configuration,
                    new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromMinutes(5)));
                }
            }

            return configuration;
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

        void UpdateConfigurations(Configuration configuration);
    }
}
