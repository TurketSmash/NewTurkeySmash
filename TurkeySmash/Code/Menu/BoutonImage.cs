#region Using
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

#endregion

namespace TurkeySmash
{
    class BoutonImage : Sprite, IBouton
    {
        #region Fields 

        private bool etat = false;

        #endregion

        #region Properties

        public bool Etat { get { return etat; } set { etat = value; } }

        #endregion

        public BoutonImage() { }

        #region Load and Draw 

        public void Load(ContentManager Content, string assetName, List<IBouton> Images)
        {
            base.Load(Content, assetName);
            Images.Add(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (etat)
            {
                Scale = 0.65f;
            }
            else
            {
                Scale = 0.6f;
            }

            base.Draw(spriteBatch);
        }

        #endregion
    }
}
