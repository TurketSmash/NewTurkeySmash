using System.Collections.Generic;
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
        List<AnimatedSprite> sortiesTerrain = new List<AnimatedSprite>();
        AnimatedSpriteDef animSortie;

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
        int[][] scoresLife = new int[4][]
        {//{Index,Score,Suicide,Temps}
            new int[] {1,-999,0, -1},
            new int[] {2,-999,0, -1},
            new int[] {3,-999,0, -1},
            new int[] {4,-999,0, -1}
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
        Sprite raquette = new Sprite();
        Sprite invincible = new Sprite();
        #endregion
        int nextItemSpawn;
        int respawnDelay = 0;

        int nextBonusSpawn;
        int bonusSpawnMin;
        int bonusSpawnMax;
        #endregion

        public Level(World _world, string backgroundName, Character[] personnages, ContentManager content)
        {
            nextItemSpawn = ((OptionsCombat.itemSpawnMin + OptionsCombat.itemSpawnMax) / 2) * 1000; //init des premier spawn
            bonusSpawnMax = (int)(OptionsCombat.itemSpawnMax * 1.5f);
            bonusSpawnMin = (int)(OptionsCombat.itemSpawnMin * 1.5f);
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
            raquette.Load(TurkeySmashGame.content, "Jeu\\Objets\\raquette");
            invincible.Load(TurkeySmashGame.content, "Jeu\\Objets\\invincible");

            animSortie = new AnimatedSpriteDef()
            {
                AssetName = "Defaut",
                FrameRate = 60,
                FrameSize = new Point(512, 512),
                Loop = false,
                NbFrames = new Point(5, 1)
            };

            Init(backgroundName);
        }

        public void Update(GameTime gameTime)
        {
            nextItemSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            nextBonusSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            timer += (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (nextItemSpawn < 0)
            {
                spawnItem(0, Vector2.Zero);
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
                    {
                        sortiesTerrain.Add(new AnimatedSprite(ConvertUnits.ToDisplayUnits(personnages[i].bodyPosition), animSortie));
                        respawn(personnages[i]);
                    }
                    if (personnages[i].Mort)
                        personnages[i] = null;
                }

            for (int i = 0; i < sortiesTerrain.Count; i++)
                if (sortiesTerrain[i].FinishedAnimation)
                {
                    sortiesTerrain.RemoveAt(i);
                    i--;
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
                    if (userData.IsUsed == true & userData.BonusType == "raquette")
                    {
                        for (int k = 0; k < 25; k++)
                            spawnItem(4, ConvertUnits.ToDisplayUnits(bonus[i].bodyPosition));
                    }
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

                case "Jeu\\level3\\background3":
                    #region Level 3

                    respawnPoint = new Vector2(Xwin * 0.670f, Ywin * 0.400f);
                    spawnPoints[0] = new Vector2(Xwin * 0.176f, Ywin * 0.737f);
                    spawnPoints[1] = new Vector2(Xwin * 0.744f, Ywin * 0.400f);
                    spawnPoints[2] = new Vector2(Xwin * 0.677f, Ywin * 0.737f);
                    spawnPoints[3] = new Vector2(Xwin * 0.431f, Ywin * 0.742f);


                    bodylist = new StaticPhysicsObject[4];

                    #region Sprites Loading

                    Sprite plateformePrincipale = new Sprite();
                    plateformePrincipale.Load(TurkeySmashGame.content, "Jeu\\level3\\plateforme1");
                    Sprite plateformes = new Sprite();
                    plateformes.Load(TurkeySmashGame.content, "Jeu\\level3\\plateforme2");


                    #endregion

                    #region Bodies
                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.484f, Ywin * 0.860f), 1, plateformePrincipale);
                    bodylist[1] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.810f + (plateformes.Width / 2), Ywin * 0.465f + plateformes.Height / 2), 1, plateformes);
                    bodylist[2] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.578f + (plateformes.Width / 2), Ywin * 0.495f + plateformes.Height / 2), 1, plateformes);
                    bodylist[3] = new StaticPhysicsObject(world, new Vector2(Xwin * 0.330f + plateformes.Width / 2, Ywin * 0.350f + plateformes.Height / 2), 1, plateformes);
                    bodylist[1].body.Rotation = MathHelper.ToRadians(90);
                    bodylist[3].body.Rotation = MathHelper.ToRadians(45);
                    #endregion

                    #region Items
                    #endregion

                    #endregion
                    break;
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
            else
            {
                if (OptionsCombat.TypePartieSelect == "vie")
                    scoresLife[Convert.PlayerIndex2Int(personnage.playerindex) - 1][3] = (int)timer;
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
                personnage.score -= 2;
                tabScores[Convert.PlayerIndex2Int(personnage.playerindex) - 1][2]++;
            }
            else
            {
                if (personnages[userData.LastHit - 1] == null)
                {
                    personnages[userData.LastHit - 1].score++;
                    tabScores[Convert.PlayerIndex2Int(personnage.playerindex) - 1][2]++;

                }
                else
                {
                    personnages[userData.LastHit - 1].score++;
                    personnage.score--;
                    tabScores[userData.LastHit - 1][Convert.PlayerIndex2Int(personnage.playerindex) + 2]++;
                }
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
                    Results.SaveResults(tabScores, "temps");
                    Basic.SetScreen(new EndGameScreen());
                }
            }
            if (OptionsCombat.TypePartieSelect == "vie")
            {
                if (personnages[0] != null & personnages[1] == null & personnages[2] == null & personnages[3] == null |
                    personnages[0] == null & personnages[1] != null & personnages[2] == null & personnages[3] == null | //braaufganzuiafgnzfpuiazgfpauzfpaizfayi
                    personnages[0] == null & personnages[1] == null & personnages[2] != null & personnages[3] == null |
                    personnages[0] == null & personnages[1] == null & personnages[2] == null & personnages[3] != null)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        scoresLife[i][1] = tabScores[i][1];
                        scoresLife[i][2] = tabScores[i][2];
                    }
                    for (int i = 0; i < personnages.Length; i++)
                        if (personnages[i] != null)
                            Results.SaveResults(scoresLife, "vie");
                    Basic.SetScreen(new EndGameScreen());
                }
            }
        }
        void spawnItem(int i, Vector2 position)
        {
            int num = i;
            Vector2 pos = position;

            if (i == 0)
                num = RandomProvider.GetRandom().Next(1, 5);

            if (position == Vector2.Zero)
                pos = new Vector2(RandomProvider.GetRandom().Next((int)(TurkeySmashGame.WindowSize.X / 6), (int)(5 * TurkeySmashGame.WindowSize.X / 6)), TurkeySmashGame.WindowSize.Y / 6);

            PhysicsObject item = null;
            switch (num)
            {
                case 1:
                    itemsSprite.Add(football);
                    item = new RoundPhysicsObject(world, pos, 4, 0.7f, football);
                    break;
                case 2:
                    itemsSprite.Add(caisseSprite);
                    item = new RectPhysicsObject(world, pos, 2.0f, caisseSprite);
                    break;
                case 3:
                    itemsSprite.Add(blocSwag);
                    item = new RectPhysicsObject(world, pos, 3.0f, blocSwag);
                    break;
                case 4:
                    itemsSprite.Add(pingpong);
                    item = new RoundPhysicsObject(world, pos, 5, 0.90f, pingpong);
                    break;
            }
            items.Add(item);
        }
        void spawnBonus()
        {
            PhysicsObject thisBonus = null;
            //int rand = RandomProvider.GetRandom().Next(1, 5);
            int rand = 4;
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
                case 3:
                    thisBonus = new RectPhysicsObject(world, randPosition, 1, raquette);
                    FarseerBodyUserData userData3 = new FarseerBodyUserData
                    {
                        IsCharacter = false,
                        IsBonus = true,
                        IsUsed = false,
                        BonusType = "raquette"
                    };
                    thisBonus.body.UserData = userData3;
                    bonusSprite.Add(raquette);
                    break;
                case 4:
                    thisBonus = new RectPhysicsObject(world, randPosition, 1, invincible);
                    FarseerBodyUserData userData4 = new FarseerBodyUserData
                    {
                        IsCharacter = false,
                        IsBonus = true,
                        IsUsed = false,
                        BonusType = "invincible"
                    };
                    thisBonus.body.UserData = userData4;
                    bonusSprite.Add(invincible);
                    break;
            }
            bonus.Add(thisBonus);
        }
        void resetItemSpawnTimer()
        {
            nextItemSpawn = RandomProvider.GetRandom().Next(OptionsCombat.itemSpawnMin, OptionsCombat.itemSpawnMax);
            nextItemSpawn *= 1000;
        }
        void resetBonusSpawnTimer()
        {
            nextBonusSpawn = RandomProvider.GetRandom().Next(bonusSpawnMin, bonusSpawnMax);
            nextBonusSpawn *= 1000;
        }
    }
}
