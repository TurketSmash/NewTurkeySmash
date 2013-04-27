using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics.Contacts;

namespace TurkeySmash
{

    class Character : PhysicsObject
    {

        int vie = 5;
        bool canJump = false;

        public bool Mort { get { return vie < 1; } }

        public Character(World world, Vector2 position, float density, Sprite sprite)
            : base(world, position, density, sprite)
        {
            body.FixedRotation = true;
            body.Friction = 2f;
        }

        public override void Update(GameTime gameTime)
        {
            int forcePower = 6;
            Vector2 force = Vector2.Zero;

            if (Keyboard.GetState().IsKeyDown(Keys.Left))
                force.X = -forcePower;
            if (Keyboard.GetState().IsKeyDown(Keys.Right))
                force.X = forcePower;

            body.OnCollision += bodyOnCollision;
            if (canJump)
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Space))
                {
                    body.ApplyForce(-Vector2.UnitY * 2 * forcePower);
                    canJump = false;
                }
            }

            body.ApplyForce(force, body.Position);
        }

        private bool bodyOnCollision (Fixture fixA,Fixture fixB, Contact contact)
        {
            canJump = true;
            return true;
        }
    }
}