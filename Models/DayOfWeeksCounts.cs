using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsIn.Models
{
    public class DayOfWeeksCount
    {
        public string DayOfWeek { set; get; }

        public int Count { set; get; }

        public DayOfWeeksCount(string _dayOfWeek)
        {
            DayOfWeek = _dayOfWeek;
        }
    }
}
