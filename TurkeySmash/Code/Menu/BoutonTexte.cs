using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    /// <summary>
    /// Class affichant un bouton sous forme d'un texte selectionnable
    /// </summary>
    class BoutonTexte : Font, IBouton
    {
        #region Fields 

        private bool etat = false;

        #endregion

        #region Properties

        public bool Etat { get { return etat; } set { etat = value; } }

        #endregion

        #region Construction 

        public BoutonTexte(float x = 0, float y = 0)
        {
            Position = new Vector2(x, y);
        }

        public BoutonTexte(Vector2 position)
        {
            Position = position;
        }

        #endregion

        #region Load and Draw 

        public void Load(ContentManager Content, List<IBouton> Boutons)
        {
            base.Load(Content);
            Boutons.Add(this);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (etat)
            {
                Color = Color.Red;
                SizeText = 1.5f;
            }
            else
            {
                Color = Color.Black;
                SizeText = 1.0f;
            }

            base.Draw(spriteBatch);
        }

        #endregion
    }
}
