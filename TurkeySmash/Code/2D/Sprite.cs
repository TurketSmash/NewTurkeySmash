using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    class Sprite
    {
        #region Fields 

        Vector2 position;
        public Texture2D texture;
        public Texture2D texture2 = null;
        public Texture2D aux = null;
        Rectangle edge;
        private float scale = 1.0f;
        public int Width { get { return texture.Width; } }
        public int Height { get { return texture.Height; } }
        public Vector2 Origin { get { return new Vector2(Width / 2, Height / 2); } }

        #endregion

        #region Properties

        public Vector2 Position 
        { 
            get 
            { 
                return position; 
            } 
            set 
            {
                try
                {
                    position.X = value.X - texture.Width / 2;
                    position.Y = value.Y - texture.Height / 2;
                }
                catch (NullReferenceException ex)
                {
                    Console.WriteLine(ex.Source);
                }
            } 
        }

        public float Scale
        {
            get
            {
                return scale;
            }
            set
            {
                scale = value;
                edge = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
            }
        }

        #endregion

        #region Construction

        public Sprite(float x = 0, float y = 0)
        {
            position.X = x;
            position.Y = y;
        }

        #endregion

        #region Load & Draw

        public void Load(ContentManager content, string assetName)
        {
            try
            {
                texture = content.Load<Texture2D>(assetName);
            }
            catch
            {
                texture = content.Load<Texture2D>("Defaut");
            }

            edge = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
        }

        public void Load(ContentManager content, string assetName, string assetName2)
        {
            try
            {
                texture = content.Load<Texture2D>(assetName);
                texture2 = content.Load<Texture2D>(assetName2);
                aux = content.Load<Texture2D>(assetName);
            }
            catch
            {
                texture = content.Load<Texture2D>("Defaut");
                texture2 = content.Load<Texture2D>("Defaut");
                aux = content.Load<Texture2D>("Defaut");
            }

            edge = new Rectangle((int)position.X, (int)position.Y, (int)(texture.Width * Scale), (int)(texture.Height * Scale));
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        public void DrawAsBackground(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, new Rectangle((int)position.X, (int)position.Y, (int)TurkeySmashGame.WindowSize.X, (int)TurkeySmashGame.WindowSize.Y), null, Color.White, 0f, Vector2.Zero, SpriteEffects.None, 1f);
        }

        public void Texture1toTexture2()
        {
            aux = texture;
            texture = texture2;
        }
        public void Texture2toTexture1()
        {
            texture = aux;
            aux = texture;
        }

        #endregion

        public void Resize(float largeur)
        {
            Scale = largeur / texture.Width;
        }
    }
}
