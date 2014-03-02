using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MallBuddyApi2.Models.existing
{
    public class Entrance : POI, ISchedulable
    {
        [Key]
        public String gateID  { get; set; }
        public List<OpeningHoursSpan> schedule;

        public virtual List<OpeningHoursSpan> Schedule
        {
            get
            {
                return schedule;
            }
            set
            {
                schedule = value;
            }
        }
    }
}