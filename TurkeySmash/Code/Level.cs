using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Microsoft.Xna.Framework.Content;

namespace TurkeySmash
{
    class Level
    {
        Sprite background;

        StaticPhysicsObject[] platesFormes = new StaticPhysicsObject[2];
        Sprite spritePlateFormes;

        Vector2 respownPoint = new Vector2(ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X / 2), ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y / 2));
        Vector2[] spawnPoints = new Vector2[4];
        Character[] personnages;
        World world;

        public Level(World _world, string backgroundName, string spritePlateFormesNames, Character[] personnages, ContentManager content)
        {
            this.world = _world;
            spawnPoints[0] = new Vector2(1200, 300);
            spawnPoints[1] = new Vector2(-1200, 300);
            this.personnages = personnages;

            background = new Sprite();
            background.Load(content, backgroundName);

            spritePlateFormes = new Sprite();
            spritePlateFormes.Load(content, spritePlateFormesNames);

            switch (backgroundName)
            {
                case "Jeu\\background":
                    platesFormes[0] = new StaticPhysicsObject(world, new Vector2(300, 400), 10, spritePlateFormes);
                    platesFormes[1] = new StaticPhysicsObject(world, new Vector2(900, 700), 10, spritePlateFormes);
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

                    if (personnages[i].bodyPosition.X < ConvertUnits.ToSimUnits(-100)
                        || personnages[i].bodyPosition.X > ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.X + 100)
                        || personnages[i].bodyPosition.Y > ConvertUnits.ToSimUnits(TurkeySmashGame.WindowSize.Y + 100)
                        || personnages[i].bodyPosition.Y < ConvertUnits.ToSimUnits(-100))

                        personnages[i].bodyPosition = respownPoint;
                }

            }
        }

        public void respawn()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.DrawAsBackground(spriteBatch);
            foreach (StaticPhysicsObject elements in platesFormes)
            {
                elements.Draw(elements.sprite, spriteBatch);
            }
            for (int i = 0; i < personnages.Length; i++)
            {
                if (personnages[i] != null)
                    personnages[i].Draw(personnages[i].sprite, spriteBatch);
            }
        }
    }
}
