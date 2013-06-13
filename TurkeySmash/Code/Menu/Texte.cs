using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    class Texte : Font
    {
        #region Construction 

        public Texte(float x = 0, float y = 0)
        {
            Position = new Vector2(x, y);
        }

        public Texte(Vector2 position)
        {
            Position = position;
        }

        #endregion

        #region Load and Draw 

        public void Load(ContentManager Content, List<Texte> Textes)
        {
            base.Load(Content);
            Textes.Add(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
                Color = Color.Black;
                SizeText = 1.0f;

            base.Draw(spriteBatch);
        }

        #endregion
    
    }
}
