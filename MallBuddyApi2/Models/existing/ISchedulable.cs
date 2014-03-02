using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public interface ISchedulable
    {
        List<OpeningHoursSpan> Schedule { get; set; }
    }
}
