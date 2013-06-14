using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace TurkeySmash
{
    public static class Results
    {
        public static int[][] ResultsBoard { get; set; }

        public static void SaveResults(int[][] scores, string TypeDePartie)
        {
            if (TypeDePartie == "temps")
            {
                int[] aux;
                int i = 0;
                while (i < 3)
                {
                    if (scores[i][1] < scores[i + 1][1])
                    {
                        aux = scores[i + 1];
                        scores[i + 1] = scores[i];
                        scores[i] = aux;
                        i = 0;
                    }
                    else
                        i++;
                }
                ResultsBoard = scores;
            }

            if (TypeDePartie == "vie")
            {
                //winner = lastStanding;
            }
        }
    }
}