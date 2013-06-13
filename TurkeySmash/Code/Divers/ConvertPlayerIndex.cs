using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TurkeySmash
{
    public static class Convert
    {
        public static int playerIndex(PlayerIndex index)
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
    }
}
