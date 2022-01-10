using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.UsersRepositories
{
    public interface IUsersRepository
    {
        void Add(Users users);
        void Delete(Users users);
        void Update(Users users);
        List<Users> GetAll();
        List<ViewUsers> GetAllView();
        Users FindID(int? id);
        Users FindUserNameAndPassword(string username, string password);
        bool FindUserNameOrEmail(string username, string email);
        Users FindEmail(string email);
    }
}
