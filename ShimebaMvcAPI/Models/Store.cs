using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using System.Globalization;

namespace ShimebaMvcAPI.Models
{
    public class Store : POI
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public List<int> floors { get; set; }
        public List<Category> categories { get; set; }
        public ContactDetails contactDetails { get; set; }
        public Schedule openingHours { get; set; }
        //List<Product> products { get; set; }
        //List<Promotion> promotions { get; set; }
        public List<Announcement> announcements { get; set; }
        public List<Polygone> entrances { get; set; }
        public List<Polygone> exits { get; set; }

        // url from which the data was scraped not the official website
        public String websiteLink { get; set; }
        public bool isAccessibilty { get; set;     }

        public string Self
        {
            get
            {
                return string.Format(CultureInfo.CurrentCulture,
                    "api/stores/{0}", this.Id);
            }
            set { }
        }


    }


}