using MallBuddyApi2.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Spatial;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MallBuddyApi2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/v1")]
    public class StoresController : ApiController
    {
        static readonly IStoreRepository storeRepository = new StoreRepository(new ApplicationDbContext());

        // GET api/<controller>
        public IEnumerable<Store> Get()
        {
            //return new string[] { "value1", "value2" };
            return storeRepository.GetAll();
        }

        // GET api/<controller>/5
        public Store Get(int id)
        {
            Store store = storeRepository.Get(id);
            return store;
            //return "value";
        }

        public List<POI> GetByLocation(string lon, string lat, int level)
        {
            var levelStores = storeRepository.GetAll().Where(x => x.Floor == level);
            DbGeometry point = DbGeometry.PointFromText("POINT (" + lon + " " + lat + ")", 4326);
            List<POI> containers = new List<POI>();
            foreach (Store s in levelStores)
            {
                if (s.Location.LocationG.Contains(point))
                    containers.Add(s);
            }
            return containers;
        }

        // POST api/<controller>
        public void Post([FromBody]Store value)
        {
            Store store = storeRepository.Add(value);
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]Store value)
        {
            storeRepository.Update(value);
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}