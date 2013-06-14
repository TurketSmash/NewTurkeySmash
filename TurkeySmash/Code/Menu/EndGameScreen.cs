﻿using System;
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
        private Texte affichagejoueur1;
        private Texte affichagejoueur2;
        private Texte affichagejoueur3;
        private Texte affichagejoueur4;

        #endregion

        #region Construction and Initialization

        public EndGameScreen()
        {
            boutonRejouer = new BoutonTexte(TurkeySmashGame.WindowSize.X / 4, 9 * TurkeySmashGame.WindowSize.Y / 10);
            boutonMainMenu = new BoutonTexte(3 * TurkeySmashGame.WindowSize.X / 4, 9 * TurkeySmashGame.WindowSize.Y / 10);
            boutonRejouer.Texte = "Rejouer";
            boutonMainMenu.Texte = "Menu Principal";

            affichagejoueur1 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2);
            affichagejoueur1.Texte = "1e : j" + Results.ResultsBoard[0][0] + " / score : " + Results.ResultsBoard[0][1];

            affichagejoueur2 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2 +50);
            affichagejoueur2.Texte = "2e : j" + Results.ResultsBoard[1][0] + " / score : " + Results.ResultsBoard[1][1];

            affichagejoueur3 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2 +100);
            affichagejoueur3.Texte = "3e : j" + Results.ResultsBoard[2][0] + " / score : " + Results.ResultsBoard[2][1];

            affichagejoueur4 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2+150);
            affichagejoueur4.Texte = "4e : j" + Results.ResultsBoard[3][0] + " / score : " + Results.ResultsBoard[3][1];


            affichagejoueur1.Color = Microsoft.Xna.Framework.Color.White;
            affichagejoueur2.Color = Microsoft.Xna.Framework.Color.White;
            affichagejoueur3.Color = Microsoft.Xna.Framework.Color.White;
            affichagejoueur4.Color = Microsoft.Xna.Framework.Color.White;
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuGameOver");
            boutonRejouer.Load(TurkeySmashGame.content, boutons);
            boutonMainMenu.Load(TurkeySmashGame.content, boutons);
            affichagejoueur1.Load(TurkeySmashGame.content, textes);
            affichagejoueur2.Load(TurkeySmashGame.content, textes);
            affichagejoueur3.Load(TurkeySmashGame.content, textes);
            affichagejoueur4.Load(TurkeySmashGame.content, textes);
        }

        #endregion

        public override void Bouton1()
        {
            Basic.Quit();
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
            Basic.Quit();
        }
    }
}