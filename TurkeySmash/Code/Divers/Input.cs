using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace TurkeySmash
{
    class Input
    {
        #region Fields

        GamePadState pad;
        PlayerIndex player;
        Character personnage;
        enum GameState { Game, Disconnected }
        GameState gameState = GameState.Game;
        bool disconnect = false;

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

        public bool Up()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Up) || GamePad.GetState(player).ThumbSticks.Left.Y < -0.5f);
        }

        public bool Down()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Down) || GamePad.GetState(player).ThumbSticks.Left.Y > 0.5f);
        }

        public bool Right(PlayerIndex player)
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Right) || GamePad.GetState(player).ThumbSticks.Left.X > 0.5f);
        }

        public bool Left(PlayerIndex player)
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Left) || GamePad.GetState(player).ThumbSticks.Left.X < -0.5f);
        }

        public bool Jump(PlayerIndex player)
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(player).Buttons.A == ButtonState.Pressed);
        }

        public bool Action(PlayerIndex player)
        {
            return (Keyboard.GetState().IsKeyDown(Keys.A) || GamePad.GetState(player).Buttons.B == ButtonState.Pressed);
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
