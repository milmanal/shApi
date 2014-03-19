using MallBuddyApi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Controllers
{
    interface IPoisRepository
    {
        IQueryable<POI> GetAll();
        POI Get(int id);
        POI Add(POI store);
        void Remove(int id);
        bool Update(POI store);
        List<POI> GetContainerByLocation(string lon, string lat, int level);

    }
}
