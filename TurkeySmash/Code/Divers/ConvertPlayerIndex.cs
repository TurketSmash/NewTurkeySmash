using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TurkeySmash
{
    public static class Convert
    {
        public static int PlayerIndex2Int(PlayerIndex index)
        {
            switch (index)
            {
                case PlayerIndex.One:
                    return 1;
                case PlayerIndex.Two:
                    return 2;
                case PlayerIndex.Three:
                    return 3;
                case PlayerIndex.Four:
                    return 4;
            }
            return -1;
        }
        public static PlayerIndex Int2PlayerIndex(int integer)
        {
            switch (integer)
            {
                case 1:
                    return PlayerIndex.One;
                case 2:
                    return PlayerIndex.Two;
                case 3:
                    return PlayerIndex.Three;
                case 4:
                    return PlayerIndex.Four;
            }
            return PlayerIndex.One;
        }
    }
}
