using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Content;

namespace TurkeySmash
{
    class Level
    {
        Sprite background;
        StaticPhysicsObject[] bodylist;
        PhysicsObject[] items;
        Sprite[] itemsSprite;

        public Vector2[] spawnPoints = new Vector2[4];
        Vector2 respawnPoint;

        Character[] personnages;
        int[] tabScores = new int[4] { 0, 0, 0, 0 };
        World world;
        float Xwin = TurkeySmashGame.WindowSize.X;
        float Ywin = TurkeySmashGame.WindowSize.Y;

        decimal timer;

        public Level(World _world, string backgroundName, Character[] personnages, ContentManager content)
        {
            this.world = _world;
            this.personnages = personnages;

            background = new Sprite();
            background.Load(content, backgroundName);


            switch (backgroundName)
            {
                case "Jeu\\level1\\background1":
                    #region Level 1

                    respawnPoint = new Vector2(Xwin * 0.510f, Ywin * 0.444f);
                    spawnPoints[0] = new Vector2(Xwin * 0.331f,Ywin * 0.528f);
                    spawnPoints[1] = new Vector2(Xwin * 0.729f,Ywin * 0.444f);

                    //items = new PhysicsObject[3];
                    //itemsSprite = new Sprite[3];
                    bodylist = new StaticPhysicsObject[5];

                    #region Sprites Loading

                    Sprite caisseSprite = new Sprite();
                    caisseSprite.Load(TurkeySmashGame.content, "Jeu\\level1\\caisse");
                    //itemsSprite[0] = caisseSprite;
                    //itemsSprite[1] = caisseSprite;
                    //itemsSprite[2] = caisseSprite;

                    Sprite plateforme1Mid = new Sprite();
                    plateforme1Mid.Load(TurkeySmashGame.content, "Jeu\\level1\\plateforme1Mid");
                    Sprite plateforme1Left = new Sprite();
                    plateforme1Left.Load(TurkeySmashGame.content, "Jeu\\level1\\plateforme1Left");
                    //Sprite plateforme1Right = new Sprite();
                    //plateforme1Right.Load(TurkeySmashGame.content, "Jeu\\plateforme1Right");   /créé un bug CHELOU
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
                    //items[0] =  new RectPhysicsObject(world, new Vector2(Xwin * 0.489f, Ywin * 0.452f), 2, caisseSprite);
                    //items[1] =  new RectPhysicsObject(world, new Vector2(Xwin * 0.530f, Ywin * 0.452f), 2, caisseSprite);
                    //items[2] =  new RectPhysicsObject(world, new Vector2(Xwin * 0.509f, Ywin * 0.392f), 2, caisseSprite);
                    //items[0].body.Friction = 1f;
                    //items[1].body.Friction = 1f;
                    //items[2].body.Friction = 1f;

                    #endregion

                    #endregion
                    break;

                case "Jeu\\level2\\background2":
                    #region Level 2
                    
                    respawnPoint = new Vector2(Xwin * 0.427f, Ywin * 0.417f);
                    spawnPoints[0] = new Vector2(Xwin * 0.328f,Ywin * 0.602f);
                    spawnPoints[1] = new Vector2(Xwin * 0.620f, Ywin * 0.602f);
                    
                    items = new PhysicsObject[6];
                    itemsSprite = new Sprite[6];
                    bodylist = new StaticPhysicsObject[4];
                    
                    #region Sprites Loading

                    Sprite football = new Sprite();
                    football.Load(TurkeySmashGame.content, "Jeu\\level2\\football");
                    itemsSprite[0] = football;
                    Sprite blocSwag = new Sprite();
                    blocSwag.Load(TurkeySmashGame.content, "Jeu\\level2\\blocSwag");
                    itemsSprite[1] = blocSwag;
                    itemsSprite[2] = blocSwag;
                    itemsSprite[3] = blocSwag;
                    itemsSprite[4] = blocSwag;
                    itemsSprite[5] = blocSwag;

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
                    items[0] = new RoundPhysicsObject(world, new Vector2(Xwin * 0.270f, Ywin * 0.417f), 4.0f, 0.7f, football);
                    items[1] = new RectPhysicsObject(world, new Vector2(Xwin * 0.729f, Ywin * 0.648f), 3.0f, blocSwag);
                    items[2] = new RectPhysicsObject(world, new Vector2(Xwin * 0.729f, Ywin * 0.581f), 3.0f, blocSwag);
                    items[3] = new RectPhysicsObject(world, new Vector2(Xwin * 0.729f, Ywin * 0.516f), 3.0f, blocSwag);
                    items[4] = new RectPhysicsObject(world, new Vector2(Xwin * 0.729f, Ywin * 0.451f), 3.0f, blocSwag);
                    items[5] = new RectPhysicsObject(world, new Vector2(Xwin * 0.729f, Ywin * 0.386f), 3.0f, blocSwag);
                    #endregion

                    #endregion
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            timer += (decimal)gameTime.ElapsedGameTime.TotalMilliseconds;
            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                {
                    foreach (IA bolo in personnages.OfType<IA>())
                        bolo.UpdatePosition(personnages);

                    personnages[i].Update(gameTime);
                    partieTerminee(personnages[i]);

                    if (!personnages[i].Mort & outOfScreen(personnages[i].bodyPosition))
                        respawn(personnages[i]);
                    if (personnages[i].Mort)
                        personnages[i] = null;
                }
            }
        }

        bool outOfScreen(Vector2 position)
        {
            return (position.X < ConvertUnits.ToSimUnits(-100)
                   || position.X > ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X + 100)
                   || position.Y > ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y + 100)
                   || position.Y < ConvertUnits.ToSimUnits(-100));
        }

        void respawn(Character personnage)
        {
            FarseerBodyUserData userData = (FarseerBodyUserData)personnage.body.UserData;
            if (personnage.vie == 1)
                personnage.vie = 0;
            if (!personnage.Mort)
            {
                personnage.bodyPosition = ConvertUnits.ToSimUnits(respawnPoint);
                Scoring(personnage);
                if (OptionsCombat.TypePartieSelect != "temps")
                    personnage.vie--;
            }
            userData.pourcent = 0;
            personnage.body.ResetDynamics();
        }

        private void Scoring(Character personnage)
        {
            FarseerBodyUserData userData = (FarseerBodyUserData)personnage.body.UserData;
            if (userData.lastHit <= 0)
                personnage.score -= 2;
            else
                personnages[userData.lastHit - 1].score += 1;

            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                    tabScores[i] = personnages[i].score;
            }
            Console.WriteLine(tabScores[0] + " / " + tabScores[1] + " / " + tabScores[2] + " / " + tabScores[3]);
        }

        void partieTerminee(Character personnage)
        {
            if (OptionsCombat.TypePartieSelect == "temps")
            {
                if (timer >= OptionsCombat.TempsPartie * 1000 * 60)
                {
                    //Ecran de fin de partie
                    Console.WriteLine("Fin de Partie");
                }
            }
            if (OptionsCombat.TypePartieSelect == "vie")
            {
                if (personnages[0] == null ^ personnages[1] == null ^ personnages[2] == null ^ personnages[3] == null)
                {
                    //Ecran de fin de partie
                    Console.WriteLine("Fin de Partie");
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.DrawAsBackground(spriteBatch);

            /*if (items != null)
            {
                for (int i = 0; i < items.Length; i++)
                {
                    items[i].Draw(itemsSprite[i], spriteBatch);
                }
            }*/
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
    }
}
