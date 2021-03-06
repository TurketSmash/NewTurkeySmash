﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurkeySmash
{
    class Input
    {
        #region Fields

        GamePadState pad;
        PlayerIndex player;
        enum GameState { Game, Disconnected }
        GameState gameState = GameState.Game;
        bool disconnect = false;
        public bool canAction = false;

        #endregion

        #region Construction

        public Input(PlayerIndex player)
        {
            this.player = player;
        }

        #endregion

        #region Update

        void UpdateInput()
        {
            disconnect = false;
            if (!GamePad.GetState(player).IsConnected)
                disconnect = true;
        }

        public void Update()
        {
            pad = GamePad.GetState(player);
            UpdateInput();

            switch (gameState)
            {
                case GameState.Game:
                    if (disconnect) 
                    {
                        gameState = GameState.Disconnected;
                    }
                    break;

                case GameState.Disconnected:
                    if (!disconnect)
                    {
                        gameState = GameState.Game;
                    }
                    break;
            }
        }

        #endregion

        #region Mapping

        public bool Up(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Up) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).ThumbSticks.Left.Y > 0.5f & player == PlayerIndex.One) ||
                (GamePad.GetState(player).ThumbSticks.Left.Y > 0.5f & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.O) & player == PlayerIndex.Four)
                );
        }

        public bool Down(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Down) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).ThumbSticks.Left.Y < -0.5f & player == PlayerIndex.One) ||
                (GamePad.GetState(player).ThumbSticks.Left.Y < -0.5f & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.L) & player == PlayerIndex.Four)
                );
        }

        public bool Right(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Right) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).ThumbSticks.Left.X > 0.5f & player == PlayerIndex.One) ||
                (GamePad.GetState(player).ThumbSticks.Left.X > 0.5f & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.M) & player == PlayerIndex.Four)
                );
        }

        public bool Left(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Left) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).ThumbSticks.Left.X < -0.5f & player == PlayerIndex.One) ||
                (GamePad.GetState(player).ThumbSticks.Left.X < -0.5f & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.K) & player == PlayerIndex.Four)
                );
        }

        public bool Jump(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Space) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).Buttons.A == ButtonState.Pressed & player == PlayerIndex.One) ||
                (GamePad.GetState(player).Buttons.A == ButtonState.Pressed & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.P) & player == PlayerIndex.Four)
                );
        }

        public bool Action(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.A) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).Buttons.B == ButtonState.Pressed & player == PlayerIndex.One) ||
                (GamePad.GetState(player).Buttons.B == ButtonState.Pressed & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.I) & player == PlayerIndex.Four)
                );
        }

        public bool Roulade(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.E) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).Buttons.Y == ButtonState.Pressed & player == PlayerIndex.One) ||
                (GamePad.GetState(player).Buttons.Y == ButtonState.Pressed & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.U) & player == PlayerIndex.Four)
                );
        }

        public bool Protection(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Z) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).Buttons.X == ButtonState.Pressed & player == PlayerIndex.One) ||
                (GamePad.GetState(player).Buttons.X == ButtonState.Pressed & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyDown(Keys.J) & player == PlayerIndex.Four)
                );
        }

        public bool ActionReleased(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyUp(Keys.A) & player == PlayerIndex.Three) ||
                (GamePad.GetState(player).Buttons.B == ButtonState.Released & player == PlayerIndex.One) ||
                (GamePad.GetState(player).Buttons.B == ButtonState.Released & player == PlayerIndex.Two) ||
                (Keyboard.GetState().IsKeyUp(Keys.I) & player == PlayerIndex.Four)
                );
        }

        public bool Enter()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Enter) ||
                GamePad.GetState(player).Buttons.Start == ButtonState.Pressed);
        }

        public bool Escape()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Escape) ||
                GamePad.GetState(player).Buttons.Back == ButtonState.Pressed);
        }

        #endregion 
    
    }
}
