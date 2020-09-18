namespace BusinessLogic.Services
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;

    public class UserService : BaseService<User>, IUserService
    {
        public UserService(DataContext dataContext)
            : base(dataContext)
        {
        }

        public User GetByEmailPassword(string email, string password)
        {
            return dataContext.Users
                .Include(u => u.Role)
                .FirstOrDefault(u => u.Email == email && u.Password == password);
        }
    }

    public interface IUserService : IService<User>
    {
        User GetByEmailPassword(string email, string password);
    }
}
