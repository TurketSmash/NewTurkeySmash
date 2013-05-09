using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Collision.Shapes;

namespace TurkeySmash
{
    class RoundPhysicsObject : PhysicsObject
    {

        public RoundPhysicsObject(World world, Vector2 position, float density,float restitution, Sprite sprite)
            : base(world, position, density,Vector2.Zero, sprite)
        {
            body = new Body(world);
            CircleShape circleShape = new CircleShape(ConvertUnits.ToSimUnits(sprite.Height/2),density);
            body.CreateFixture(circleShape);
            body.BodyType = BodyType.Dynamic;
            body.Position = ConvertUnits.ToSimUnits(position);
            body.Friction = 0.4f;
            body.Restitution = restitution;
        }

    }
    
}
