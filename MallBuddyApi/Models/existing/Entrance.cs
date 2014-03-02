using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MallBuddyApi.Models.existing
{
    public class Entrance : POI, ISchedulable
    {
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