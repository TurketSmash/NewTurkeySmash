using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using FarseerPhysics.Dynamics;
using Libraries;
using Microsoft.Xna.Framework.Content;

namespace TurkeySmash.Code.Main
{
    public class Level
    {
        private Rectangle cadreDecor = new Rectangle(-3500, 2500, 7000, -5000);
        public Sprite background { get; set; }
        public Sprite spritePlateFormes { get; set; }
        private Point[] spawnPoints = new Point[4];
        private Point positionRespawn = new Point(0, 1100);
        StaticPhysicsObject[] platesFormes = new StaticPhysicsObject[2];
        World world;


        public Level(World _world, string backGroundName, ContentManager content)
        {
            this.world = _world;
            spawnPoints[0] = new Point(1200, 300);
            spawnPoints[1] = new Point(-1200, 300);

            background = new Sprite();
            background.Load(content, backGroundName);

            spritePlateFormes = new Sprite();
            spritePlateFormes.Load(content, "Jeu\\ground");

            switch (backGroundName)
            {
                case "Jeu\\background":
                    platesFormes[0] = new StaticPhysicsObject(world, new Vector2(300, 400), 10, spritePlateFormes);
                    platesFormes[1] = new StaticPhysicsObject(world, new Vector2(900, 700), 10, spritePlateFormes);
                    break;
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            background.Draw(spriteBatch);
            foreach (StaticPhysicsObject elements in platesFormes)
            {
                elements.Draw(elements.sprite, spriteBatch);
            }
        }
    }
}
