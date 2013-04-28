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
        public Vector2 position;
        AnimatedSpriteDef definition;
        Texture2D sprite;
        Point CurrentFrame;
        bool FinishedAnimation = false;
        double TimeBetweenFrame = 16;
        double lastFrameUpdatedTime = 0;
        public Body body;
        public Vector2 bodyPosition { get { return body.Position; } set { body.Position = value; } }
        public Vector2 bodySize;
        public Vector2 Origin { get { return new Vector2(bodySize.X / 2, bodySize.Y / 2); } }

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

        public AnimatedSprite(World world, Vector2 position, float density, Vector2 bodySize, AnimatedSpriteDef definition)
        {
            this.definition = definition;
            this.bodySize = bodySize;
            CurrentFrame = new Point();
            frameRate = definition.FrameRate;
            body = BodyFactory.CreateRectangle(world, bodySize.X, bodySize.Y, density);
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
            Console.WriteLine(" {0} x, {0} y", bodyPosition.X, bodyPosition.Y);
            spriteBatch.Draw(sprite, new Rectangle((int)ConvertUnits.ToDisplayUnits(bodyPosition.X - bodySize.X), (int)ConvertUnits.ToDisplayUnits(bodyPosition.Y - bodySize.Y), definition.FrameSize.X, definition.FrameSize.Y),
                                    new Rectangle(CurrentFrame.X * definition.FrameSize.X, CurrentFrame.Y * definition.FrameSize.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                    Color.White);
        }
    }
}
