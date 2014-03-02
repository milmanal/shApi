using MallBuddyApi2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Controllers
{
    public class PoisRepository : IPoisRepository
    {
        private Models.ApplicationDbContext db;

        public PoisRepository(Models.ApplicationDbContext applicationDbContext)
        {
            // TODO: Complete member initialization
            this.db = applicationDbContext;
        }

        public IQueryable<Models.POI> GetAll()
        {
            throw new NotImplementedException();
        }

        public Models.POI Get(int id)
        {
            POI poi = db.POIs.Find(id);


            return poi;
        }

        public Models.POI Add(Models.POI store)
        {
            throw new NotImplementedException();
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Models.POI store)
        {
            throw new NotImplementedException();
        }
    }
}
