using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TurkeySmash
{
    class HUD
    {
        Font[] pourcentages = new Font[4];
        Color[] color = new Color[4];

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
            pourcentages[0].Color = Color.Red; //= color[0];
            //pourcentages[1].Color = Color.Black;
        }

        public void Update(Character[] players)
        {
            for(int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i].Texte = string.Format("{0} %", players[i].pourcent);
                    color[i] = new Color(255, 255 - (255 * ((int)players[i].pourcent / 100)), 255 - (255 * ((int)players[i].pourcent / 100)));
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