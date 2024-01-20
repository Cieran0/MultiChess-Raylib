using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiChess
{
    public struct Move
    {
        public int relativeX;
        public int relativeY;
        public bool moving;
        public bool jumping;
        public bool taking;
        public bool first;
    }
}
