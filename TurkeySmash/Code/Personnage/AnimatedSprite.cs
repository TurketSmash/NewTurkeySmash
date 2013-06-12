using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;

namespace TurkeySmash
{
    class AnimatedSpriteDef
    {
        public string AssetName { get; set; }
        public Point FrameSize { get; set; }
        public Point NbFrames { get; set; }
        public int FrameRate { get; set; }
        public bool Loop { get; set; }
    }

    class AnimatedSprite
    {
        public AnimatedSpriteDef definition;
        Texture2D texture;
        protected Point CurrentFrame;
        protected bool FinishedAnimation = false;
        protected double TimeBetweenFrame = 100;
        protected double lastFrameUpdatedTime = 0;
        float scale = 1.0f;
        public Body body;
        public Vector2 bodyPosition { get { return body.Position; } set { body.Position = value; } }
        public Vector2 bodySize;
        protected SpriteEffects effects;
        protected Color color = Color.White;
        protected World world;

        int frameRate = 60;

        public AnimatedSprite(World world, Vector2 position, float density, Vector2 bodySize, AnimatedSpriteDef definition)
        {
            this.definition = definition;
            this.bodySize = bodySize;
            CurrentFrame = new Point();
            frameRate = definition.FrameRate;
            body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(bodySize.X * scale), ConvertUnits.ToSimUnits(bodySize.Y * scale), density);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Restitution = 0.3f;
            texture = TurkeySmashGame.content.Load<Texture2D>(definition.AssetName);
            this.world = world;
        }

        public void Reset(Point point)
        {
            CurrentFrame = point;
            FinishedAnimation = false;
            lastFrameUpdatedTime = 0;
            definition.Loop = true;
        }

        public virtual void Update(GameTime time)
        {
            if (FinishedAnimation) return;
            lastFrameUpdatedTime += time.ElapsedGameTime.Milliseconds;
            if (lastFrameUpdatedTime > TimeBetweenFrame)
            {
                lastFrameUpdatedTime = 0;
                if (definition.Loop)
                {
                    CurrentFrame.X++;
                    if (CurrentFrame.X >= definition.NbFrames.X)
                    {
                        CurrentFrame.X = 0;
                    }
                }
                else
                {
                    CurrentFrame.X++;
                    if (CurrentFrame.X >= definition.NbFrames.X)
                    {
                        CurrentFrame.X = 0;
                        CurrentFrame.X = definition.NbFrames.X - 1;
                        FinishedAnimation = true;
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Vector2(ConvertUnits.ToDisplayUnits(bodyPosition.X) - (bodySize.X * scale), ConvertUnits.ToDisplayUnits(bodyPosition.Y) - (bodySize.Y * scale)),
                                    new Rectangle(CurrentFrame.X * definition.FrameSize.X, CurrentFrame.Y * definition.FrameSize.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                    color, 0, Vector2.Zero, scale, effects, 0);
        }

        public void Resize(float largeur, float longueur)
        {
            scale = largeur / longueur;
        }
    }
}
