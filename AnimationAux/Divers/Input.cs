using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Libraries
{
    public class Input
    {
        #region Fields

        private PlayerIndex player;
        private Vector2 Lstick;
        private enum GameState { Game, Disconnected }
        private GameState gameState = GameState.Game;
        private bool disconnect = false;

        #endregion

        #region Properties

        public Vector2 LStick { get { return Lstick; } set { Lstick = value; } }

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
            UpdateInput();

            switch (gameState)
            {
                case GameState.Game:
                    if (disconnect) 
                    {
                        gameState = GameState.Disconnected;
                    }
                    LStick = GamePad.GetState(player).ThumbSticks.Left;
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
            return (Keyboard.GetState().IsKeyDown(Keys.Up) || LStick.Y > 0.5f);
        }

        public bool Down()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Down) || LStick.Y < -0.5f);
        }

        public bool Right()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Right) || LStick.X > 0.5f);
        }

        public bool Left()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Left) || LStick.X < -0.5f);
        }

        public bool Jump()
        {
            return (Keyboard.GetState().IsKeyDown(Keys.Space) || GamePad.GetState(player).Buttons.A == ButtonState.Pressed);
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
