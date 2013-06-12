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
    class Joueur : Character
    {
        Input input;

        public Joueur(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex playerindex, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, playerindex, definition)
        {
            input = new Input(playerindex);
        }

        public override void Update(GameTime gameTime)
        {
            if (input.Left(playerindex))
            {
                direction = Direction.Left;
                lookingRight = false;
                isMoving = true;
            }
            else if (input.Right(playerindex))
            {
                direction = Direction.Right;
                lookingRight = true;
                isMoving = true;
            }
            else
                isMoving = false;

            if (input.Up(playerindex))
            {
                direction = Direction.Up;
            }
            else if (input.Down(playerindex))
            {
                direction = Direction.Down;
            }

            action = input.Action(playerindex);
            jump = input.Jump(playerindex);


            base.Update(gameTime);
        }
    }
}
