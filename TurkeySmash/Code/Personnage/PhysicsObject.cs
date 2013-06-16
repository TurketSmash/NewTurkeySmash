using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace TurkeySmash
{
    class PhysicsObject
    {
        public Body body;
        public Sprite sprite;
        public Vector2 bodyPosition { get { return body.Position; } set { body.Position = value; } }

        public PhysicsObject(Sprite sprite)
        {
            this.sprite = sprite;
        }

        public virtual void Draw(Sprite sprite, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.texture, new Rectangle((int)ConvertUnits.ToDisplayUnits(bodyPosition.X), (int)ConvertUnits.ToDisplayUnits(bodyPosition.Y), sprite.Width, sprite.Height), null, Color.White, body.Rotation, sprite.Origin, SpriteEffects.None, 0f);
        }

        public virtual void Update(GameTime gameTime)
        {

        }
    }
}
