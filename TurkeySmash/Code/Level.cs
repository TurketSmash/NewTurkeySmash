﻿using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Content;
using System;

namespace TurkeySmash
{
    class Level
    {
        #region Fields
        World world;
        Sprite background;
        StaticPhysicsObject[] bodylist;

        List<PhysicsObject> items = new List<PhysicsObject>();
        List<Sprite> itemsSprite = new List<Sprite>();

        List<PhysicsObject> bonus = new List<PhysicsObject>();
        List<Sprite> bonusSprite = new List<Sprite>();

        decimal timer;
        public Vector2[] spawnPoints = new Vector2[4];
        Vector2 respawnPoint;
        Character[] personnages;
        int[][] tabScores = new int[4][]
        {//{Index,Score,Suicide,J1Kills,J2Kills,J3Kills,J4Kills}
            new int[] {1,-999,0,0,0,0,0},
            new int[] {2,-999,0,0,0,0,0},
            new int[] {3,-999,0,0,0,0,0},
            new int[] {4,-999,0,0,0,0,0}
        };
            #region Items
            Sprite caisseSprite = new Sprite(); //items.Add(new RectPhysicsObject(world, TurkeySmashGame.WindowMid, 3.0f, caisseSprite));
            Sprite blocSwag = new Sprite();
            Sprite football = new Sprite(); //items.Add(new RoundPhysicsObject(world, TurkeySmashGame.WindowMid, 4, 0.7f, football));
            Sprite pingpong = new Sprite();
            #endregion
            #region Bonus
            Sprite bonusTest = new Sprite();
            Sprite hamburger = new Sprite();
            #endregion
            int nextItemSpawn;
            int itemSpawnMin = 10;
            int itemSpawnMax = 15;

            int nextBonusSpawn;
            int bonusSpawnMin;
            int bonusSpawnMax;
        #endregion

        public Level(World _world, string backgroundName, Character[] personnages, ContentManager content)
        {
            nextItemSpawn = ((itemSpawnMin + itemSpawnMax) / 2) * 1000; //init des premier spawn
            bonusSpawnMax = (int)(itemSpawnMax * 1.5f);
            bonusSpawnMin = (int)(itemSpawnMin * 1.5f);
            nextBonusSpawn = ((bonusSpawnMax + bonusSpawnMin) / 2) * 1000;
            this.world = _world;
            this.personnages = personnages;

            background = new Sprite();
            background.Load(content, backgroundName);

            football.Load(TurkeySmashGame.content, "Jeu\\level2\\football");
            caisseSprite.Load(TurkeySmashGame.content, "Jeu\\level1\\caisse");
            blocSwag.Load(TurkeySmashGame.content, "Jeu\\level2\\blocSwag");
            pingpong.Load(TurkeySmashGame.content, "Jeu\\Objets\\PingPong");

            bonusTest.Load(TurkeySmashGame.content, "Jeu\\Objets\\BonusTest");
            hamburger.Load(TurkeySmashGame.content, "Jeu\\Objets\\hamburger");

            Init(backgroundName);
        }

