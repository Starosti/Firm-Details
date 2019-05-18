using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmCalculator
{
    public class Firm
    {
        public int id { get; set; }
        public string name { get; set; }
        public long balance { get; set; }
        public int size { get; set; }
        public int execs { get; set; }
        public int assocs { get; set; }
        public int coo { get; set; }
        public int cfo { get; set; }
        public int tax { get; set; }
        public int rank { get; set; }
        public int last_payout { get; set; }
    }
}
