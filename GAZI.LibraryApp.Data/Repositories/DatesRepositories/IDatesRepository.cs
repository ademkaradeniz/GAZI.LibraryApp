using GAZI.LibraryApp.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GAZI.LibraryApp.Data.Repositories.DatesRepositories
{
    public interface IDatesRepository
    {
        void Add(Dates dates);
        void Delete(Dates dates);
        void Update(Dates dates);
        List<Dates> GetAll();
        Dates FindID(int id);
        Dates FindDate(Dates date);
    }
}
