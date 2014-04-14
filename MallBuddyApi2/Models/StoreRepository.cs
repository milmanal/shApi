using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Spatial;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models
{
    public class StoreRepository : IStoreRepository
    {
        //private SqlConnection con = null;
        private ApplicationDbContext context;

        public StoreRepository(ApplicationDbContext context)
        {
            //con = new SqlConnection(ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString);
            this.context = context;
        }

        public IEnumerable<Store> GetAll()
        {
            return this.context.Stores.ToList();
        }

        public Store Get(int id)
        {
            return this.context.Stores.Find(id);
        }

        public List<Store> GetContainerByLocation(string lon, string lat, int level)
        {
            List<Store> containers = new List<Store>();
            using (var context = new ApplicationDbContext())
            {
                var levelStores = context.Stores.Include("Location").Where(x => x.Level == level);
                //int count = levelStores.Count();

                DbGeometry point = DbGeometry.PointFromText("POINT (" + lon + " " + lat + ")", 4326);
                foreach (var s in levelStores)
                {
                    //context.Entry(s).Reference("Location").Load();
                    if (s.Location.LocationG.Contains(point))
                        containers.Add(s);
                }
            }
            return containers;
        }

        public Store Add(Store store)
        {
            Store s = this.context.Stores.Add(store);
            context.SaveChanges();
            return s;
        }

        public void Remove(int id)
        {
            throw new NotImplementedException();
        }

        public bool Update(Store store)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}