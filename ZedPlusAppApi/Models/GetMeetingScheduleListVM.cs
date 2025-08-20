using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ZedPlusAppApi.Models
{
    public class GetMeetingScheduleListVM
    {
        public long ID { get; set; }
        public string MeetingDate { get; set; }
        public string MeetingCity { get; set; }
        public string Location { get; set; }
        public string Timing { get; set; }
        public string EntryDate { get; set; }
        public string Status { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

    }
}