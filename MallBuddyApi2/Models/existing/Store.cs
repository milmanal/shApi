using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Globalization;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using MallBuddyApi2.Models.existing;
using System.Reflection;

namespace MallBuddyApi2.Models
{
    public class Store : POI, ISchedulable
    {
        //private POI poi;

        public Store(POI poi)
        {
            foreach (PropertyInfo prop in poi.GetType().GetProperties())
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(poi, null), null);
        }

        public Store()
        {
            // TODO: Complete member initialization
        }
        //[KeyAttribute]
        //public int Id { get; set; }
        //public string Name { get; set; }
        public List<Category> Categories { get; set; }
        public List<String> Tags { get; set; }

        //public decimal Price { get; set; }
        //public List<int> floors { get; set; }
        public int Floor { get; set; }
        //public List<Category> categories { get; set; }
        public virtual ContactDetails ContactDetails { get; set; }
        public virtual List<OpeningHoursSpan> Schedule { get; set; }
        //List<Product> products { get; set; }
        //List<Promotion> promotions { get; set; }
        //public List<Announcement> announcements { get; set; }
        public virtual List<Point3D> Entrances { get; set; }
        //public List<Polygone> exits { get; set; }
        //public List<Area> Area { get; set; }
        // url from which the data was scraped not the official website
        public String WebsiteLink { get; set; }
        public string LogoUrl { get; set; }
        //public List<Image> ImageList { get; set; }

        public bool IsAccessible { get; set; }

        //public string Self
        //{
        //    get
        //    {
        //        return string.Format(CultureInfo.CurrentCulture,
        //            "api/stores/{0}", this.Id);
        //    }
        //    set { }
        //}




        public string Name2 { get; set; }
    }


}