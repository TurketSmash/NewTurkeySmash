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
        Rectangle cadreDecor = new Rectangle(-3500, 2500, 7000, -5000);
        Sprite background;

        StaticPhysicsObject[] platesFormes = new StaticPhysicsObject[2];
        Sprite spritePlateFormes;

        Point[] spawnPoints = new Point[4];
        Point positionRespawn = new Point(0, 1100);
        Character[] personnages;
        World world;

        public Level(World _world, string backgroundName, string spritePlateFormesNames, Character[] personnages, ContentManager content)
        {
            this.world = _world;
            spawnPoints[0] = new Point(1200, 300);
            spawnPoints[1] = new Point(-1200, 300);
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
            foreach (PhysicsObject objet in personnages)
            {
                if (objet != null)
                    objet.Update(gameTime);
            }
        }

        public void respawn()
        {

        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            foreach (StaticPhysicsObject elements in platesFormes)
            {
                elements.Draw(elements.sprite, spriteBatch);
            }
            foreach (Character elements in personnages)
            {
                if (elements != null)
                    elements.Draw(elements.sprite, spriteBatch);
            }
        }
    }
}
