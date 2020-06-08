using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsIn.Models
{
    public class HourUsersCount
    {
        public string User { set; get; }

        public int Hour { set; get; }

        public int Count { set; get; }
    }
}
