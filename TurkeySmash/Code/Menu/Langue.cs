using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurkeySmash
{
    class Langue : Menu
    {
        #region Fields 

        public static bool French = false;

        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private float xPos = 350;
        private float yPos = 200;

        private Texte antibug1 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);

        #endregion

        #region Construction and Initialization

        public Langue()
        {
            xPos = 3 * TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton2 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.55f);
            bouton2.Texte = "Retour";
            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2);
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Options");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(400, 120);
            bouton1.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
            bouton1.Texte = French ? "Francais" : "English";
        }

        #endregion


        public override void Bouton1()
        {
            French = !French;
            bouton1.Texte = French ? "Francais" : "English";
            bouton2.Texte = French ? "Retour" : "Back";
        }
        public override void Bouton2()
        {
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.SetScreen(new Accueil());
            Basic.SetScreen(new Options());
        }
    }
}
