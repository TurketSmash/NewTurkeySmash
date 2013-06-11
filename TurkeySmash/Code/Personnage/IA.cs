using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using FarseerPhysics;
using FarseerPhysics.Dynamics;
using FarseerPhysics.Factories;
using FarseerPhysics.Collision;
using FarseerPhysics.Dynamics.Contacts;
using FarseerPhysics.Collision.Shapes;
using System;


namespace TurkeySmash
{
    class IA : Character
    {
        public IA(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, playerindex, definition) { }


    }
}
