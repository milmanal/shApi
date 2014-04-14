using MallBuddyApi2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Controllers
{
    public class PoisRepository : IPoisRepository
    {
        private Models.ApplicationDbContext db;

        public PoisRepository()
        {
            // TODO: Complete member initialization
            this.db = new ApplicationDbContext();
        }
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

        public List<POI> GetContainerByLocation(string lon, string lat, int level)
        {
            List<POI> containers = new List<POI>();
            using (var context = new ApplicationDbContext())
            {
                var levelStores = context.POIs.Include("Location").Where(x => x.Location.Level == level);
                //int count = levelStores.Count();

                DbGeometry point = DbGeometry.PointFromText("POINT (" + lon + " " + lat + ")", 4326);
                foreach (var s in levelStores)
                {
                    //context.Entry(s).Reference("Location").Load();
                    if (s.Location.LocationG.Contains(point))
                        containers.Add(s);
                    //if (s.DbID == 528)
                     //   containers = containers;
                }
            }
            return containers;
        }

    }
}
