using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class EndGameScreen : Menu
    {
        #region Fields

        private BoutonTexte boutonRejouer;
        private BoutonTexte boutonMainMenu;
        private float xPos = 500;
        private float yPos = 200;

        #endregion

        #region Construction and Initialization

        public EndGameScreen()
        {
            xPos = TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            boutonRejouer = new BoutonTexte(xPos, yPos + 200);
            boutonMainMenu = new BoutonTexte(xPos, yPos + 300);
            boutonRejouer.Texte = "Selection des personnages";
            boutonMainMenu.Texte = "Menu Principal";
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPause");
            boutonRejouer.Load(TurkeySmashGame.content, boutons);
            boutonMainMenu.Load(TurkeySmashGame.content, boutons);
        }

        #endregion

        public override void Bouton1()
        {
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
        }

        public override void Bouton2()
        {
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
        }
    }
}