        public void Update(GameTime gameTime)
        {
            nextItemSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            nextBonusSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            timer += (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (nextItemSpawn < 0)
            {
                spawnItem();
                resetItemSpawnTimer();
            } if (nextBonusSpawn < 0)
            {
                spawnBonus();
                resetBonusSpawnTimer();
            }

            for (int i = 0; i < personnages.Length; i++)
                if (personnages[i] != null)
                {
                    foreach (IA ia in personnages.OfType<IA>())
                        ia.UpdatePosition(personnages);

                    personnages[i].Update(gameTime);
                    gameFinished(personnages[i]);

                    if (outOfScreen(personnages[i].bodyPosition))
                        respawn(personnages[i]);
                    if (personnages[i].Mort)
                        personnages[i] = null;
                }

            for (int i = 0; i < items.Count; i++)
                if (outOfScreen(items[i].bodyPosition))
                {
                    world.RemoveBody(items[i].body);
                    items.RemoveAt(i);
                    itemsSprite[i] = null;
                    itemsSprite.RemoveAt(i);
                }

            for (int i = 0; i < bonus.Count; i++)
            {
            FarseerBodyUserData userData = (FarseerBodyUserData)bonus[i].body.UserData;
            if (outOfScreen(bonus[i].bodyPosition) | userData.IsUsed == true)
                {
                    world.RemoveBody(bonus[i].body);
                    bonus.RemoveAt(i);
                    bonusSprite[i] = null;
                    bonusSprite.RemoveAt(i);
                }
            }
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            background.DrawAsBackground(spriteBatch);

            for (int i = 0; i < items.Count; i++)
                if (items[i] != null)
                    items[i].Draw(itemsSprite[i], spriteBatch);

            for (int i = 0; i < bonus.Count; i++)
                if (bonus[i] != null)
                    bonus[i].Draw(bonusSprite[i], spriteBatch);

            foreach (StaticPhysicsObject elements in bodylist)
            {
                if (elements != null)
                    elements.Draw(elements.sprite, spriteBatch);
            }
            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                    personnages[i].Draw(spriteBatch);
            }
        }
        bool outOfScreen(Vector2 position)
        {
            return (position.X < ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X * -0.1f)
                   || position.X > ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X + TurkeySmashGame.WindowSize.X * 0.1f)
                   || position.Y > ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y + TurkeySmashGame.WindowSize.Y * 0.1f)
                   || position.Y < ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y * -0.5f));
        }
        void respawn(Character personnage)
        {
            FarseerBodyUserData userData = (FarseerBodyUserData)personnage.body.UserData;
            scoring(personnage);
            if (personnage.vie == 1 & OptionsCombat.TypePartieSelect == "vie")
                personnage.vie = 0;
            if (!personnage.Mort)
            {
                personnage.bodyPosition = ConvertUnits.ToSimUnits(respawnPoint);
                if (OptionsCombat.TypePartieSelect != "temps")
                    personnage.vie--;
            }
            userData.LastHit = 0;
            userData.Pourcent = 0;
            personnage.body.ResetDynamics();
        }
        private void scoring(Character personnage)
        {
            FarseerBodyUserData userData = (FarseerBodyUserData)personnage.body.UserData;
            if (userData.LastHit <= 0)
            {
                personnage.score -= 2;//Suicide = -2 pts
                tabScores[Convert.PlayerIndex2Int(personnage.playerindex) - 1][2] ++; //Ajout +1 suicide dans le compteur
            }
            else
            {
                if (personnages[userData.LastHit - 1] != null)
                    personnages[userData.LastHit - 1].score ++; //score ++ pour le dernier perso qui t'as tapé
                tabScores[userData.LastHit - 1][Convert.PlayerIndex2Int(personnage.playerindex) + 2]++; //Ajout +1 au score de kill pour le joueur qui t'as tué
            }

            for (int i = 0; i < personnages.Length; i++)
                if (personnages[i] != null)
                    tabScores[i][1] = personnages[i].score;
        }
        void gameFinished(Character personnage)
        {
            if (OptionsCombat.TypePartieSelect == "temps")
            {
                if (timer >= OptionsCombat.TempsPartie * 1000 * 60)
                {
                    timer = 0;
                    Results.SaveResults(tabScores,-1);
                    Basic.SetScreen(new EndGameScreen());
                }
            }
            if (OptionsCombat.TypePartieSelect == "vie")
            {
                if (personnages[0] != null & personnages[1] == null & personnages[2] == null & personnages[3] == null |
                    personnages[0] == null & personnages[1] != null & personnages[2] == null & personnages[3] == null | //braaufganzuiafgnzfpuiazgfpauzfpaizfayi
                    personnages[0] == null & personnages[1] == null & personnages[2] != null & personnages[3] == null |
                    personnages[0] == null & personnages[1] == null & personnages[2] == null & personnages[3] != null )
                {
                    for (int i = 0; i < personnages.Length; i++)
                        if (personnages[i] != null)
                            Results.SaveResults(tabScores,i+1);
                    Basic.SetScreen(new EndGameScreen());
                }
            }
        }
        void spawnItem()

        {
            PhysicsObject item = null;
            int rand = RandomProvider.GetRandom().Next(1, 5);
            int Xrand = RandomProvider.GetRandom().Next((int)(TurkeySmashGame.WindowSize.X / 6), (int)(5 * TurkeySmashGame.WindowSize.X / 6));
            Vector2 randposition = new Vector2(Xrand, TurkeySmashGame.WindowSize.Y / 6);
            switch (rand)
            {
                case 1:
                    itemsSprite.Add(football);
                    item = new RoundPhysicsObject(world, randposition, 4, 0.7f, football);
                    break;
                case 2:
                    itemsSprite.Add(caisseSprite);
                    item = new RectPhysicsObject(world, randposition, 2.0f, caisseSprite);
                    break;
                case 3:
                    itemsSprite.Add(blocSwag);
                    item = new RectPhysicsObject(world, randposition, 3.0f, blocSwag);
                    break;
                case 4:
                    itemsSprite.Add(pingpong);
                    item = new RoundPhysicsObject(world, randposition, 5, 0.90f, pingpong);
                    break;
            }
            items.Add(item);
        }
        void spawnBonus()
        {
            PhysicsObject thisBonus = null;
            int rand = RandomProvider.GetRandom().Next(1, 3);
            int Xrand = RandomProvider.GetRandom().Next((int)(TurkeySmashGame.WindowSize.X / 6), (int)(5 * TurkeySmashGame.WindowSize.X / 6));
            Vector2 randPosition = new Vector2(Xrand, TurkeySmashGame.WindowSize.Y / 6);
            FarseerBodyUserData userData = new FarseerBodyUserData();
            switch (rand)
            {
                case 1:
                    thisBonus = new RectPhysicsObject(world, randPosition, 1, bonusTest);
                    bonusSprite.Add(bonusTest);
                    FarseerBodyUserData userData1 = new FarseerBodyUserData
                    {
                        IsCharacter = false,
                        IsBonus = true,
                        IsUsed = false,
                        BonusType = "vie"
                    };
                    thisBonus.body.UserData = userData1;
                    break;
                case 2:
                    thisBonus = new RectPhysicsObject(world, randPosition, 1, hamburger);
                    FarseerBodyUserData userData2 = new FarseerBodyUserData
                    {
                        IsCharacter = false,
                        IsBonus = true,
                        IsUsed = false,
                        BonusType = "pourcent"
                    };
                    thisBonus.body.UserData = userData2;
                    bonusSprite.Add(hamburger);
                    break;
            }
            bonus.Add(thisBonus);
        }
        void resetItemSpawnTimer()
        {
            nextItemSpawn = RandomProvider.GetRandom().Next(itemSpawnMin, itemSpawnMax);
            Console.WriteLine(timer / 1000 + nextItemSpawn);
            nextItemSpawn *= 1000;
        }
        void resetBonusSpawnTimer()
        {
            nextBonusSpawn = RandomProvider.GetRandom().Next(bonusSpawnMin, bonusSpawnMax);
            Console.WriteLine(timer / 1000 + nextBonusSpawn);
            nextBonusSpawn *= 1000;
        }
        void Init(string backgroundName)
        {
            float Xwin = TurkeySmashGame.WindowSize.X;
            float Ywin = TurkeySmashGame.WindowSize.Y;

            switch (backgroundName)
            {
                case "Jeu\\level1\\background1":
                    #region Level 1

                    respawnPoint = new Vector2(Xwin * 0.510f, Ywin * 0.444f);
                    spawnPoints[0] = new Vector2(Xwin * 0.331f, Ywin * 0.528f);
                    spawnPoints[1] = new Vector2(Xwin * 0.729f, Ywin * 0.444f);
                    spawnPoints[2] = new Vector2(Xwin * 0.106f, Ywin * 0.537f);
                    spawnPoints[3] = new Vector2(Xwin * 0.621f, Ywin * 0.557f);

                    bodylist = new StaticPhysicsObject[5];

                    #region Sprites Loading

                    Sprite plateforme1Mid = new Sprite();
                    plateforme1Mid.Load(TurkeySmashGame.content, "Jeu\\level1\\plateforme1Mid");
                    Sprite plateforme1Left = new Sprite();
                    plateforme1Left.Load(TurkeySmashGame.content, "Jeu\\level1\\plateforme1Left");
                    //Sprite plateforme1Right = new Sprite();
                    //plateforme1Right.Load(TurkeySmashGame.content, "Jeu\\plateforme1Right");   //créé un bug CHELOU
                    Sprite plateforme2 = new Sprite();
                    plateforme2.Load(TurkeySmashGame.content, "Jeu\\level1\\plateforme2");


                    #endregion

                    #region Bodies

                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.111f, Ywin * 0.611f), 1, plateforme2);
                    bodylist[1] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.512f, Ywin * 0.515f), 1, plateforme2); //3 plateformes volantes
                    bodylist[2] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.735f, Ywin * 0.520f), 1, plateforme2);

                    bodylist[3] = new StaticPhysicsObject(world, new Vector2((Xwin / 2) - (plateforme1Mid.Width / 2) + (plateforme1Left.Width / 2), (2 * Ywin / 3) - (plateforme1Mid.Height / 2) - (plateforme1Left.Height / 2) + 1), 1, plateforme1Left); //partie gauche de la plateforme centrale
                    bodylist[4] = new StaticPhysicsObject(world, new Vector2(Xwin / 2, 2 * Ywin / 3), 1, plateforme1Mid); //partie central de la plateforme centrale
                    //bodylist[5] = new StaticPhysicsObject(world, new Vector2((Xwin/2) + (plateforme1Mid.Width/2) - (plateforme1Right.Width/2) -1,(2*Ywin/3)-(plateforme1Mid.Height /2) - (plateforme1Right.Height/2)+1), 1, plateforme1Right); //partie droite de la plateforme centrale

                    #endregion

                    #region Items

                    #endregion

                    #endregion
                    break;

                case "Jeu\\level2\\background2":
                    #region Level 2

                    respawnPoint = new Vector2(Xwin * 0.427f, Ywin * 0.417f);
                    spawnPoints[0] = new Vector2(Xwin * 0.328f, Ywin * 0.602f);
                    spawnPoints[1] = new Vector2(Xwin * 0.620f, Ywin * 0.602f);
                    spawnPoints[2] = new Vector2(Xwin * 0.463f, Ywin * 0.546f);
                    spawnPoints[3] = new Vector2(Xwin * 0.941f, Ywin * 0.638f);


                    bodylist = new StaticPhysicsObject[4];

                    #region Sprites Loading

                    Sprite plateforme3 = new Sprite();
                    plateforme3.Load(TurkeySmashGame.content, "Jeu\\level2\\plateforme3");
                    Sprite plateformeBlocBleu = new Sprite();
                    plateformeBlocBleu.Load(TurkeySmashGame.content, "Jeu\\level2\\plateformeBlocBleu");
                    Sprite plateformeBlocJaune = new Sprite();
                    plateformeBlocJaune.Load(TurkeySmashGame.content, "Jeu\\level2\\plateformeBlocJaune");
                    Sprite plateformeBlocRouge = new Sprite();
                    plateformeBlocRouge.Load(TurkeySmashGame.content, "Jeu\\level2\\plateformeBlocRouge");


                    #endregion

                    #region Bodies
                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.479f, Ywin * 0.741f), 1, plateforme3);
                    bodylist[1] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.861f + (plateformeBlocBleu.Width / 2), Ywin * 0.64f + plateformeBlocBleu.Height / 2), 1, plateformeBlocBleu);
                    bodylist[2] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.191f + (plateformeBlocJaune.Width / 2), Ywin * 0.457f + plateformeBlocJaune.Height / 2), 1, plateformeBlocJaune);
                    bodylist[3] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.383f + plateformeBlocRouge.Width / 2, Ywin * 0.55f + plateformeBlocRouge.Height / 2), 1, plateformeBlocRouge);

                    #endregion

                    #region Items
                    #endregion

                    #endregion
                    break;
            }
        }
    }
}
