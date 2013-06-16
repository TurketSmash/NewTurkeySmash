﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using System.Collections.Generic;

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

        protected bool canJump = false;
        protected bool inAction = false;
        public bool lookingRight = true;
        protected bool jump = false;
        protected bool isMoving = false;
        protected Direction direction;
        
        int forcePower = 3; // force appliquée au personnage lors des déplacements droite/gauche
        bool canHit = true;
        bool isHit = false;
        bool isProtecting = false;
        //bool isCharging = false;
        //int chargedAttack = 1;
        int frameHit = 0;
        Vector2 oldPosition; // variable pour calcul la chute du personnage
        Vector2 newPosition;

        ParticleEngine particles;
        List<Texture2D> textures = new List<Texture2D>();

        int oldPourcent;
        int i = 0; // compteur nombre de frame filtre rouge
        int compteurRoulade = 0;
        int x = 0;
        int y = 0;

        int allongeCoup = 18;
        int forceJump = 70;
        float forceItem = 0.3f;
        float maxForceCharged = 1.0f;
        float ForceItem { get { return forceItem; } set { forceItem = forceItem > maxForceCharged ? maxForceCharged : value; } }
        public bool Mort { get { return vie < 1; } }

        #region Constructeur

        public Character(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, definition)
        {
            this.playerindex = playerindex;
            body.FixedRotation = true;
            body.Friction = 0.1f;
            FarseerBodyUserData userData = new FarseerBodyUserData
            {
                AssociatedName = definition.AssetName,
                IsCharacter = true,
                Pourcent = 0,
                LastHit = 0
            };
            body.UserData = userData;
            textures.Add(TurkeySmashGame.content.Load<Texture2D>("Jeu\\particules\\star"));
            textures.Add(TurkeySmashGame.content.Load<Texture2D>("Jeu\\particules\\diamond"));
        }

        #endregion 

        public override void Update(GameTime gameTime)
        {
            newPosition = bodyPosition;
            FarseerBodyUserData userdata = (FarseerBodyUserData)body.UserData;
            userdata.Protecting = isProtecting;

            if (particles != null)
                particles.Update(gameTime, ConvertUnits.ToDisplayUnits(bodyPosition));

            #region Hit

            if (inAction & CurrentFrame.X == frameHit & canHit)
            {
                RectPhysicsObject hit = new RectPhysicsObject(world, new Vector2(ConvertUnits.ToDisplayUnits(body.Position.X) + (allongeCoup + bodySize.X / 2) * x,
                    ConvertUnits.ToDisplayUnits(body.Position.Y) + (allongeCoup + bodySize.Y) * y), 1, new Vector2(bodySize.X / 2, bodySize.Y / 2));
                hit.body.IsSensor = true;
                hit.body.OnCollision += hitOnColision;
                world.Step(1 / 3000f);
                world.RemoveBody(hit.body);
                canHit = false;
            }

            #endregion

            if (pourcent > oldPourcent)
                isHit = true;

            #region compteurRoulage

            if (!inAction)
                compteurRoulade += gameTime.ElapsedGameTime.Milliseconds;
            else
                compteurRoulade = 0;

            #endregion

            #region Reset Anim

            if (FinishedAnimation)
            {
                inAction = false;
                Reset(new Point(0, 1));
                TimeBetweenFrame = 100;
                canHit = true;
                isHit = false;
            }

            #endregion

            #region Chute

            if (newPosition.Y > (oldPosition.Y + 0.1f) & !inAction)
            {
                CurrentFrame = new Point(4, 2);
                definition.Loop = false;
                FinishedAnimation = true;
            }

            #endregion

            #region Flip Droite/Gauche

            if (!inAction)
            {
                if (lookingRight)
                    effects = SpriteEffects.None;
                else
                    effects = SpriteEffects.FlipHorizontally;
            }

            #endregion

            #region Filtre couleur + Anim coup reçu

            if (isHit)
            {
                color = Color.Red;
                i = 5;
                if (newPosition.X > (oldPosition.X + 0.1f) || newPosition.X < (oldPosition.X - 0.1f))
                {
                    if (!inAction)
                    {
                        Reset(new Point(0, 8));
                        TimeBetweenFrame = 50;
                        inAction = true;
                    }
                }
                else
                {
                    CurrentFrame = new Point(0, 10);
                    FinishedAnimation = true;
                }
                canHit = false;
                definition.Loop = false;
            }
            else if (i > 0)
            {
                color = Color.Red;
                i--;
                CurrentFrame = new Point(0, 10);
                definition.Loop = false;
                FinishedAnimation = true;
                canHit = false;
            }
            else
                color = Color.White;

            #endregion

            #region Charging

            /* if (isCharging)
            {
                inAction = true;
                CurrentFrame.X = 0;
                definition.Loop = false;
                FinishedAnimation = true;
            }
            else
            {
                if (true)
                {
                    CurrentFrame.Y = chargedAttack;
                    FinishedAnimation = false;
                    definition.Loop = false;
                    inAction = true;
                }
            } */

            #endregion

            body.OnCollision += bodyOnCollision;
            oldPourcent = pourcent;
            direction = Direction.Nodirection;
            pourcent = userdata.Pourcent;
            oldPosition = newPosition;
            isProtecting = false;
            //isCharging = false;

            base.Update(gameTime);
        }

        private bool bodyOnCollision(Fixture fixA, Fixture fixB, Contact contact)
        {
            canJump = true;
            return true;
        }

        public bool hitOnColision(Fixture fixA, Fixture fixB, Contact contact)
        {
            FarseerBodyUserData dataB = (FarseerBodyUserData)fixB.Body.UserData;
            FarseerBodyUserData dataA = (FarseerBodyUserData)body.UserData;
            int pourcentB = 0;

            if (fixB.Body.UserData != null)
            {
                if (dataB.IsBonus)
                {
                    if (dataB.BonusType == "vie")
                        vie++;
                    if (dataB.BonusType == "pourcent")
                    {
                        dataA.Pourcent -= 7 * 3;
                        if (dataA.Pourcent < 0)
                            dataA.Pourcent = 0;
                    }
                    dataB.IsUsed = true;
                }
                if (!dataB.Protecting)
                {
                    dataB.LastHit = Convert.PlayerIndex2Int(playerindex);
                    dataB.Pourcent = dataB.Pourcent + 7;
                    pourcentB = dataB.Pourcent;
                    fixB.Body.ApplyLinearImpulse(new Vector2(lookingRight ? 1 : -1, 2 * y - 0.5f) * (1 + (pourcentB / 50)) * forceItem);
                    particles = new ParticleEngine(textures, ConvertUnits.ToDisplayUnits(new Vector2(fixB.Body.Position.X, fixB.Body.Position.Y - 0.2f)), new Vector2(0,0), Color.White, 4, 500, 1.2f);
                }
            }
            else
            {
                fixB.Body.ApplyLinearImpulse(new Vector2(lookingRight ? 1 : -1, 2 * 2 * y - 0.5f) * (1 + (pourcentB / 50)) * forceItem);
            }

            return true;
        }

        protected void Attack()
        {
            if (!inAction & canHit)
            {
                x = 0;
                if (direction == Direction.Up)
                {
                    TimeBetweenFrame = 75;
                    Reset(new Point(0, 4));
                    allongeCoup = 20;
                    frameHit = 2;
                }
                else if (direction == Direction.Down && !canJump)
                {
                    TimeBetweenFrame = 100;
                    Reset(new Point(0, 6));
                    allongeCoup = 15;
                    frameHit = 2;
                }
                else
                {
                    TimeBetweenFrame = 50;
                    if (canJump)
                    {
                        Reset(new Point(0, 3));
                        allongeCoup = 15;
                        frameHit = 1;
                    }
                    else
                    {
                        Reset(new Point(0, 5));
                        allongeCoup = 25;
                        frameHit = 2;
                    }
                    x = lookingRight ? 1 : -1;
                }
                //chargedAttack = CurrentFrame.Y;
                definition.Loop = false;
                inAction = true;

                y = direction == Direction.Up ? -1 : direction == Direction.Down ? canJump ? 0 : 1 : 0;
                // Si la direction est vers le haut y = -1, si la direction est vers le bas et le personnage est au sol y = 0
                // Si la direction est vers le bas et le personnage est en l'air y = 1
                // Si c'est aucune des deux directions y = 0;
            }
            //isCharging = true;
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
        protected void Roulade()
        {
            if (!inAction & compteurRoulade > 500)
            {
                Reset(new Point(0, 7));
                definition.Loop = false;
                inAction = true;
                canHit = false;
                TimeBetweenFrame = 75;
                x = lookingRight ? 1 : -1;
                bodyPosition = new Vector2(bodyPosition.X + x, bodyPosition.Y);
                body.ApplyForce(new Vector2(0, 0.001f));
            }
        }
        protected void Protection()
        {
            if (!inAction)
            {
                CurrentFrame = new Point(0, 9);
                FinishedAnimation = true;
                definition.Loop = false;
                inAction = true;
                canHit = false;
                isProtecting = true;
            }
        }
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
            if (particles != null)
                particles.Draw(spriteBatch);
        }
    }
}