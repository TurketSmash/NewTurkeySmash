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
        private BoutonTexte boutonOptions;
        public static string persoSelect;

        #endregion

        #region Consctruction & initialization

        public SelectionPersonnage()
        {
            boutonNaruto = new BoutonImage();
            boutonSakura = new BoutonImage();
            boutonOptions = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.6f);
            boutonOptions.Texte = "Options de Combat";
            boutonRetour = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.WindowSize.Y * 0.7f);
            boutonRetour.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            boutonNaruto.Load(TurkeySmashGame.content, "Menu\\BoutonNaruto", boutons);
            boutonNaruto.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            boutonSakura.Load(TurkeySmashGame.content, "Menu\\BoutonSakura", boutons);
            boutonSakura.Position = new Microsoft.Xna.Framework.Vector2(2* TurkeySmashGame.manager.PreferredBackBufferWidth / 3, TurkeySmashGame.manager.PreferredBackBufferHeight / 2);
            boutonOptions.Load(TurkeySmashGame.content, boutons);
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
            Basic.SetScreen(new OptionsCombat());
        }

        public override void Bouton4()
        {
            Basic.Quit();
        }

        #endregion
    }
}
