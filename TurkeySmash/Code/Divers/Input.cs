using Microsoft.Xna.Framework;
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
            return ((Keyboard.GetState().IsKeyDown(Keys.Up) & player == PlayerIndex.Two) || (GamePad.GetState(player).ThumbSticks.Left.Y > 0.5f & player == PlayerIndex.One));
        }

        public bool Down(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Down) & player == PlayerIndex.Two) || (GamePad.GetState(player).ThumbSticks.Left.Y < -0.5f & player == PlayerIndex.One));
        }

        public bool Right(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Right) & player == PlayerIndex.Two) || (GamePad.GetState(player).ThumbSticks.Left.X > 0.5f & player == PlayerIndex.One));
        }

        public bool Left(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Left) & player == PlayerIndex.Two) || (GamePad.GetState(player).ThumbSticks.Left.X < -0.5f & player == PlayerIndex.One));
        }

        public bool Jump(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.Space) & player == PlayerIndex.Two) || (GamePad.GetState(player).Buttons.A == ButtonState.Pressed & player == PlayerIndex.One));
        }

        public bool Action(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.A) & player == PlayerIndex.Two) || (GamePad.GetState(player).Buttons.B == ButtonState.Pressed & player == PlayerIndex.One));
        }

        public bool Roulade(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyDown(Keys.E) & player == PlayerIndex.Two) || (GamePad.GetState(player).Buttons.Y == ButtonState.Pressed & player == PlayerIndex.One));
        }

        public bool ActionReleased(PlayerIndex player)
        {
            return ((Keyboard.GetState().IsKeyUp(Keys.A) & player == PlayerIndex.Two) || (GamePad.GetState(player).Buttons.B == ButtonState.Released & player == PlayerIndex.One));
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
