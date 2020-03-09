using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace battleship
{
    class Field
    {
        public bool Empty { get; set; }
        public bool Hit { get; set; }
        public bool Ship { get; set; }

        public Field() { }
    }
}
