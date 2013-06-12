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
            Right,
            Nodirection
        }

        public int vie = OptionsCombat.NombreVies;
        public int pourcent = 0;
        public PlayerIndex playerindex;

        protected bool canJump;
        bool inAction = false;

        protected bool lookingRight = true;
        protected bool jump = false;
        protected bool isMoving = false;
        protected Direction direction;
        
        int forcePower = 3; // force appliquée au personnage lors des déplacements droite/gauche

        float oldDrop; // variable pour calcul la chute du personnage
        float newDrop;

        int oldPourcent;
        int i = 0; // compteur nombre de frame filtre rouge

        float j = 0f;

        int forceJump = 80;
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

            if (FinishedAnimation)
            {
                if (canJump)
                    Reset(new Point(0, 1));
                inAction = false;
                TimeBetweenFrame = 100;
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

            body.OnCollision += bodyOnCollision;
            oldPourcent = pourcent;
            pourcent = (int)body.UserData;
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

        protected void Attack()
        {
            if (!inAction)
            {
                Reset(new Point());
                definition.Loop = false;
                FinishedAnimation = false;
                RectPhysicsObject hit;

                if (direction == Direction.Up)
                {
                    hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + 18 + bodySize.X / 2,
                        ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    TimeBetweenFrame = 75;
                    CurrentFrame.Y = 4;
                }
                else if (direction == Direction.Down && !canJump)
                {
                    hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + 18 + bodySize.X / 2,
                        ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    TimeBetweenFrame = 100;
                    CurrentFrame.Y = 6;
                }
                else
                {
                    int x = lookingRight ? 1 : -1;
                    hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + 18 * x + x * bodySize.X / 2,
                        ConvertUnits.ToDisplayUnits(body.Position.Y)), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                    
                    TimeBetweenFrame = 50;
                    if (canJump)
                        CurrentFrame.Y = 3;
                    else
                        CurrentFrame.Y = 5;
                }

                hit.body.IsSensor = true;
                hit.body.OnCollision += hitOnColision;
                world.Step(1 / 3000f);
                world.RemoveBody(hit.body);
                inAction = true;
            }
        }
        protected void Jump()
        {
            if (canJump)
            {
                body.ApplyForce(-Vector2.UnitY * forceJump);
                canJump = false;
                definition.Loop = false;
                FinishedAnimation = false;
                CurrentFrame.Y = 2;
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
                    definition.Loop = false;
                    CurrentFrame.Y = 0;
                }
                body.ApplyForce(force, body.Position);
            }
        }
    }
}