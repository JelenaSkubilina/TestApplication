using BusinessLogic.Helper;
using BusinessLogic.Models;
using System.Linq;

namespace BusinessLogic.Services
{
    public class ConfigurationTypeService : BaseService<ConfigurationType>, IConfigurationTypeService
    {
        public ConfigurationTypeService(DataContext dataContext)
            : base(dataContext)
        {
        }

        public IQueryable<ConfigurationType> GetTypes()
        {
            return dataContext.ConfigurationTypes.Where(t => !t.Configurations.Any() || t.Id == (int)Constants.ConfigurationType.Extension);
        }
    }

    public interface IConfigurationTypeService : IService<ConfigurationType>
    {
        IQueryable<ConfigurationType> GetTypes();
    }
}
