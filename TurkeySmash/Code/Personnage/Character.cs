﻿using Microsoft.Xna.Framework;
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
        float oldDrop;
        float newDrop;

        public bool Mort { get { return vie < 1; } }

        public Character(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, definition)
        {
            this.playerindex = playerindex;
            input = new Input(playerindex);
            body.FixedRotation = true;
            body.Friction = 0.1f;
            body.UserData = this.pourcent;
        }

        public override void Update(GameTime gameTime)
        {
            newDrop = bodyPosition.Y;

            if (FinishedAnimation && canJump)
            {
                Reset(new Point(0, 1));
                TimeBetweenFrame = 100;
            }

            #region Controls

                //Right & Left
                int forcePower = 3;
                Vector2 force = Vector2.Zero;
                if (input.Left(playerindex))
                {
                    force.X = -forcePower;
                    isMovingRight = false;
                    effects = SpriteEffects.FlipHorizontally;
                    if (canJump)
                    {
                        definition.Loop = false;
                        CurrentFrame.Y = 0;
                    }
                }
                else if (input.Right(playerindex))
                {
                    force.X = forcePower;
                    isMovingRight = true;
                    effects = SpriteEffects.None;
                    if (canJump)
                    {
                        definition.Loop = false;
                        CurrentFrame.Y = 0;
                    }
                }
                body.ApplyForce(force, body.Position);

                //Jump
                body.OnCollision += bodyOnCollision;
                if (input.Jump(playerindex) & canJump)
                {
                    body.ApplyForce(-Vector2.UnitY * 100);
                    canJump = false;
                    definition.Loop = false;
                    FinishedAnimation = false;
                    CurrentFrame.Y = 2;
                }

                //Attack
                if (input.ActionReleased(playerindex))
                    input.canAction = true;

                if (input.Action(playerindex) & input.canAction)
                {
                    RectPhysicsObject hit;
                    if (isMovingRight)
                    {
                        hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + 18 + bodySize.X / 2, ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    }
                    else
                    {
                        hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) - 18 - bodySize.X / 2, ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    }
                    hit.body.IsSensor = true;
                    hit.body.OnCollision += hitOnColision;
                    world.Step(1 / 3000f);
                    world.RemoveBody(hit.body);

                    Reset(new Point());
                    definition.Loop = false;
                    FinishedAnimation = false;
                    TimeBetweenFrame = 50;
                    CurrentFrame.Y = 3;

                    input.canAction = false;
                }
            
            #endregion

            if (newDrop > oldDrop + 0.1)
            {
                CurrentFrame = new Point(4, 2);
                definition.Loop = false;
                FinishedAnimation = true;
            }

            oldDrop = newDrop;
            pourcent = (int)body.UserData;

            base.Update(gameTime);
        }


        private bool bodyOnCollision(Fixture fixA, Fixture fixB, Contact contact)
        {
            canJump = true;
            return true;
        }

        public bool hitOnColision(Fixture fixA, Fixture fixB, Contact contact)
        {
            float forceItem = 2.0f;
            int pourcentB;
            if (fixB.Body.UserData != null)
            {
                forceItem = 1.0f;
                fixB.Body.UserData = (int)fixB.Body.UserData + 7;
                pourcentB = (int)fixB.Body.UserData;
            }
            else
                pourcentB = 0;
            if (isMovingRight)
                fixB.Body.ApplyLinearImpulse(new Vector2(1.8f, -.8f) * (1 + (pourcentB / 50)) * forceItem);
            else
                fixB.Body.ApplyLinearImpulse(new Vector2(-1.8f, -.8f) * (1 + (pourcentB / 50)) * forceItem);

            return true;
        }

    }
}