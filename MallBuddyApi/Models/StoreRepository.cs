using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MallBuddyApi.Models
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
            return this.context.Stores;
        }

        public Store Get(int id)
        {
            return this.context.Stores.Find(id);
        }

        public Store Add(Store store)
        {
            throw new NotImplementedException();
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