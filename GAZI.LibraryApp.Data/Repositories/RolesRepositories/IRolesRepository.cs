using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.RolesRepositories
{
    public interface IRolesRepository
    {
        void Add(Roles roles);
        void Delete(Roles roles);
        void Update(Roles roles);
        List<Roles> GetAll();
        Roles FindID(int? id);
        bool FindName(string name);
        int GetID(string name);
    }
}
