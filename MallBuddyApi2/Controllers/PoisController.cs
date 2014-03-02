using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using MallBuddyApi2.Models;
using System.Web.Http.Cors;

namespace MallBuddyApi2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PoisController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        static readonly IStoreRepository storeRepository = new StoreRepository(new ApplicationDbContext());

        // GET api/Poi
        [Route("api/pois")]
        public IEnumerable<POI> GetPOIs()
        {
            return storeRepository.GetAll().Concat(db.POIs.Where(x=>!(x is Store)).Include("Location").Include("Location.Points").
                Include("ImageList").Include("Location.Areas"));
        }

        [Route("api/pois/{poitype}")]
        public IQueryable<POI> GetPOIsByType(string poitype)
        {
            POI.POIType typeclass;
            try
            {
                typeclass = (POI.POIType)(Enum.Parse(typeof(POI.POIType), poitype.ToUpper()));
            }
            catch (Exception ex)
            {
                var resp = new HttpResponseMessage(HttpStatusCode.BadRequest)
                {
                    Content = new StringContent(string.Format("No Poi with type = {0}", poitype)),
                    ReasonPhrase = "POI type Not Found"
                };
                throw new HttpResponseException(resp);
            }
            switch (typeclass)
            {
                case POI.POIType.STORE:
                    {
                        return db.POIs.OfType<Store>().Include(x => x.ContactDetails)
                            .Include(x => x.Schedule).Include(x => x.Entrances).Include(x => x.Categories)
                            .Include("Location").Include("Location.Points").
                            Include("ImageList").Include("Location.Areas");
                    }
                default:
                    {
                        return db.POIs.Where(x => x.Type == typeclass).Include("Location").Include("Location.Points").
    Include("ImageList").Include("Location.Areas");
                    }
            }

        }

        // GET api/Poi/5
        [ResponseType(typeof(POI))]
        [Route("api/pois/{id:int}", Name="GetPOIById")]
        public IHttpActionResult GetPOI(int id)
        {
            POI poi = db.POIs.Find(id);
            if (poi == null)
            {
                return NotFound();
            }

            return Ok(poi);
        }

        //[Route("api/pois/{id:int}")]
        //[AcceptVerbs("Patch")]
        //public void PatchPOI(int id, Delta<POI> value)
        //{
            
        //    var t = db.POIs.FirstOrDefault(x => x.DbID == id);
        //    if (t == null) throw new HttpResponseException(HttpStatusCode.NotFound);

        //    value.Patch(t);
        //}

        // PUT api/Poi/5
        [Route("api/pois/{id:int}")]
        public IHttpActionResult PutPOI(int id, POI poi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != poi.DbID)
            {
                return BadRequest();
            }

            db.Entry(poi).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!POIExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Poi
        [ResponseType(typeof(POI))]
        [Route("api/pois")]
        public IHttpActionResult PostPOI(POI poi)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //switch (poi.GetType().ToString())
            //{
            //    case "Store":
            //        {
            //            db.Stores.Add(new Store(poi));
            //        }
            //        db.POIs.Add(poi);
            //}
            db.POIs.Add(poi);
            db.SaveChanges();
            return CreatedAtRoute("GetPOIById", new { id = poi.DbID }, poi);
            //var response = Request.CreateResponse<POI>(HttpStatusCode.Created, poi);
            ////Request.Properties.controller.Request.Properties.Add(HttpPropertyKeys.HttpRouteDataKey, routeData);
            //string uri = Url.Link("GetPOIById", new { id = poi.DbID });
            ////string uri = Url.Link("DefaultApi", new { id = poi.DbID });
            //response.Headers.Location = new Uri(uri);
            //return response;
        }

        // DELETE api/Poi/5
        [ResponseType(typeof(POI))]
        [Route("api/pois/{id:int}")]
        public IHttpActionResult DeletePOI(int id)
        {
            POI poi = db.POIs.Find(id);
            if (poi == null)
            {
                return NotFound();
            }

            db.POIs.Remove(poi);
            db.SaveChanges();

            return Ok(poi);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool POIExists(long id)
        {
            return db.POIs.Count(e => e.DbID == id) > 0;
        }
    }
}