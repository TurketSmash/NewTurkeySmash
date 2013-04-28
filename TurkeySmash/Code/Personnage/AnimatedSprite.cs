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
        Texture2D sprite;
        protected Point CurrentFrame;
        bool FinishedAnimation = false;
        protected double TimeBetweenFrame = 100;
        double lastFrameUpdatedTime = 0;
        public Body body;
        public Vector2 bodyPosition { get { return body.Position; } set { body.Position = value; } }
        public Vector2 bodySize;
        public Vector2 Origin { get { return new Vector2(bodySize.X / 2, bodySize.Y / 2); } }
        protected SpriteEffects effects;

        int frameRate = 60;

        public AnimatedSprite(World world, Vector2 position, float density, Vector2 bodySize, AnimatedSpriteDef definition)
        {
            this.definition = definition;
            this.bodySize = bodySize;
            CurrentFrame = new Point();
            frameRate = definition.FrameRate;
            body = BodyFactory.CreateRectangle(world, ConvertUnits.ToSimUnits(bodySize.X), ConvertUnits.ToSimUnits(bodySize.Y), density);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Restitution = 0.3f;
            sprite = TurkeySmashGame.content.Load<Texture2D>(definition.AssetName);
        }

        public void Reset()
        {
            CurrentFrame = new Point();
            FinishedAnimation = false;
            lastFrameUpdatedTime = 0;
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
            spriteBatch.Draw(sprite, new Rectangle((int)(ConvertUnits.ToDisplayUnits(bodyPosition.X) - bodySize.X), (int)(ConvertUnits.ToDisplayUnits(bodyPosition.Y) - bodySize.Y), definition.FrameSize.X, definition.FrameSize.Y),
                                    new Rectangle(CurrentFrame.X * definition.FrameSize.X, CurrentFrame.Y * definition.FrameSize.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                    Color.White, 0, Vector2.Zero, effects, 0);
        }
    }
}
