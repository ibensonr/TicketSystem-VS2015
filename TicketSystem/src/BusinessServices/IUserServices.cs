using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessServices
{
    public interface IUserServices
    {
        UserEntity GetUserById(int? userId);
        IEnumerable<UserEntity> GetAllUsers();
        int CreateUser(UserEntity userEntity);
        bool UpdateUser(int userId, UserEntity userEntity);
        bool DeleteUser(int userId);
        UserEntity GetUserHistoryById(int userId);
        UserEntity GetUserDetails(string uname, string password);
    }
}
