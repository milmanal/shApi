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
        public List<OperationHours> schedule;

        public virtual List<OperationHours> Schedule
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

        public override void LoadForDetails(ApplicationDbContext db)
        {
            base.LoadForDetails(db);
            db.Entry(this).Collection("Schedule").Load();
        }
    }
}