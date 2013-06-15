#region Using
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace TurkeySmash
{
    class BoutonImageMenu : Sprite, IBouton
    {
        #region Fields

        private bool etat = false;

        #endregion

        #region Properties

        public bool Etat { get { return etat; } set { etat = value; } }

        #endregion

        public BoutonImageMenu() { }

        #region Load and Draw

        public void Load(ContentManager Content, string assetName, string assetName2 , List<IBouton> Images)
        {
            base.Load(Content, assetName, assetName2);
            Images.Add(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (etat)
            {
                Texture2toTexture1();
            }
            else
            {
                Texture2toTexture1();
                Texture1toTexture2();
            }

            base.Draw(spriteBatch);
        }

        #endregion
    }
}
