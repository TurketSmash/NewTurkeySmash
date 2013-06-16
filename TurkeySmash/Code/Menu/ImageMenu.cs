using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    class ImageMenu : Sprite
    {
        #region Construction 


        public ImageMenu(float x, float y)
            : base(x, y) { }

        #endregion

        #region Load and Draw 

        public void Load(ContentManager Content,string assetName, List<Sprite> sprites)
        {
            base.Load(Content, assetName);
            sprites.Add(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);
        }

        #endregion
    
    }
}
