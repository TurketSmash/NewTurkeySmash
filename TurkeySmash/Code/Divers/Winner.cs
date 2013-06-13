using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TurkeySmash
{
    public static class Winner
    {
        public static int[] winner { get; set; }
        
        public static void saveWinner(int[] scores, string TypeDePartie, int lastStanding)
        {
            if (TypeDePartie == "temps")
            {
                winner = new int[4] { 1, 3, 4, 2 };
                int i = 0;
                while (i < 3)
                {
                    if (scores[i] < scores[i + 1])
                    {
                        scores[i] = scores[i + 1];
                        winner[i] = winner[i + 1];
                        i = 0;
                    }
                    else
                        i++;

                }
                Console.WriteLine(Winner.winner[0] + " / " + Winner.winner[1] + " / " + Winner.winner[2] + " / " + Winner.winner[3]);
                Console.WriteLine(scores[0] + " / " + scores[1] + " / " + scores[2] + " / " + scores[3]);
            }

            if (TypeDePartie == "vie")
            {
                //winner = lastStanding;
            }
        }
    }
}