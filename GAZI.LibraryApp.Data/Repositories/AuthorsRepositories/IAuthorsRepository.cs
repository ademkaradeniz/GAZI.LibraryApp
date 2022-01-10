using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.AuthorsRepositories
{
    public interface IAuthorsRepository
    {
        void Add(Authors author);
        void Delete(Authors author);
        void Update(Authors author);
        List<Authors> GetAll();
        Authors FindID(int id);
        bool FindName(string name);
    }
}
