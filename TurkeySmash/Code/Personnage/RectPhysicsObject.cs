using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace TurkeySmash
{

    class RectPhysicsObject : PhysicsObject
    {

        public RectPhysicsObject(World world, Vector2 position, float density, Vector2 bodySize)
            : base(null)
        {
            body = BodyFactory.CreateRectangle(
                world,ConvertUnits.ToSimUnits(bodySize.X), ConvertUnits.ToSimUnits(bodySize.Y), density);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Restitution = 0.3f;
        }

        public RectPhysicsObject(World world, Vector2 position, float density, Sprite sprite)
            : base(sprite)
        {
            this.sprite = sprite;
            body = BodyFactory.CreateRectangle(
                world,ConvertUnits.ToSimUnits(sprite.Width), ConvertUnits.ToSimUnits(sprite.Height), density);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Restitution = 0.3f;
        }

    }
}

