using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models
{
    public class SimplePOI
    {
        public long Id { get; set; }
        public String Name { get; set; }
        public String Name2 { get; set; }
        public List<int> Categories { get; set; }
        public String LogoUrl { get; set; }

        public SimplePOI() { }

        public SimplePOI(POI poi)
        {
            this.Id = poi.DbID;
            this.Name = poi.Name;
            if (poi is Store)
            {
                this.Name2 = ((Store)poi).Name2;
                if (((Store)poi).Categories!=null)
                    this.Categories = ((Store)poi).Categories.Select(x=>x.Id).ToList();
                this.LogoUrl = ((Store)poi).LogoUrl;
            }
        }
    }
}
