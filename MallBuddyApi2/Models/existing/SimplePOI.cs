using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models
{
    public class SimplePOIsCollection
    {
        public IEnumerable<SimplePOI> POIs {get; set;}
    }
    public class SimplePOI
    {
        [JsonProperty("storeId")]
        public long Id { get; set; }
        [JsonProperty("storeName")]
        public String Name { get; set; }
        [JsonProperty("otherName")]
        public String Name2 { get; set; }
        public List<int> Categories { get; set; }
        [JsonProperty("storeIconURL")]
        public String LogoUrl { get; set; }

        public SimplePOI() { }

        public SimplePOI(POI poi)
        {
            this.Id = poi.DbID;
            this.Name = poi.Name;
            if (poi is Store)
            {
                this.Name2 = ((Store)poi).Name2;
                if (((Store)poi).Categories != null)
                    Categories = (List<int>)((Store)poi).Categories.Select(x => (int)x.CategoryType).ToList();
                this.LogoUrl = ((Store)poi).LogoUrl;
            }
        }
    }
}
