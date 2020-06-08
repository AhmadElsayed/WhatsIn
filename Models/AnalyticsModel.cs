using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsIn.Models
{
    public class AnalyticsModel
    {
        public Dictionary<DateTime, int> DateCounts { set; get; }

        public Dictionary<DayOfWeek, int[]> DayOfWeekHours { set; get; }

        public Dictionary<string, int[]> HoursActivity { set; get; } // Hour / User / Count 

        public Dictionary<DayOfWeek, DayOfWeeksCount> DayofWeekActivity { set; get; } // Day of Week / User / Count

        public Dictionary<string, int> UsersMessages { set; get; } // Name/Count

        public Dictionary<string, Dictionary<string, int>> UserWords { set; get; }

        public Dictionary<string, int> UsersMedia { set; get; }

        public AnalyticsModel() {
            DateCounts = new Dictionary<DateTime, int>();
            
            DayOfWeekHours = new Dictionary<DayOfWeek, int[]>();

            HoursActivity = new Dictionary<string, int[]>();
            
            DayofWeekActivity = new Dictionary<DayOfWeek, DayOfWeeksCount>();
            
            for(int i = 0; i < 7; i++)
            {
                var value = (DayOfWeek)i;
                DayOfWeekHours.Add(value, new int[24]);
                DayofWeekActivity.Add(value, new DayOfWeeksCount(value.ToString()));
            }

            UsersMessages = new Dictionary<string, int>();
            UsersMedia = new Dictionary<string, int>();
            UserWords = new Dictionary<string, Dictionary<string, int>>();
        }

    }
}
