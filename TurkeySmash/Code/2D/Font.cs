using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    class Font
    {
        #region Fields

        private Vector2 position = new Vector2 (0, 0);
        private SpriteFont spriteFont;
        private Color color = Color.Black;
        private Vector2 FontOrigin;
        private float tailleText = 1.0f;
        private string police = "SuperMario";
        private string texte = "";

        #endregion

        #region Properties

        public Vector2 Position { set { position = value; } }
        public float XPos { set { position.X = value; } }
        public float YPos { set { position.Y = value; } }
        public string NameFont { set { police = value; } }
        public string Texte { set { texte = value; } }
        public Color Color { set { color = value; } }
        public float SizeText { set { tailleText = value; } }

        #endregion

        #region Construction

        public Font() { }

        public Font(Vector2 position)
        {
            Position = position;
        }

        public Font(float x, float y)
        {
            position.X = x;
            position.Y = y;
        }

        #endregion

        #region Load and Draw

        public void Load(ContentManager Content)
        {
            spriteFont = Content.Load<SpriteFont>(police);
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            FontOrigin = spriteFont.MeasureString(texte) / 2;
            Resize();
            spriteBatch.DrawString(spriteFont, texte, position, color, 0, FontOrigin, tailleText, SpriteEffects.None, 0.5f);
        }

        #endregion

        public void Resize()
        {
            if (TurkeySmashGame.manager.PreferredBackBufferWidth == 1280)
                tailleText = 0.7f;
            else if (TurkeySmashGame.manager.PreferredBackBufferWidth == 1920)
                tailleText = 1.3f;
            else
                tailleText = 1.0f;
        }

    }
}
