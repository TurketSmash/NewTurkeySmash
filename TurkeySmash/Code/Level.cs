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

        StaticPhysicsObject[] bodylist = new StaticPhysicsObject[6];
        Sprite spritePlateFormes;

        Vector2 respownPoint = new Vector2(ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X / 2), ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y / 2));
        Vector2[] spawnPoints = new Vector2[4];
        Character[] personnages;
        World world;

        public Level(World _world, string backgroundName, Character[] personnages, ContentManager content)
        {
            this.world = _world;
            spawnPoints[0] = new Vector2(1200, 300);
            spawnPoints[1] = new Vector2(-1200, 300);
            this.personnages = personnages;

            background = new Sprite();
            background.Load(content, backgroundName);

            //spritePlateFormes = new Sprite();
            //spritePlateFormes.Load(content, spritePlateFormesNames);

            switch (backgroundName)
            {
                case "Jeu\\level1":
                    bodylist[0] = new StaticPhysicsObject(world, new Vector2(235, 665), 10, new Vector2(240, 70));
                    bodylist[1] = new StaticPhysicsObject(world, new Vector2(1117, 335), 10, new Vector2(240, 70)); //3 plateformes volantes
                    bodylist[2] = new StaticPhysicsObject(world, new Vector2(1458, 510), 10, new Vector2(240, 70));
                    bodylist[3] = new StaticPhysicsObject(world, new Vector2(550, 720), 10, new Vector2(68, 172)); //partie gauche de la plateforme centrale
                    bodylist[4] = new StaticPhysicsObject(world, new Vector2(882, 754), 10, new Vector2(580, 102)); //partie central de la plateforme centrale
                    //bodylist[5] = new StaticPhysicsObject(world, new Vector2(1204, 738), 10, new Vector2(68, 139)); //partie droite de la plateforme centrale
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
            //foreach (StaticPhysicsObject elements in bodylist)
            //{
            //    elements.Draw(elements.sprite, spriteBatch);
            //}
            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                    personnages[i].Draw(spriteBatch);
            }
        }
    }
}
