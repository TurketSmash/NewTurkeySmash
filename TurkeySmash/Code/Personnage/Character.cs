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

    class Character : AnimatedSprite
    {

        public int vie = 5;
        public int pourcent = 0;
        bool canJump;
        PlayerIndex playerindex;
        Input input;
        bool isMovingRight = true;
        World world;

        public bool Mort { get { return vie < 1; } }

        public Character(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, definition)
        {
            this.playerindex = playerindex;
            input = new Input(playerindex);
            body.FixedRotation = true;
            body.Friction = 1f;
            this.world = world;
            body.UserData = this.pourcent;
        }

        public override void Update(GameTime gameTime)
        {
            if (playerindex == PlayerIndex.One)
            #region P1
            {
                //Right & Left
                int forcePower = 6;
                Vector2 force = Vector2.Zero;
                if (input.Left(playerindex))
                {
                    force.X = -forcePower;
                    isMovingRight = false;
                }

                if (input.Right(playerindex))
                {
                    force.X = forcePower;
                    isMovingRight = true;
                }
                body.ApplyForce(force, body.Position);

                //Jump
                body.OnCollision += bodyOnCollision;
                if (input.Jump(playerindex) & canJump)
                {
                    body.ApplyForce(-Vector2.UnitY * 100);
                    canJump = false;
                }

                //Attack
                if (input.Action(playerindex))
                {
                    PhysicsObject hit;
                    if (isMovingRight)
                    {
                        hit = new PhysicsObject(world, new Vector2(body.Position.X + 15 + bodySize.X / 2, body.Position.Y), 1, new Vector2((bodySize.X / 2), bodySize.Y / 2));
                    }
                    else
                    {
                        hit = new PhysicsObject(world, new Vector2(body.Position.X - 15 - bodySize.X / 2, body.Position.Y), 1, new Vector2((bodySize.X / 2), bodySize.Y / 2));
                    }
                    hit.body.BodyType = BodyType.Kinematic;
                    hit.body.OnCollision += hitOnColision;
                    world.Step(1 / 300f);
                    world.RemoveBody(hit.body);
                }
            }
            #endregion

            if (playerindex == PlayerIndex.Two)
            #region P2
            {
                //Right & Left
                int forcePower = 6;
                Vector2 force = Vector2.Zero;
                if (input.Left(playerindex))
                {
                    force.X = -forcePower;
                    isMovingRight = false;
                }

                if (input.Right(playerindex))
                {
                    force.X = forcePower;
                    isMovingRight = true;
                }
                body.ApplyForce(force, body.Position);

                //Jump
                body.OnCollision += bodyOnCollision;
                if (input.Jump(playerindex) & canJump)
                {
                    body.ApplyForce(-Vector2.UnitY * 100);
                    canJump = false;
                }

                //Attack
                if (input.Action(playerindex))
                {
                    PhysicsObject hit;
                    if (isMovingRight)
                    {
                        hit = new PhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + 15 + bodySize.X / 2, ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2((bodySize.X / 2), bodySize.Y / 2));
                    }
                    else
                    {
                        hit = new PhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) - 15 - bodySize.X / 2, ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2((bodySize.X / 2), bodySize.Y / 2));
                    }
                    hit.body.BodyType = BodyType.Kinematic;
                    hit.body.OnCollision += hitOnColision;
                    world.Step(1 / 300f);
                    world.RemoveBody(hit.body);
                }
            }
                #endregion 
            base.Update(gameTime);
        }


        private bool bodyOnCollision(Fixture fixA, Fixture fixB, Contact contact)
        {
            canJump = true;
            return true;
        }

        public bool hitOnColision(Fixture fixA, Fixture fixB, Contact contact)
        {
            int pourcentB;
            if (fixB.Body.UserData != null)
                pourcentB = (int)fixB.Body.UserData;
            else
                pourcentB = 0;
            Console.WriteLine(playerindex +" : " + pourcentB);

            if (isMovingRight)
                fixB.Body.ApplyLinearImpulse(new Vector2(1f, -.5f) * (1 + (pourcentB / 100)));
            else
                fixB.Body.ApplyLinearImpulse(new Vector2(-1f, -.5f) * (1 + (pourcentB / 100)));

            if (fixB.Body.UserData != null)
                fixB.Body.UserData = (int)fixB.Body.UserData + 7;
            return true;
        }

    }
}