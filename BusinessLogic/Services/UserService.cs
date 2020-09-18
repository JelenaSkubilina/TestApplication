using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Services
{
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class UserService : BaseService<User>, IUserService
    {
        public UserService(DataContext dataContext)
            : base(dataContext)
        {
        }

        //public User GetById(int id)
        //{
        //    return dataContext.Users.Where(u => u.Id == id).Single();//.Include(u => u.Company).Single();
        //}

        //public IQueryable<User> GetAllWithRoles()
        //{
        //    return dataContext.Users
        //        .Include(u => u.Roles).ThenInclude(r => r.Role);
        //      //  .Include(u => u.Company);
        //}
    }

    public interface IUserService : IService<User>
    {
    //    User GetById(int id);
       // Task<User> GetByEmail(string email);
     //   IQueryable<User> GetAllWithRoles();
    }
}
