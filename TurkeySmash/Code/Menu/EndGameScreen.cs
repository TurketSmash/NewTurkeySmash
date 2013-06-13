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
        private Texte affichageGagnant;

        #endregion

        #region Construction and Initialization

        public EndGameScreen()
        {
            boutonRejouer = new BoutonTexte(TurkeySmashGame.WindowSize.X / 4, 9 * TurkeySmashGame.WindowSize.Y / 10);
            boutonMainMenu = new BoutonTexte(3 * TurkeySmashGame.WindowSize.X / 4, 9 * TurkeySmashGame.WindowSize.Y / 10);
            boutonRejouer.Texte = "Rejouer";
            boutonMainMenu.Texte = "Menu Principal";
            affichageGagnant = new Texte(TurkeySmashGame.WindowMid);
            affichageGagnant.Texte = "Gagnant : joueur " + Winner.winner[0].ToString();
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuGameOver");
            boutonRejouer.Load(TurkeySmashGame.content, boutons);
            boutonMainMenu.Load(TurkeySmashGame.content, boutons);
            affichageGagnant.Load(TurkeySmashGame.content, textes);
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
