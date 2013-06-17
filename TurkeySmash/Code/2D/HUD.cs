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
        Font[] scores = new Font[4];

        Font timerFont;
        decimal timer;

        public void Load(Character[] players)
        {
            timerFont = new Font(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 10);
            timerFont.NameFont = "Pourcent";
            timerFont.Load(TurkeySmashGame.content);
            timerFont.SizeText = 1.0f;
            if (SelectionNiveau.niveauSelect == "level2")
                timerFont.Color = Color.White;

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

                    icone[i] = new Sprite((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5) - 250,
                                        (7 * TurkeySmashGame.manager.PreferredBackBufferHeight / 8 - 120));
                    if (OptionsCombat.TypePartieSelect == "vie")
                    {
                        xlife[i] = new Font((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5),
                            4 * TurkeySmashGame.manager.PreferredBackBufferHeight / 5);

                        iconeLife[i] = new Sprite((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5) - 50
                            ,TurkeySmashGame.manager.PreferredBackBufferHeight * 0.78f);

                        xlife[i].NameFont = "Pourcent";
                        xlife[i].Load(TurkeySmashGame.content);
                        xlife[i].SizeText = 0.5f;
                    }
                    if (OptionsCombat.TypePartieSelect == "temps")
                    {
                        scores[i] = new Font((i + 1) * (TurkeySmashGame.manager.PreferredBackBufferWidth / 5),
                            4 * TurkeySmashGame.manager.PreferredBackBufferHeight / 5);
                        scores[i].NameFont = "Pourcent";
                        scores[i].Load(TurkeySmashGame.content);
                        scores[i].SizeText = 0.6f;
                    }

                    if (players[i].definition.AssetName == "Jeu\\naruto")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDnaruto");
                        if (OptionsCombat.TypePartieSelect == "vie")
                            iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDnarutoLifeIcone");
                    }

                    if (players[i].definition.AssetName == "Jeu\\sakura")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDSakura");
                        if (OptionsCombat.TypePartieSelect == "vie")
                            iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDsakuraLifeIcone");
                    }

                    if (players[i].definition.AssetName == "Jeu\\sai")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDsai");
                        if (OptionsCombat.TypePartieSelect == "vie")
                            iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDsaiLifeIcone");
                    }

                    if (players[i].definition.AssetName == "Jeu\\suigetsu")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDsuigetsu");
                        if (OptionsCombat.TypePartieSelect == "vie")
                            iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDsuigetsuLifeIcone");
                    }
                    if (players[i].definition.AssetName == "Jeu\\turkey")
                    {
                        icone[i].Load(TurkeySmashGame.content, "HUD\\HUDTurkey");
                        if (OptionsCombat.TypePartieSelect == "vie")
                            iconeLife[i].Load(TurkeySmashGame.content, "HUD\\HUDTurkeyLifeIcone");
                    }
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
                    if (OptionsCombat.TypePartieSelect == "vie")
                    {
                        xlife[i].Texte = "x" + (players[i].vie).ToString();
                        xlife[i].Color = Color.White;
                    }
                    if (OptionsCombat.TypePartieSelect == "temps")
                    {
                        scores[i].Texte = players[i].score.ToString();
                        scores[i].Color = Color.White;
                    }
                }
                else
                {
                    pourcentages[i] = null;
                    icone[i] = null;
                    if (OptionsCombat.TypePartieSelect == "vie")
                    {
                        iconeLife[i] = null;
                        xlife[i] = null;
                    }
                    if (OptionsCombat.TypePartieSelect == "temps")
                    {
                        scores[i] = null;
                    }
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
                if (pourcentages[i] != null)
                    pourcentages[i].Draw(TurkeySmashGame.spriteBatch);
                if (OptionsCombat.TypePartieSelect == "vie")
                {
                    if (iconeLife[i] != null)
                        iconeLife[i].Draw(TurkeySmashGame.spriteBatch);
                    if (xlife[i] != null)
                        xlife[i].Draw(TurkeySmashGame.spriteBatch);
                }
                if (OptionsCombat.TypePartieSelect == "temps")
                    if (scores[i] != null)
                        scores[i].Draw(TurkeySmashGame.spriteBatch);
            }
        }
    }
}