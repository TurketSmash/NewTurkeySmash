using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TurkeySmash
{
    class HUD
    {
        Font[] pourcentages = new Font[4];

        public void Load(Character[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i] = new Font((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5),
                                            (7 * TurkeySmashGame.manager.PreferredBackBufferHeight / 8));
                    pourcentages[i].NameFont = "Pourcent";
                    pourcentages[i].Load(TurkeySmashGame.content);
                    pourcentages[i].SizeText = 1.0f;
                }
            }
        }

        public void Update(Character[] players)
        {
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i].Texte = players[i].pourcent + " %";
                    pourcentages[i].Color = new Color(255, 255 - (255 * ((int)players[i].pourcent / 100)), 255 - (255 * ((int)players[i].pourcent / 100)));
                }

            }
        }

        public void Draw()
        {
            for (int i = 0; i < pourcentages.Length; i++)
            {
                if (pourcentages[i] != null)
                    pourcentages[i].Draw(TurkeySmashGame.spriteBatch);
            }
        }
    }
}