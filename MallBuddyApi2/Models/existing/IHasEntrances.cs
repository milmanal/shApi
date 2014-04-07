using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MallBuddyApi2.Models.existing
{
    public interface IHasEntrances
    {
        List<Point3D> Entrances { get; set; }
    }
}
