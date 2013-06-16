using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
        private Texture2D texture;
        protected Vector2 position; // position en haut à gauche en DisplayUnits
        protected Point CurrentFrame;
        public bool FinishedAnimation = false;
        protected double TimeBetweenFrame = 100;
        protected double lastFrameUpdatedTime = 0;
        protected float scale = 1.0f;
        protected SpriteEffects spriteEffects;
        protected Color color = Color.White;
        int frameRate = 60;

        public AnimatedSprite(Vector2 position, AnimatedSpriteDef definition)
        {
            this.definition = definition;
            this.position = position;
            CurrentFrame = new Point();
            frameRate = definition.FrameRate;
            texture = TurkeySmashGame.content.Load<Texture2D>(definition.AssetName);
        }
        public void Reset(Point point)
        {
            CurrentFrame = point;
            FinishedAnimation = false;
            lastFrameUpdatedTime = 0;
            definition.Loop = true;
        }
        public virtual void Update(GameTime gameTime)
        {
            if (FinishedAnimation) return;
            lastFrameUpdatedTime += gameTime.ElapsedGameTime.Milliseconds;
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
        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(CurrentFrame.X * definition.FrameSize.X, CurrentFrame.Y * definition.FrameSize.Y, definition.FrameSize.X, definition.FrameSize.Y),
                                    color, 0, Vector2.Zero, scale, spriteEffects, 0);
        }
        public void Resize(float largeur, float longueur)
        {
            scale = largeur / longueur;
        }
    }
}
