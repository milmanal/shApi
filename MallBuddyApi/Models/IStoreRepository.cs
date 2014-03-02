using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MallBuddyApi.Models
{
    interface IStoreRepository
    {
        IEnumerable<Store> GetAll();
        Store Get(int id);
        Store Add(Store store);
        void Remove(int id);
        bool Update(Store store);
    }
}
