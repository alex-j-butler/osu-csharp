using System;

namespace OsuAPI.ReplayParser
{

    public class LifeFrame
    {

        public int Time;
        public float TimeInSeconds { get { return Time / 1000.0f; } }
        public float LifePercent;

    }

}
