using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.TypesRepositories
{
    public interface ITypesRepository
    {
        void Add(Types types);
        void Delete(Types types);
        void Update(Types types);
        List<Types> GetAll();
        Types FindID(int id);
        bool FindName(string name);
    }
}
