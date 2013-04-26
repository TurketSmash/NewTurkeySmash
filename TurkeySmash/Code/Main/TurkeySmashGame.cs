#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Libraries;
#endregion

namespace TurkeySmash
{
    public class TurkeySmashGame : Microsoft.Xna.Framework.Game
    {
        public static GraphicsDeviceManager manager;
        public static SpriteBatch spriteBatch;
        public static ContentManager content;
        Input input1;

        public TurkeySmashGame()
        {
            manager = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            content = Content;
        }

        protected override void Initialize()
        {
            this.Window.AllowUserResizing = false;
            this.Window.Title = "TurkeySmash";
            manager.PreferredBackBufferWidth = 1600; //parametre par defaut
            manager.PreferredBackBufferHeight = 900;
            manager.IsFullScreen = false;           
            manager.ApplyChanges();

            input1 = new Input(PlayerIndex.One);

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Basic.SetUp();  // Fonction établissant la scène d'accueil, la première image lors de l'ouverture du programme
        }

        protected override void UnloadContent()
        {
            
        }

        protected override void Update(GameTime gameTime)
        {
            Basic.Update(gameTime, input1);
            if (Basic.screens.Count < 1)
                this.Exit();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            Basic.Render();
            base.Draw(gameTime);
        }
    }
}


