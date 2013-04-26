using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurkeySmash
{
    class SelectionPersonnage : Menu
    {
        #region Fields

        private BoutonImage bouton1;
        private BoutonTexte bouton2;
        public static string persoSelect;

        #endregion

        #region Consctruction & initialization

        public SelectionPersonnage()
        {
            bouton1 = new BoutonImage();
            bouton2 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth / 2, 700);
            bouton2.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            bouton1.Load(TurkeySmashGame.content, "Menu\\perso1", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            bouton2.Load(TurkeySmashGame.content, boutons);
        }

        public override void Bouton1()
        {
            persoSelect = "soldat";
            Basic.SetScreen(new SelectionNiveau());
        }

        public override void Bouton2()
        {
            Basic.Quit();
        }

        #endregion
    }
}
