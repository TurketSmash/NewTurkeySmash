using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using Libraries;

namespace TurkeySmash
{
    public class StaticPhysicsObject : PhysicsObject
    {
        public StaticPhysicsObject(World world, Vector2 position, float density, Sprite sprite)
            : base(world, position, 1, sprite)
        {
            body.BodyType = BodyType.Static;
        }
        public StaticPhysicsObject(World world, Vector2 position, float density,Vector2 bodySize)
            : base(world, position, 1, bodySize)
        {
            body.BodyType = BodyType.Static;
        }
    }
}