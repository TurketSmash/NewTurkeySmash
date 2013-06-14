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
        protected enum Direction
        {
            Up,
            Down,
            Left,
            Right,
            Nodirection
        }

        public int vie = OptionsCombat.NombreVies;
        public int pourcent = 0;
        public PlayerIndex playerindex;
        public int score = 0;

        protected bool canJump;
        protected bool inAction = false;

        public bool lookingRight = true;
        protected bool jump = false;
        protected bool isMoving = false;
        protected Direction direction;
        
        int forcePower = 3; // force appliquée au personnage lors des déplacements droite/gauche
        RectPhysicsObject hit;
        bool canHit = true;
        int frameHit = 0;
        float oldDrop; // variable pour calcul la chute du personnage
        float newDrop;

        ParticleEngine[] particles = new ParticleEngine[4];

        int oldPourcent;
        int i = 0; // compteur nombre de frame filtre rouge
        int x = 0;
        int y = 0;

        int allongeCoup = 18;
        int forceJump = 70;
        float forceItem;
        float maxForceCharged = 1.0f;
        float ForceItem { get { return forceItem; } set { forceItem = forceItem > maxForceCharged ? maxForceCharged : value; } }
        public bool Mort { get { return vie < 1; } }

        public Character(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, definition)
        {
            this.playerindex = playerindex;
            body.FixedRotation = true;
            body.Friction = 0.1f;
            FarseerBodyUserData userdata = (FarseerBodyUserData)body.UserData;
            userdata.associatedName = definition.AssetName;
            userdata.pourcent = 0;
            userdata.lastHit = 0;
        }

        public override void Update(GameTime gameTime)
        {
            newDrop = bodyPosition.Y;
            FarseerBodyUserData userdata = (FarseerBodyUserData)body.UserData;

            if (FinishedAnimation)
            {
                inAction = false;
                Reset(new Point(0, 1));
                TimeBetweenFrame = 100;
                canHit = true;
            }

            if (newDrop > (oldDrop + 0.1f))
            {
                CurrentFrame = new Point(4, 2);
                definition.Loop = false;
                FinishedAnimation = true;
            }
            oldDrop = newDrop;

            #region Flip Droite/Gauche

            if (!inAction)
            {
                if (lookingRight)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            }

            #endregion

            #region Filtre couleur

            if (pourcent > oldPourcent)
            {
                color = Color.Red;
                i = 5;
            }
            else if (i > 0)
            {
                color = Color.Red;
                i--;
            }
            else
                color = Color.White;

            #endregion

            if (inAction & CurrentFrame.X == frameHit & canHit)
            {
                hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + (allongeCoup + bodySize.X / 2) * x,
                    ConvertUnits.ToDisplayUnits(body.Position.Y) + (allongeCoup + bodySize.Y) * y), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                hit.body.IsSensor = true;
                hit.body.OnCollision += hitOnColision;
                world.Step(1 / 3000f);
                world.RemoveBody(hit.body);
                canHit = false;
            }

            body.OnCollision += bodyOnCollision;
            oldPourcent = pourcent;
            pourcent = userdata.pourcent;
            direction = Direction.Nodirection;

            base.Update(gameTime);
        }

        private bool bodyOnCollision(Fixture fixA, Fixture fixB, Contact contact)
        {
            canJump = true;
            return true;
        }

        public bool hitOnColision(Fixture fixA, Fixture fixB, Contact contact)
        {
            FarseerBodyUserData fixBuserdata = (FarseerBodyUserData)fixB.Body.UserData;
            int pourcentB;
            if (fixB.Body.UserData != null)
            {
                fixBuserdata.lastHit = Convert.PlayerIndex2Int(playerindex);
                fixBuserdata.pourcent = fixBuserdata.pourcent + 7;
                pourcentB = fixBuserdata.pourcent;
            }
            else
            {
                forceItem = 1.5f;
                pourcentB = 0;
            }
            if (lookingRight)
                fixB.Body.ApplyLinearImpulse(new Vector2(1.8f, -.8f) * (1 + (pourcentB / 50)) * forceItem);
            else
                fixB.Body.ApplyLinearImpulse(new Vector2(-1.8f, -.8f) * (1 + (pourcentB / 50)) * forceItem);

            return true;
        }

        protected void Attack()
        {
            if (!inAction)
            {
                x = 0;
                if (direction == Direction.Up)
                {
                    TimeBetweenFrame = 75;
                    Reset(new Point(0, 4));
                    allongeCoup = 20;
                    forceItem = 0.3f;
                    frameHit = 2;
                }
                else if (direction == Direction.Down && !canJump)
                {
                    TimeBetweenFrame = 100;
                    Reset(new Point(0, 6));
                    allongeCoup = 15;
                    forceItem = 0.3f;
                    frameHit = 2;
                }
                else
                {
                    TimeBetweenFrame = 50;
                    if (canJump)
                    {
                        Reset(new Point(0, 3));
                        allongeCoup = 15;
                        forceItem = 0.3f;
                        frameHit = 1;
                    }
                    else
                    {
                        Reset(new Point(0, 5));
                        allongeCoup = 25;
                        forceItem = 0.3f;
                        frameHit = 2;
                    }
                    x = lookingRight ? 1 : -1;
                }
                definition.Loop = false;
                inAction = true;

                y = direction == Direction.Down ? 1 : direction == Direction.Up ? -1 : 0;
            }
        }

        protected void Jump()
        {
            if (!inAction & canJump)
            {
                body.ApplyForce(-Vector2.UnitY * forceJump);
                canJump = false;
                Reset(new Point(0, 2));
                definition.Loop = false;
            }
        }
        protected void Moving()
        {
            if (!inAction)
            {
                Vector2 force = Vector2.Zero;
                force.X = lookingRight ? forcePower : -forcePower;
                if (canJump) // Running
                {
                    CurrentFrame.Y = 0;
                    definition.Loop = false;
                }
                body.ApplyForce(force, body.Position);
            }
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }
    }
}