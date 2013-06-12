using System;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace TurkeySmash
{
    class HUD
    {
        Font[] pourcentages = new Font[4];
        Sprite[] icone = new Sprite[4];
        Font life;
        Font timerFont;
        decimal timer;

        public void Load(Character[] players)
        {
            
            timerFont = new Font(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 10);
            timerFont.NameFont = "Pourcent";
            timerFont.Load(TurkeySmashGame.content);
            timerFont.SizeText = 1.0f;
            life = new Font(TurkeySmashGame.WindowMid);
            life.NameFont = "Pourcent";
            life.Load(TurkeySmashGame.content);
            life.SizeText = 0.8f;

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
                    pourcentages[i].SizeText = 1.0f;
                    icone[i] = new Sprite((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5) - 200,
                                        (7 * TurkeySmashGame.manager.PreferredBackBufferHeight / 8 - 120));
                    if (players[i].definition.AssetName == "Jeu\\narutosheet")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDnaruto");
                    }
                    if (players[i].definition.AssetName == "Jeu\\sakura")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDSakura");
                    }
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
                }
            }
            if (OptionsCombat.TypePartieSelect == "temps")
                timer -= (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;
            else
                timer += (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;

            timerFont.Texte = (Math.Truncate((Math.Round((timer / 1000), 0)) / 60)).ToString() + " : " + (Math.Round((timer / 1000), 0) % 60).ToString();

            life.Texte = (players[0].vie).ToString();
        }

        public void Draw()
        {
            life.Draw(TurkeySmashGame.spriteBatch);
            timerFont.Draw(TurkeySmashGame.spriteBatch);
            for (int i = 0; i < pourcentages.Length; i++)
            {
                if (icone[i] != null)
                    icone[i].Draw(TurkeySmashGame.spriteBatch);
                if (pourcentages[i] != null)
                    pourcentages[i].Draw(TurkeySmashGame.spriteBatch);
            }
        }
    }
}