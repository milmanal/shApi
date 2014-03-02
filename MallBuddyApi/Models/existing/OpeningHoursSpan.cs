using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi.Models.existing
{
    public class OpeningHoursSpan
    {
        public int Id { get; set; }
        public DayOfWeek day { get; set; }
        public int from { get; set; }
        public int to { get; set; }
    }
}
