using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurkeySmash
{
    class SelectionPersonnage : Menu
    {
        #region Fields

        private BoutonImage boutonNaruto;
        private BoutonImage boutonSakura;
        private BoutonTexte boutonRetour;
        private BoutonTexte boutonOption;
        public static string persoSelect;

        #endregion

        #region Consctruction & initialization

        public SelectionPersonnage()
        {
            boutonNaruto = new BoutonImage();
            boutonSakura = new BoutonImage();
            boutonOption = new BoutonTexte();
            boutonRetour = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth / 2, 700);
            boutonRetour.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            boutonNaruto.Load(TurkeySmashGame.content, "Menu\\BoutonNaruto", boutons);
            boutonNaruto.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            boutonSakura.Load(TurkeySmashGame.content, "Menu\\BoutonSakura", boutons);
            boutonSakura.Position = new Microsoft.Xna.Framework.Vector2(2* TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            boutonRetour.Load(TurkeySmashGame.content, boutons);
        }

        public override void Bouton1()
        {
            persoSelect = "Naruto";
            Basic.SetScreen(new SelectionNiveau());
        }

        public override void Bouton2()
        {
            persoSelect = "Sakura";
            Basic.SetScreen(new SelectionNiveau());
        }

        public override void Bouton3()
        {
            Basic.Quit();
        }

        #endregion
    }
}
