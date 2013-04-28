using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

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
        public Vector2 Position;
        protected AnimatedSpriteDef definition;
        protected Texture2D sprite;
        protected Point CurrentFrame;
        protected bool FinishedAnimation = false;
        protected double TimeBetweenFrame = 16;
        protected double lastFrameUpdatedTime = 0;

        int frameRate = 60;
        public int FrameRate
        {
            get
            {
                return frameRate;
            }
            set
            {
                if (value <= 0)
                    throw new ArgumentOutOfRangeException("Framerate can't be less or equl to 0");
                else
                {
                    if (frameRate != value)
                    {
                        frameRate = value;
                        TimeBetweenFrame = 1000.0d / (double)value;
                    }
                }
            }
        }

        public AnimatedSprite(AnimatedSpriteDef definition)
        {
            this.definition = definition;
            this.Position = new Vector2();
            CurrentFrame = new Point();
        }

        public void Initialize()
        {
            frameRate = definition.FrameRate;
        }

        public void LoadContent()
        {
            sprite = TurkeySmashGame.content.Load<Texture2D>(definition.AssetName);
        }

        public void Reset()
        {
            CurrentFrame = new Point();
            FinishedAnimation = false;
            lastFrameUpdatedTime = 0;
        }

        public void Update(GameTime time)
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
                        CurrentFrame.Y++;
                        if (CurrentFrame.Y >= definition.NbFrames.Y)
                            CurrentFrame.Y = 0;
                    }
                }
                else
                {
                    CurrentFrame.X++;
                    if (CurrentFrame.X >= definition.NbFrames.X)
                    {
                        CurrentFrame.X = 0;
                        CurrentFrame.Y++;
                        if (CurrentFrame.Y >= definition.NbFrames.Y)
                        {
                            CurrentFrame.X = definition.NbFrames.X - 1;
                            CurrentFrame.Y = definition.NbFrames.Y - 1;
                            FinishedAnimation = true;
                        }
                    }
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, new Rectangle((int)Position.X, (int)Position.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                    new Rectangle(CurrentFrame.X * definition.FrameSize.X, CurrentFrame.Y * definition.FrameSize.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                    Color.White);
        }
    }
}
