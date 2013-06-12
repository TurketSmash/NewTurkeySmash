using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TurkeySmash
{
    class HUD
    {
        Font[] pourcentages = new Font[4];
        Sprite[] icone = new Sprite[4];
        Font[] xlife = new Font[4];
        Sprite[] iconeLife = new Sprite[4];

        Font timerFont;
        decimal timer;

        public void Load(Character[] players)
        {
            
            timerFont = new Font(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 10);
            timerFont.NameFont = "Pourcent";
            timerFont.Load(TurkeySmashGame.content);
            timerFont.SizeText = 1.0f;

            if (OptionsCombat.TypePartieSelect == "temps")
                timer = OptionsCombat.TempsPartie * 60000 ; //60*1000 = 1 min en millisecond
            else
                timer = 0;
            
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i] = new Font((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5),
                                            (7 * TurkeySmashGame.manager.PreferredBackBufferHeight / 8));
                    pourcentages[i].NameFont = "Pourcent";
                    pourcentages[i].Load(TurkeySmashGame.content);
                    pourcentages[i].SizeText = 1.3f;

                    icone[i] = new Sprite((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5) - 200,
                                        (7 * TurkeySmashGame.manager.PreferredBackBufferHeight / 8 - 120));

                    xlife[i] = new Font((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth /5),
                        4 * TurkeySmashGame.manager.PreferredBackBufferHeight / 5);

                    iconeLife[i] = new Sprite((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5)-50
                        ,
                        TurkeySmashGame.manager.PreferredBackBufferHeight *0.78f);

                    xlife[i].NameFont = "Pourcent";
                    xlife[i].Load(TurkeySmashGame.content);
                    xlife[i].SizeText = 0.5f;

                    if (players[i].definition.AssetName == "Jeu\\narutosheet")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDnaruto");
                        iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDnarutoLifeIcone");
                    }
                    if (players[i].definition.AssetName == "Jeu\\sakura")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDSakura");
                        iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDsakuraLifeIcone");
                    }
                    iconeLife[i].Scale = 1.2f;
                    icone[i].Scale = 0.7f;
                }
            }
        }

        public void Update(GameTime gameTime, Character[] players)
        {
            for (int i = 0; i < players.Length; i++)
            {
                if (players[i] != null)
                {
                    pourcentages[i].Texte = players[i].pourcent + " %";
                    int gradationRatio = 255 - (255 * players[i].pourcent / 250);
                    pourcentages[i].Color = Color.FromNonPremultiplied(255, gradationRatio, gradationRatio, 255);
                    xlife[i].Texte = "x" + (players[i].vie).ToString();
                    xlife[i].Color = Color.White;
                }
                else
                {
                    pourcentages[i] = null;
                    icone[i] = null;
                    iconeLife[i] = null;
                    xlife[i] = null;
                }
            }
            if (OptionsCombat.TypePartieSelect == "temps")
                timer -= (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;
            else
                timer += (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;

            timerFont.Texte = (Math.Truncate((Math.Round((timer / 1000), 0)) / 60)).ToString() + " : " + (Math.Round((timer / 1000), 0) % 60).ToString();

        }

        public void Draw()
        {
            timerFont.Draw(TurkeySmashGame.spriteBatch);
            for (int i = 0; i < pourcentages.Length; i++)
            {
                if (icone[i] != null)
                    icone[i].Draw(TurkeySmashGame.spriteBatch);
                if (iconeLife[i] != null)
                    iconeLife[i].Draw(TurkeySmashGame.spriteBatch);
                if (pourcentages[i] != null)
                    pourcentages[i].Draw(TurkeySmashGame.spriteBatch);
                if (icone[i] != null)
                    xlife[i].Draw(TurkeySmashGame.spriteBatch);
            }
        }
    }
}