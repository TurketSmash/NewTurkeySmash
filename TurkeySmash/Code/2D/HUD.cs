using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TurkeySmash
{
    class HUD
    {
        Font[] pourcentages = new Font[4];

        public HUD() { }

        public void Load(Character[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i] = new Font((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 4),
                                            (3.5f * TurkeySmashGame.manager.PreferredBackBufferHeight / 4));
                    pourcentages[i].NameFont = "Pourcent";
                    pourcentages[i].Load(TurkeySmashGame.content);
                    pourcentages[i].SizeText = 1.0f;
                }
            }
            pourcentages[0].Color = Color.Red;
        }

        public void Update(Character[] players)
        {
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i].Texte = Convert.ToString(players[i].pourcent) + " %";
                    i++;
                }

            }
        }

        public void Draw()
        {
            TurkeySmashGame.spriteBatch.Begin();

            for (int i = 0; i < pourcentages.Length; i++)
            {
                if (pourcentages[i] != null)
                    pourcentages[i].Draw(TurkeySmashGame.spriteBatch);
            }

            TurkeySmashGame.spriteBatch.End(); 
        }
    }
}