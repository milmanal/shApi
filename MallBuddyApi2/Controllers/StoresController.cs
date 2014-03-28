using MallBuddyApi2.Models;
using MallBuddyApi2.Utils;
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
    //[RoutePrefix("api/v1")]
    public class StoresController : ApiController
    {
        readonly IStoreRepository storeRepository = new StoreRepository(new ApplicationDbContext());

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
        [Route("api/stores/getbylocation")]
        public List<Store> GetByLocation(string lon, string lat, int level)
        {
            int parsedLon;
            if (int.TryParse(lon, out parsedLon))
            {
                int parsedLat = int.Parse(lat);
                Point3D point = GeoUtils.pixelPoint2LongLat(parsedLat, parsedLon, level);
                return storeRepository.GetContainerByLocation(point.Longitude.ToString(), point.Latitude.ToString(), level);

            }
            return storeRepository.GetContainerByLocation(lon, lat, level);

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