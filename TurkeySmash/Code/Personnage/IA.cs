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
        bool canAction = false;
        bool fuir = false;
        bool booleenDeLaFamille = true;
        int compteur = 0;


        public IA(World world, Vector2 position, float density, Vector2 bodySize, PlayerIndex player, AnimatedSpriteDef definition)
            : base(world, position, density, bodySize, player, definition)
        { }

        Vector2[] posPerso = new Vector2[4];


        public void UpdatePosition(Character[] personnages)
        {
            int i = 0;
            foreach (Character perso in personnages)
            {
                if ((perso != null) && (playerindex != perso.playerindex))
                {
                    posPerso[i] = perso.bodyPosition;
                    i = i + 1;
                }
            }
        }


        public override void Update(GameTime gameTime)
        {
            booleenDeLaFamille = false;
            if (compteur < 25)
                compteur = compteur + 1;
            else
            {
                compteur = 0;
                booleenDeLaFamille = true;
            }

            float positionXLaPlusProche = 99999; //chiffre assez grand pour qu'il soit modifié des le premier "mecton" du foreach qui suit.
            float positionYLaPlusProche = -1;

            foreach (Vector2 position in posPerso)
            {
                if (position != null)
                {
                    if ((position.X < positionXLaPlusProche) && (position.X != 0))
                    {
                        positionXLaPlusProche = position.X;
                        positionYLaPlusProche = position.Y;
                    }
                }
            }

            if ((bodyPosition.X <= positionXLaPlusProche + 0.5) && (bodyPosition.X >= positionXLaPlusProche - 0.5) && (bodyPosition.Y <= positionYLaPlusProche + 0.1) && (bodyPosition.Y >= positionYLaPlusProche - 0.1))
            {
                if ((bodyPosition.X <= positionXLaPlusProche) && (bodyPosition.X >= positionXLaPlusProche))
                {
                    lookingRight = false;
                    Roulade();
                    Attack();
                }
                else
                {
                    lookingRight = true;
                    Roulade();
                    Attack();
                }
            }

            if (fuir == false)
            {
                if (positionXLaPlusProche < (bodyPosition.X) - 1)
                {
                    lookingRight = false;
                    isMoving = true;
                    Moving();
                }
                else
                    if (positionXLaPlusProche > (bodyPosition.X) + 1)
                    {
                        lookingRight = true;
                        isMoving = true;
                        Moving();
                    }
                    else
                        isMoving = false;
            }


            if (fuir)
            {
                if ((bodyPosition.X < positionXLaPlusProche + 1.7) && (bodyPosition.X > positionXLaPlusProche))
                {
                    lookingRight = true;
                    isMoving = true;
                    Moving();
                }
                else
                    if ((bodyPosition.X > positionXLaPlusProche - 1.7) && (bodyPosition.X < positionXLaPlusProche))
                    {
                        lookingRight = false;
                        isMoving = true;
                        Moving();
                    }
                    else
                        isMoving = false;
            }

            if ((fuir == true) && ((bodyPosition.X > positionXLaPlusProche + 1.7) || (bodyPosition.X < positionXLaPlusProche - 1.7)))
            {
                fuir = false;
            }

            //Jump

            if ((positionYLaPlusProche < bodyPosition.Y - 0.3) && (booleenDeLaFamille))
                Jump();
            if ((positionYLaPlusProche < bodyPosition.Y - 0.3) && (booleenDeLaFamille))
                Jump();

            if ((positionXLaPlusProche < (bodyPosition.X) - 2.5f) || (positionXLaPlusProche > (bodyPosition.X) + 2.5f))
                Roulade();


            //Attack

            if ((bodyPosition.X <= positionXLaPlusProche + 0.6) && (bodyPosition.X >= positionXLaPlusProche - 0.6) && (fuir == false) && (bodyPosition.Y <= positionYLaPlusProche + 0.1) && (bodyPosition.Y >= positionYLaPlusProche - 0.1))
                canAction = true;

            if (canAction)
                Attack();

            if (canAction)
            {
                canAction = false;
                fuir = true;
            }
            base.Update(gameTime);
        }
    }
}
