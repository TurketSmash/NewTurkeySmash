using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace Libraries
{
    public class Sprite
    {
        #region Fields 

        private Vector2 position = new Vector2(0, 0);
        private Texture2D texture;
        private Rectangle edge;
        private float scale = 1.0f;

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
            x = position.X;
            y = position.Y;
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

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(texture, position, new Rectangle(0, 0, texture.Width, texture.Height), Color.White, 0, Vector2.Zero, Scale, SpriteEffects.None, 0);
        }

        #endregion

        public void Resize(float largeur)
        {
            Scale = largeur / texture.Width;
        }
    }
}
