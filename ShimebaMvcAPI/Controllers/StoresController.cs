using ShimebaMvcAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace ShimebaMvcAPI.Controllers
{
    public class StoresController : ApiController
    {
        Store[] stores;

        public IEnumerable<Store> GetAllStores()
        {
            return stores;
        }

        public IHttpActionResult GetStore(int id)
        {
            var store = stores.FirstOrDefault((p) => p.Id == id);
            if (store == null)
            {
                return NotFound();
            }
            return Ok(store);
        }
    }
}
