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

        Vector2 respownPoint = new Vector2(ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X / 2), ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y / 2));
        Vector2[] spawnPoints = new Vector2[4];
        Character[] personnages;
        World world;
        float Xwin = TurkeySmashGame.WindowSize.X;
        float Ywin = TurkeySmashGame.WindowSize.Y;

        public Level(World _world, string backgroundName, Character[] personnages, ContentManager content)
        {
            //spawnPoints[0] = new Vector2(1200, 300);
            //spawnPoints[1] = new Vector2(-1200, 300);

            this.world = _world;
            this.personnages = personnages;

            background = new Sprite();
            background.Load(content, backgroundName);


            switch (backgroundName)
            {
                case "Jeu\\background1":
                    #region bodies
                    
                    Sprite plateforme1Mid = new Sprite();
                    plateforme1Mid.Load(TurkeySmashGame.content, "Jeu\\plateforme1Mid");
                    Sprite plateforme1Left = new Sprite();
                    plateforme1Left.Load(TurkeySmashGame.content, "Jeu\\plateforme1Left");
                    Sprite plateforme1Right = new Sprite();
                    plateforme1Right.Load(TurkeySmashGame.content, "Jeu\\plateforme1Right");
                    Sprite plateforme2 = new Sprite();
                    plateforme2.Load(TurkeySmashGame.content, "Jeu\\plateforme2");
                    
                    bodylist = new StaticPhysicsObject[6];

                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(Xwin*0.111f, Ywin*0.611f), 1, plateforme2);
                    bodylist[1] =  new StaticPhysicsObject(world, new Vector2(Xwin*0.512f, Ywin*0.515f), 1, plateforme2); //3 plateformes volantes
                    bodylist[2] =  new StaticPhysicsObject(world, new Vector2(Xwin*0.735f, Ywin*0.520f), 1, plateforme2);

                    bodylist[3] =  new StaticPhysicsObject(world, new Vector2((Xwin/2) - (plateforme1Mid.Width/2) + (plateforme1Left.Width/2),(2*Ywin/3)-(plateforme1Mid.Height /2) - (plateforme1Left.Height/2)+1), 1, plateforme1Left); //partie gauche de la plateforme centrale
                    bodylist[4] =  new StaticPhysicsObject(world, new Vector2(Xwin/2, 2*Ywin/3), 1, plateforme1Mid); //partie central de la plateforme centrale
                    //bodylist[5] = new StaticPhysicsObject(world, new Vector2((Xwin/2) + (plateforme1Mid.Width/2) - (plateforme1Right.Width/2) -1,(2*Ywin/3)-(plateforme1Mid.Height /2) - (plateforme1Right.Height/2)+1), 1, plateforme1Right); //partie droite de la plateforme centrale

                    #endregion


                    break;

                case "Jeu\\level2":
                    
                    break;
            }
        }

        public void Update(GameTime gameTime)
        {
            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                {
                    personnages[i].Update(gameTime);

                    if (outOfScreen(personnages[i].bodyPosition))
                    {
                        respawn(personnages[i]);
                    }
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
            if (!personnage.Mort)
            {
                personnage.bodyPosition = respownPoint;
                personnage.vie--;
            }
            personnage.body.UserData = 0;
            personnage.body.ResetDynamics();
        }

        void partieTerminee(Character personnage)
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.DrawAsBackground(spriteBatch);
            
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
