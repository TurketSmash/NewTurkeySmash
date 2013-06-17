using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Dynamics.Contacts;
using System.Collections.Generic;
using FarseerPhysics.Factories;
using Microsoft.Xna.Framework.Audio;
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

        protected bool canJump = true;
        protected bool inAction = false;
        public bool lookingRight = true;
        protected bool jump = false;
        protected bool isMoving = false;
        protected Direction direction;
        private Direction oldDirection;

        SoundEffect punchCharacter;
        SoundEffect punchMiss;
        SoundEffect punchObjet;
        SoundEffect punchBonus;
        SoundEffect teleportation;
        
        int forcePower = 3; // force appliquée au personnage lors des déplacements droite/gauche
        bool canHit = true;
        bool isHit = false;
        bool isProtecting = false;
        bool invincible = false;
        bool powerUp = false;
        //bool isCharging = false;
        //int chargedAttack = 1;
        int frameHit = 0;
        Vector2 oldPosition; // variable pour calcul la chute du personnage
        Vector2 newPosition;

        ParticleEngine particles;
        List<AnimatedSprite> animatedEffects = new List<AnimatedSprite>();
        List<Texture2D> textures = new List<Texture2D>();

        int oldPourcent;
        int i = 0; // compteur nombre de frame filtre rouge
        int compteur = 0;
        int compteurRoulade = 0;
        int compteurProtection = 0;
        int compteurInvincible = 0;
        int compteurPowerUp = 0;
        const int tempsProtection = 5000; // en ms
        const int tempsInvincibilite = 30000; // 30 sec
        const int tempsPowerUp = 15000; // 15 sec
        int x = 0;
        int y = 0;
        int pourcentageInflige = 3;

        int allongeCoup = 18;
        int forceJump = 70;
        float forceItem = 0.3f;
        float maxForceCharged = 1.0f;
        float ForceItem { get { return forceItem; } set { forceItem = forceItem > maxForceCharged ? maxForceCharged : value; } }
        public bool Mort { get { return vie < 1; } }

        #region Bodie

        public Body body;
        public Vector2 bodyPosition { get { return body.Position; } set { body.Position = value; } } // EN SimUnits
        public Vector2 bodySize; // EN PIXEL
        World world;

        #endregion

        #region Constructeur
        /// <summary>
        /// Classe Character animée et possédant un body
        /// </summary>
        /// <param name="world">Monde dans le lequel ce trouve le character</param>
        /// <param name="position">En DisplayUnits(pixels)</param>
        /// <param name="density">Calcule du poids en fonction de la taille</param>
        /// <param name="bodySize">En DisplayUnits(pixels)</param>
        /// <param name="playerindex"></param>
        /// <param name="definition">Propriétés de l'image animée</param>
        public Character(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(position, definition)
        {
            this.playerindex = playerindex;
            this.world = world;
            this.bodySize = bodySize;
            body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(bodySize.X * scale), ConvertUnits.ToSimUnits(bodySize.Y * scale), density);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Restitution = 0.3f;
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
            CurrentFrame = new Point(0, 1);

            punchCharacter = TurkeySmashGame.content.Load<SoundEffect>("Sons\\effets\\PUNCH");
            punchMiss = TurkeySmashGame.content.Load<SoundEffect>("Sons\\effets\\whoosh");
            punchObjet = TurkeySmashGame.content.Load<SoundEffect>("Sons\\effets\\punchboite");
            punchBonus = TurkeySmashGame.content.Load<SoundEffect>("Sons\\effets\\bonus");
            teleportation = TurkeySmashGame.content.Load<SoundEffect>("Sons\\effets\\Teleportation");

        }

        #endregion 

        public override void Update(GameTime gameTime)
        {
            if (playerindex == PlayerIndex.One)
                Console.WriteLine(forceItem);

            position = ConvertUnits.ToDisplayUnits(bodyPosition) - bodySize; // update de la position de l'image
            newPosition = bodyPosition;
            FarseerBodyUserData userdata = (FarseerBodyUserData)body.UserData;
            userdata.Protecting = isProtecting;
            invincible = userdata.Invincible;
            powerUp = userdata.PowerUp;
            int time = gameTime.ElapsedGameTime.Milliseconds;

            #region animatedEffects & particules update

            if (animatedEffects.Count > 0)
            {
                for (int k = 0; k < animatedEffects.Count; k++)
                {
                    animatedEffects[k].Update(gameTime);
                    if (animatedEffects[k].FinishedAnimation)
                        animatedEffects.RemoveAt(k);
                }
            }

            if (particles != null)
                particles.Update(gameTime, ConvertUnits.ToDisplayUnits(bodyPosition));

            #endregion

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

            #region Update Compteurs

            if (!inAction)
                compteurRoulade += time;
            else
                compteurRoulade = 0;

            if (isProtecting)
                compteurProtection += time;
            else
                compteurProtection -= compteurProtection > 0 ? time : 0;

            if (invincible)
                compteurInvincible += time;
            else
                compteurInvincible = 0;

            if (powerUp)
                compteurPowerUp+= time;
            else
                compteurPowerUp = 0;

            #endregion

            #region Protection

            if (compteurProtection > tempsProtection)
            {
                isProtecting = false;
                userdata.Protecting = false;
            }
            else
            {
                if (isProtecting)
                {
                    particles = new ParticleEngine(TurkeySmashGame.content.Load<Texture2D>("Jeu\\effets\\bulleProtectrice"), ConvertUnits.ToDisplayUnits(new Vector2(bodyPosition.X, bodyPosition.Y - 0.1f)),
                        Vector2.Zero, Color.Red, 1, BlendState.Additive, 0, 1.0f, false, false, false, false, false, false);
                }
            }

            #endregion

            #region Invincible

            if (compteurInvincible > tempsInvincibilite)
            {
                invincible = false;
                userdata.Invincible = false;
            }
            else
            {
                compteur += time;
                if (invincible & compteur > 600)
                {
                    particles = new ParticleEngine(textures, ConvertUnits.ToDisplayUnits(bodyPosition), Vector2.Zero, Color.Yellow, 15, 600, 1.0f, false);
                    color = Color.DarkBlue;
                    compteur = 0;
                }
            }

            #endregion

            #region PowerUp

            if (compteurPowerUp > tempsPowerUp)
            {
                powerUp = false;
                userdata.PowerUp = false;
            }
            else
            {
                compteur += time;
                if (powerUp & compteur > 600)
                {
                    forceItem = 0.8f;
                    color = new Color(210,70,90);
                    compteur = 0;
                }
            }

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
                    spriteEffects = SpriteEffects.None;
                else
                    spriteEffects = SpriteEffects.FlipHorizontally;
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
            {
                if (!invincible & !powerUp)
                    color = Color.White;
                if (!powerUp)
                    forceItem = 0.3f;
            }

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
            oldDirection = direction;
            direction = Direction.Nodirection;
            pourcent = userdata.Pourcent;
            oldPosition = newPosition;
            isMoving = false;
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
                    punchBonus.Play();
                    if (dataB.BonusType == "dinde")
                        dataA.PowerUp = true;
                    if (dataB.BonusType == "pourcent")
                    {
                        dataA.Pourcent -= 3 * 3;
                        if (dataA.Pourcent < 0)
                            dataA.Pourcent = 0;
                    }
                    if (dataB.BonusType == "invincible")
                        dataA.Invincible = true;
                    dataB.IsUsed = true;
                }
                if (dataB.IsCharacter)
                {
                    if (!dataB.Protecting & !dataB.Invincible)
                    {
                        punchCharacter.Play();
                        dataB.LastHit = Convert.PlayerIndex2Int(playerindex);
                        dataB.Pourcent = dataB.Pourcent + pourcentageInflige;
                        pourcentB = dataB.Pourcent;
                        fixB.Body.ApplyLinearImpulse(new Vector2(lookingRight ? 1 : -1, 2 * y - 0.5f) * (1 + (pourcentB / 50)) * forceItem);
                        particles = new ParticleEngine(textures, ConvertUnits.ToDisplayUnits(new Vector2(fixB.Body.Position.X, fixB.Body.Position.Y - 0.2f)), new Vector2(0, 0), Color.White, 4, 500, 1.2f);
                    }
                    else
                    {
                        punchMiss.Play();
                    }
                }
            }
            else
            {
                punchObjet.Play();
                fixB.Body.ApplyLinearImpulse(new Vector2(lookingRight ? 12 : -12, 2 * 2 * y - 0.5f) * (1 + (pourcentB / 50)) * forceItem);
            }

            return true;
        }

        protected void Attack()
        {
            if (!inAction & canHit)
            {
                punchMiss.Play();
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
                isMoving = true;
                body.ApplyForce(force, body.Position);
            }
        }
        protected void Roulade()
        {
            if (!inAction & compteurRoulade > 1000)
            {
                teleportation.Play();
                Reset(new Point(0, 7));
                definition.Loop = false;
                inAction = true;
                canHit = false;
                TimeBetweenFrame = 125;
                x = lookingRight ? 2 : -2;
                bodyPosition = new Vector2(bodyPosition.X + x, bodyPosition.Y);
                body.ApplyForce(new Vector2(0, 0.001f));

                animatedEffects.Add(new AnimatedSprite(new Vector2(ConvertUnits.ToDisplayUnits(oldPosition.X) - 55, ConvertUnits.ToDisplayUnits(oldPosition.Y) - 75), new AnimatedSpriteDef()
                {
                    AssetName = "Jeu\\effets\\fumeeTp",
                    FrameRate = 60,
                    FrameSize = new Point(110, 110),
                    Loop = false,
                    NbFrames = new Point(5, 1),
                }));
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
            foreach (AnimatedSprite effect in animatedEffects)
                effect.Draw(spriteBatch);
            if (particles != null)
                particles.Draw(spriteBatch);
        }
    }
}