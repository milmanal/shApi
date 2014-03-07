using MallBuddyApi2.Models.existing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace MallBuddyApi2.Controllers
{
     [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class MapsController : ApiController
    {
        // GET api/maps
        public MapsData GetLatestVersion()
        {
            MapsData mapsData = null;
            return mapsData;
        }

        // GET api/maps/5
        public MapsData GetByVersion(string version)
        {
            MapsData mapsData = null;
            return mapsData;
        }

        // POST api/maps
        public MapsData GetByVersionAndLevel(string version, int level)
        {
            MapsData mapsData = null;
            return mapsData;
        }

    }
}
