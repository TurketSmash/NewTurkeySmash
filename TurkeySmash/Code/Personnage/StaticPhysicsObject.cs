using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;

namespace TurkeySmash
{
    class StaticPhysicsObject : RectPhysicsObject
    {
        public StaticPhysicsObject(World world, Vector2 position, float density, Sprite sprite)
            : base(world, position, 1, sprite)
        {
            body.BodyType = BodyType.Static;
            body.Friction = 0.3f;
        }
        public StaticPhysicsObject(World world, Vector2 position, float density,Vector2 bodySize)
            : base(world, position, 1, bodySize)
        {
            body.BodyType = BodyType.Static;
            body.Friction = 0.3f;
        }
    }
}