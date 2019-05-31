using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirmCalculatorJson
{
    public class Firm
    {
        public string name { get; set; }
        public long balance { get; set; }
        public int size { get; set; }
        public int execs { get; set; }
        public int coo { get; set; }
        public int cfo { get; set; }
        public int assocs { get; set; }
        public int board { get; set; }
        public int tax { get; set; }
        public int rank { get; set; }

        public Firm()
        {
            board = coo + cfo + 1;
        }
    }
}
