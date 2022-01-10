using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.PublishersRepositories
{
    public interface IPublishersRepository
    {
        void Add(Publishers publisher);
        void Delete(Publishers publisher);
        void Update(Publishers publisher);
        List<Publishers> GetAll();
        Publishers FindID(int id);
        bool FindName(string name);
    }
}
