using BusinessLogic.Models;

namespace BusinessLogic.Services
{
    public class DataService : BaseService<Data>, IDataService
    {
        public DataService(DataContext dataContext)
            : base(dataContext)
        {
        }
    }

    public interface IDataService : IService<Data>
    {
    }
}
