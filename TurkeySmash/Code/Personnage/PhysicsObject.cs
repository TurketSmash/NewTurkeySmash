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
        public Vector2 bodyPosition { get { return body.Position; } }

        public PhysicsObject(World world, Vector2 position, float density, Vector2 bodySize)
        {
            body = BodyFactory.CreateRectangle(
                world,bodySize.X, bodySize.Y, density);
            body.BodyType = BodyType.Dynamic;
            body.Position = position;
            body.Restitution = 0.3f;
        }

        public PhysicsObject(World world, Vector2 position, float density, Sprite sprite)
        {
            this.sprite = sprite;
            body = BodyFactory.CreateRectangle(
                world,ConvertUnits.ToSimUnits(sprite.Width), ConvertUnits.ToSimUnits(sprite.Height), density);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Restitution = 0.3f;
        }

        public virtual void Update(GameTime gameTime)
        {

        }

        public virtual void Draw(Sprite sprite, SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.texture, new Rectangle((int)ConvertUnits.ToDisplayUnits(bodyPosition.X), (int)ConvertUnits.ToDisplayUnits(bodyPosition.Y), sprite.Width, sprite.Height), null, Color.White, body.Rotation, sprite.Origin, SpriteEffects.None, 0f);
        }

    }
}

