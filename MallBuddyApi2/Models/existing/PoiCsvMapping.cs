using CsvHelper.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class PoiCsvMapping : CsvClassMap<POI>
    {
        public override void CreateMap()
        {
            Map(m => m.Type).Name("Type");
            Map(m => m.Name).Name("Name");
            Map(m => m.Type).Name("Type");
            Map(m => m.Type).Name("Type");
            Map(m => m.Type).Name("Type");
            Map(m => m.Type).Name("Type");
            Map(m => m.Type).Name("Type");

            //base.CreateMap();
        }
    }
}