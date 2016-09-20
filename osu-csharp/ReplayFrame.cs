using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OsuAPI.ReplayParser
{

    public class ReplayFrame
    {

        public int TimeDiff;
        public int Time;
        public float TimeInSeconds { get { return Time / 1000.0f; } }
        public float X { get; set; }
        public float Y { get; set; }
        public Keys Keys { get; set; }

    }

}
