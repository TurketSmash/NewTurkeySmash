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
        protected enum Direction
        {
            Up,
            Down,
            Left,
            Right
        }

        public int vie = OptionsCombat.NombreVies;
        public int pourcent = 0;
        public PlayerIndex playerindex;

        bool canJump;
        bool inAction = false;

        protected bool lookingRight = true;
        protected bool jump = false;
        protected bool action = false;
        protected bool isMoving = false;
        protected Direction direction;
        
        int forcePower = 3; // force appliquée au personnage lors des déplacements droite/gauche

        float oldDrop; // variable pour calcul la chute du personnage
        float newDrop;

        int oldPourcent;
        int i = 0; // compteur nombre de frame filtre rouge

        float j = 0f;

        float forceItem = 1.0f;
        float maxForceCharged = 2.0f;
        float ForceItem { get { return forceItem; } set { forceItem = forceItem > maxForceCharged ? maxForceCharged : forceItem; } }
        public bool Mort { get { return vie < 1; } }

        public Character(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, definition)
        {
            this.playerindex = playerindex;
            body.FixedRotation = true;
            body.Friction = 0.1f;
            body.UserData = this.pourcent;
        }

        public override void Update(GameTime gameTime)
        {
            newDrop = bodyPosition.Y;

            if (FinishedAnimation && canJump)
            {
                inAction = false;
                Reset(new Point(0, 1));
                TimeBetweenFrame = 100;
            }

            #region Controls

            Vector2 force = Vector2.Zero;
            body.OnCollision += bodyOnCollision;

            if (!inAction)
            {
                if (lookingRight)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;

                //Attack
                if (action)
                {
                    RectPhysicsObject hit;
                    if (lookingRight)
                        hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + 18 + bodySize.X / 2, ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    else
                        hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) - 18 - bodySize.X / 2, ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    hit.body.IsSensor = true;
                    hit.body.OnCollision += hitOnColision;
                    world.Step(1 / 3000f);
                    world.RemoveBody(hit.body);
                    inAction = true;
                    ForceItem = 1.0f;
                    Reset(new Point());
                    definition.Loop = false;
                    FinishedAnimation = false;
                    TimeBetweenFrame = 50;
                    CurrentFrame.Y = 3;
                }

                if (isMoving)
                {
                    force.X = lookingRight ? forcePower : -forcePower;
                    if (canJump)
                    {
                        definition.Loop = false;
                        CurrentFrame.Y = 0;
                    }
                }
                body.ApplyForce(force, body.Position);

                //Jump
                if (jump & canJump)
                {
                    body.ApplyForce(-Vector2.UnitY * 80);
                    canJump = false;
                    definition.Loop = false;
                    FinishedAnimation = false;
                    CurrentFrame.Y = 2;
                }
            }

            #endregion

            if (newDrop > (oldDrop + 0.1f))
            {
                CurrentFrame = new Point(4, 2);
                definition.Loop = false;
                FinishedAnimation = true;
            }
            oldDrop = newDrop;

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

            oldPourcent = pourcent;
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
            int pourcentB;
            if (fixB.Body.UserData != null)
            {
                fixB.Body.UserData = (int)fixB.Body.UserData + 7;
                pourcentB = (int)fixB.Body.UserData;
            }
            else
                pourcentB = 0;
            if (lookingRight)
                fixB.Body.ApplyLinearImpulse(new Vector2(1.8f, -.8f) * (1 + (pourcentB / 50)) * forceItem);
            else
                fixB.Body.ApplyLinearImpulse(new Vector2(-1.8f, -.8f) * (1 + (pourcentB / 50)) * forceItem);

            return true;
        }
    }
}