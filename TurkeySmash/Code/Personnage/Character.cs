using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using System;

namespace TurkeySmash
{

    class Character : PhysicsObject
    {

        public int vie = 5;
        public int pourcent = 0;
        bool canJump;
        World world;

        public bool Mort { get { return vie < 1; } }

        public Character(World world, Vector2 position, float density, Sprite sprite)
            : base(world, position, density, sprite)
        {
            body.FixedRotation = true;
            body.Friction = 2f;
            this.world = world;
        }

        public override void Update(GameTime gameTime)
        {
            //Right & Left
            int forcePower = 6;
            Vector2 force = Vector2.Zero;
            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                force.X = -forcePower;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                force.X = forcePower;
            body.ApplyForce(force, body.Position);

            //Jump
            body.OnCollision += bodyOnCollision;
            if (canJump & Keyboard.GetState().IsKeyDown(Keys.Space))
            {
                body.ApplyForce(-Vector2.UnitY * 100);
                canJump = false;
            }

            //Attack
            if (Keyboard.GetState().IsKeyDown(Keys.A))
            {
                PhysicsObject hit = new PhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + sprite.Width/2 , ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(sprite.Width/2, sprite.Height/2));
                hit.body.OnCollision += hitOnColision;
                body.IgnoreCollisionWith(hit.body);
                world.Step(1 / 300f);
                world.RemoveBody(hit.body);
            }
        }

        public override void Draw(Sprite sprite, SpriteBatch spriteBatch)
        {

        }

        private bool bodyOnCollision(Fixture fixA, Fixture fixB, Contact contact)
        {
            canJump = true;
            return true;
        }

        public bool hitOnColision(Fixture fixA, Fixture fixB, Contact contact)
        {
            fixB.Body.ApplyLinearImpulse(new Vector2(1f, -.5f));
            return true;
        }
    }
}