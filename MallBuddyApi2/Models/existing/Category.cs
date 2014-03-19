using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public class Category
    {
        //public String ID { get; set; }
        
        public String Text { get; set; }
        [Key]
        public int Id { get; set; }
    }
}
