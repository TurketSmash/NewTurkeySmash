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
        private BoutonImage bouton2;
        private BoutonTexte bouton3;
        public static string persoSelect;

        #endregion

        #region Consctruction & initialization

        public SelectionPersonnage()
        {
            bouton1 = new BoutonImage();
            bouton2 = new BoutonImage();
            bouton3 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth / 2, 700);
            bouton3.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            bouton1.Load(TurkeySmashGame.content, "Menu\\BoutonNaruto", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            bouton2.Load(TurkeySmashGame.content, "Menu\\BoutonSakura", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(2* TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            bouton3.Load(TurkeySmashGame.content, boutons);
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
