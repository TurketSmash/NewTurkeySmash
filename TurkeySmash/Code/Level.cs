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
        List<PhysicsObject> items = new List<PhysicsObject>();
        List<Sprite> itemsSprite = new List<Sprite>();
        decimal timer;
        float Xwin = TurkeySmashGame.WindowSize.X;
        float Ywin = TurkeySmashGame.WindowSize.Y;
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
            int nextItemSpawn=0;
            int itemSpawnMin = 10;
            int itemSpawnMax = 15;
        #endregion

        public Level(World _world, string backgroundName, Character[] personnages, ContentManager content)
        {
            nextItemSpawn = ((itemSpawnMin + itemSpawnMax) / 2) * 1000; //init du premier spawn
            this.world = _world;
            this.personnages = personnages;

            background = new Sprite();
            background.Load(content, backgroundName);

            football.Load(TurkeySmashGame.content, "Jeu\\level2\\football");
            caisseSprite.Load(TurkeySmashGame.content, "Jeu\\level1\\caisse");
            blocSwag.Load(TurkeySmashGame.content, "Jeu\\level2\\blocSwag");
            pingpong.Load(TurkeySmashGame.content, "Jeu\\PinGPong");


            switch (backgroundName)
            {
                case "Jeu\\level1\\background1":
                    #region Level 1

                    respawnPoint = new Vector2(Xwin * 0.510f, Ywin * 0.444f);
                    spawnPoints[0] = new Vector2(Xwin * 0.331f,Ywin * 0.528f);
                    spawnPoints[1] = new Vector2(Xwin * 0.729f,Ywin * 0.444f);
                    spawnPoints[2] = new Vector2(Xwin * 0.106f,Ywin * 0.537f); 
                    spawnPoints[3] = new Vector2(Xwin * 0.621f,Ywin * 0.557f);

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

                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(Xwin*0.111f, Ywin*0.611f), 1, plateforme2);
                    bodylist[1] = new StaticPhysicsObject(world, new Vector2(Xwin*0.512f, Ywin*0.515f), 1, plateforme2); //3 plateformes volantes
                    bodylist[2] = new StaticPhysicsObject(world, new Vector2(Xwin*0.735f, Ywin*0.520f), 1, plateforme2);

                    bodylist[3] = new StaticPhysicsObject(world, new Vector2((Xwin/2) - (plateforme1Mid.Width/2) + (plateforme1Left.Width/2),(2*Ywin/3)-(plateforme1Mid.Height /2) - (plateforme1Left.Height/2)+1), 1, plateforme1Left); //partie gauche de la plateforme centrale
                    bodylist[4] = new StaticPhysicsObject(world, new Vector2(Xwin/2, 2*Ywin/3), 1, plateforme1Mid); //partie central de la plateforme centrale
                    //bodylist[5] = new StaticPhysicsObject(world, new Vector2((Xwin/2) + (plateforme1Mid.Width/2) - (plateforme1Right.Width/2) -1,(2*Ywin/3)-(plateforme1Mid.Height /2) - (plateforme1Right.Height/2)+1), 1, plateforme1Right); //partie droite de la plateforme centrale
                    
                    #endregion

                    #region Items

                    #endregion

                    #endregion
                    break;

                case "Jeu\\level2\\background2":
                    #region Level 2
                    
                    respawnPoint = new Vector2(Xwin * 0.427f, Ywin * 0.417f);
                    spawnPoints[0] = new Vector2(Xwin * 0.328f,Ywin * 0.602f);
                    spawnPoints[1] = new Vector2(Xwin * 0.620f, Ywin * 0.602f);
                    spawnPoints[2] = new Vector2(Xwin * 0.463f,Ywin * 0.546f);
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
                    Sprite plateformeBlocVert = new Sprite();
                    plateformeBlocVert.Load(TurkeySmashGame.content, "Jeu\\level2\\plateformeBlocVert");

                    #endregion
                    
                    #region Bodies
                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(Xwin*0.479f , Ywin*0.741f) , 1, plateforme3);
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
        public void Update(GameTime gameTime)
        {
            if (Microsoft.Xna.Framework.Input.Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.NumPad1))
                spawnObject();
            nextItemSpawn -= gameTime.ElapsedGameTime.Milliseconds;
            if (nextItemSpawn < 0)
            {
                spawnObject();
                resetItemSpawnTimer();
            }

            timer += (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;
            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                {
                    foreach (IA bolo in personnages.OfType<IA>())
                        bolo.UpdatePosition(personnages);

                    personnages[i].Update(gameTime);
                    gameFinished(personnages[i]);

                    if (outOfScreen(personnages[i].bodyPosition))
                        respawn(personnages[i]);
                    if (personnages[i].Mort)
                        personnages[i] = null;
                }
            }

            for (int i = 0; i < items.Count; i++)
            {
                if (outOfScreen(items[i].bodyPosition))
                {
                    items.RemoveAt(i);
                    itemsSprite.RemoveAt(i);
                }
            }
            //System.Console.WriteLine(items.Count);
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            background.DrawAsBackground(spriteBatch);

            if (items != null)
            {
                for (int i = 0; i < items.Count; i++)
                {
                    items[i].Draw(itemsSprite[i], spriteBatch);
                }
            }
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
                   || position.Y < ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y * -0.1f));
        }
        void respawn(Character personnage)
        {
            scoring(personnage);
            FarseerBodyUserData userData = (FarseerBodyUserData)personnage.body.UserData;
            if (personnage.vie == 1)
                personnage.vie = 0;
            if (!personnage.Mort)
            {
                personnage.bodyPosition = ConvertUnits.ToSimUnits(respawnPoint);
                if (OptionsCombat.TypePartieSelect != "temps")
                    personnage.vie--;
            }
            userData.lastHit = 0;
            userData.pourcent = 0;
            personnage.body.ResetDynamics();
        }
        private void scoring(Character personnage)
        {
            FarseerBodyUserData userData = (FarseerBodyUserData)personnage.body.UserData;
            if (userData.lastHit <= 0)
            {
                personnage.score -= 2;//Suicide = -2 pts
                tabScores[Convert.PlayerIndex2Int(personnage.playerindex) - 1][2] ++; //Ajout +1 suicide dans le compteur
            }
            else
            {
                if (personnages[userData.lastHit - 1] != null)
                    personnages[userData.lastHit - 1].score ++; //score ++ pour le dernier perso qui t'as tapé
                tabScores[userData.lastHit - 1][Convert.PlayerIndex2Int(personnage.playerindex) + 2]++; //Ajout +1 au score de kill pour le joueur qui t'as tué
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
                    Results.SaveResults(tabScores);
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
                    Results.SaveResults(tabScores);
                    Basic.SetScreen(new EndGameScreen());
                }
            }
        }
        void spawnObject()
        {
            int rnd = new Random().Next(1, 5);
            int Xrand = new Random().Next((int)(TurkeySmashGame.WindowSize.X / 6), (int)((5/6) * TurkeySmashGame.WindowSize.X));
            switch (rnd)
            {
                case 1:
                    itemsSprite.Add(football);
                    items.Add(new RoundPhysicsObject(world, new Vector2(Xrand, TurkeySmashGame.WindowSize.Y / 6), 4, 0.7f, football));
                    break;
                case 2:
                    itemsSprite.Add(caisseSprite);
                    items.Add(new RectPhysicsObject(world, new Vector2(Xrand, TurkeySmashGame.WindowSize.Y / 6), 2.0f, caisseSprite));
                    break;
                case 3:
                    itemsSprite.Add(blocSwag);
                    items.Add(new RectPhysicsObject(world, new Vector2(Xrand, TurkeySmashGame.WindowSize.Y / 6), 3.0f, blocSwag));
                    break;
                case 4:
                    itemsSprite.Add(pingpong);
                    items.Add(new RoundPhysicsObject(world, new Vector2(Xrand, TurkeySmashGame.WindowSize.Y / 6), 2, 0.90f, pingpong));
                    break;
            }
        }
        void resetItemSpawnTimer()
        {
            nextItemSpawn = new System.Random().Next(itemSpawnMin, itemSpawnMax);
            nextItemSpawn = nextItemSpawn * 1000;
        }
    }
}
