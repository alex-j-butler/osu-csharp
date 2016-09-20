using System;

namespace OsuAPI.ReplayParser
{

    public enum Keys
    {

        None = 0,
        M1 = (1 << 0),
        M2 = (1 << 1),
        K1 = (1 << 2) | (1 << 0),
        K2 = (1 << 3) | (1 << 1)
        
    }

}
