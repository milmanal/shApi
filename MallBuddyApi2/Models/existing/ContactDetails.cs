using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models
{
    public class ContactDetails
    {
        [Key]
        public string PoiName{ get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Contactname { get; set; }
        //public POI POI;
    }
}
