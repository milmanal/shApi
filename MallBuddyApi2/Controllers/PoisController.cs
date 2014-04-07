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
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core;
using Newtonsoft.Json;
using MallBuddyApi2.Models.existing;
using MallBuddyApi2.Utils;

namespace MallBuddyApi2.Controllers
{
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    public class PoisController : ApiController
    {
        private ApplicationDbContext db;// = new ApplicationDbContext();
        static IStoreRepository storeRepository;// = new StoreRepository(db);
        readonly IPoisRepository poisRepository;// = new PoisRepository(new ApplicationDbContext());

        public PoisController()
        {
            db = new ApplicationDbContext();
            storeRepository = new StoreRepository(db);
            poisRepository = new PoisRepository(db);
        }
        // GET api/Poi
        [Route("api/pois")]
        public IEnumerable<POI> GetPOIs()
        {
            return storeRepository.GetAll().Concat(db.POIs.Where(x => !(x is Store)).Include("Location").Include("Location.Points").
                Include("ImageList").Include("Location.Areas"));
        }

        // GET api/Poi
        [Route("api/pois/slim")]
        public IEnumerable<SimplePOI> GetPOIsCompact()
        {
            List<SimplePOI> poisToReturn = new List<SimplePOI>();
            foreach (var poi in db.POIs.ToList())
            {
                SimplePOI simplePOI = new SimplePOI(poi);
                poisToReturn.Add(simplePOI);
            }
            return poisToReturn;
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
                        return db.POIs.OfType<Store>().Include(x => x.Schedule).Include(x => x.Entrances).Include(x => x.Categories)
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
        [Route("api/pois/{id:int}", Name = "GetPOIById")]
        public IHttpActionResult GetPOI(int id)
        {
            if (db.POIs.Find(id) == null)
                return NotFound();
            POI poi = db.POIs.Include("Location").Include("Location.Points").
    Include("ImageList").Include("Location.Areas").Single(x => x.DbID == id);
            if (poi == null)
            {
                return NotFound();
            }

            return Ok(poi);
        }

        [Route("api/pois/getbylocation")]
        public List<POI> GetByLocation(string lon, string lat, int level)
        {
            //IPoisRepository poisRepository = new PoisRepository();
            int parsedLon;
            if (int.TryParse(lon, out parsedLon))
            {
                int parsedLat = int.Parse(lat);
                Point3D point = GeoUtils.pixelPoint2LongLat(parsedLat, parsedLon, level);
                return poisRepository.GetContainerByLocation(point.Longitude.ToString(), point.Latitude.ToString(), level);

            }
            return poisRepository.GetContainerByLocation(lon, lat, level);

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
            //var found = db.POIs.Find(id);
            db.Entry(poi).State = EntityState.Modified;
            //db.ChangeTracker.DetectChanges();
            //var entry = db.Entry(poi);
            //entry.OriginalValues.SetValues(found);
            //entry.CurrentValues.SetValues(person);

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

            return CreatedAtRoute("GetPOIById", new { id = poi.DbID }, poi);
        }


        // POST api/Poi
        //[ResponseType(typeof(POI))]
        [HttpPost]
        [Route("api/pois")]
        public HttpResponseMessage PostPOI([FromBody]string content) //(POI poi)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
            POI poi = null;
            try
            {
                poi = JsonConvert.DeserializeObject<Store>(content, settings);
                poi.Type = POI.POIType.STORE;
                //if (((Store)poi).ContactDetails != null && ((Store)poi).ContactDetails.PoiName == null)
                //    ((Store)poi).ContactDetails.PoiName = poi.Name;
            }
            catch (Exception)
            {
                try
                {
                    poi = JsonConvert.DeserializeObject<POI>(content, settings);
                }
                catch (Exception)
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest);
                }
            }

            if (!ModelState.IsValid)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
            }
            poi.Modified = DateTime.Now;
            if (poi.Location != null)
            {
                if (poi.Location.Id > 0)
                {
                    Polygone foundP = db.Polygones.Find(poi.Location.Id);
                    if (foundP == null)
                    {
                        foundP = db.Polygones.First(x => x.Wkt == poi.Location.Wkt && x.Level == poi.Location.Level);
                        if (foundP != null)
                            db.Entry<Polygone>(foundP).State = EntityState.Modified;
                    }
                }
            }
            POI found = db.POIs.FirstOrDefault(x => x.Name == poi.Name && x.Location.Level == poi.Location.Level && x.Location.Wkt == poi.Location.Wkt);
            if (found == null)
            {
                switch (poi.Type)
                {
                    case POI.POIType.STORE:
                        {
                            db.Stores.Add(new Store(poi));
                            break;
                        }
                    default:
                        {
                            db.POIs.Add(poi);
                            break;
                        }
                }

                //if (db.Entry<POI>(poi).State == EntityState.Detached)
                //{
                //    //EFSet.Attach(updatedEntity);
                //    db.POIs.Attach(poi);
                //}
                //else
                //{
                //    //EFContext.Entry<TEntity>(updatedEntity).CurrentValues.SetValues(entity);
                //    db.Entry(poi).CurrentValues.SetValues(poi);
                //}
                //db.Entry<POI>(poi).State = EntityState.Modified;

            }
            else
            {
                if (db.Entry<POI>(poi).State == EntityState.Detached)
                {
                    //EFSet.Attach(updatedEntity);
                    db.POIs.Attach(poi);
                }
                else
                {
                    //EFContext.Entry<TEntity>(updatedEntity).CurrentValues.SetValues(entity);
                    db.Entry(poi).CurrentValues.SetValues(poi);
                }
                db.Entry<POI>(poi).State = EntityState.Modified;
            }



            //POI existing = db.POIs.SingleOrDefault(x => x.DbID == poi.DbID);

            //db.POIs.Add(poi);
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                var context = ((IObjectContextAdapter)db).ObjectContext;
                context.Refresh(System.Data.Entity.Core.Objects.RefreshMode.StoreWins, poi);
                db.SaveChanges();
            }
            //return CreatedAtRoute("GetPOIById", new { id = poi.DbID }, poi);
            return Request.CreateResponse(HttpStatusCode.Created, poi);

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