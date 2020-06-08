using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WhatsIn.Models
{
    public class UserWords
    {
        public string User { set; get; }

        public Dictionary<string, int> Words { set; get; }

        public UserWords()
        {
            Words = new Dictionary<string, int>();
        }
    }
}
