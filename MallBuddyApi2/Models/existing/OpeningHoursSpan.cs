﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public class OpeningHoursSpan
    {
        public int Id { get; set; }
        public DayOfWeek day { get; set; }
        public DateTime from { get; set; }
        public DateTime to { get; set; }
    }
}
