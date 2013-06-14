﻿using Microsoft.Xna.Framework;
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
            base.Update(gameTime);

            if (input.Left(playerindex))
            {
                direction = Direction.Left;
                lookingRight = false;
                isMoving = true;
                Moving();
            }
            else if (input.Right(playerindex))
            {
                direction = Direction.Right;
                lookingRight = true;
                isMoving = true;
                Moving();
            }
            else
                isMoving = false;

            if (input.Up(playerindex))
                direction = Direction.Up;
            else if (input.Down(playerindex))
                direction = Direction.Down;

            if (input.Action(playerindex))
                Attack();

            if (input.Jump(playerindex))
                Jump();

            if (input.Roulade(playerindex))
                Roulade();
        }
    }
}
