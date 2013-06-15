using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework;

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
        private Texte[] affichagejoueurs = new Texte[4];
        private Color colorTexte;

        #endregion

        #region Construction and Initialization

        public EndGameScreen()
        {
            boutonRejouer = new BoutonTexte(TurkeySmashGame.WindowSize.X / 4, 9 * TurkeySmashGame.WindowSize.Y / 10);
            boutonMainMenu = new BoutonTexte(3 * TurkeySmashGame.WindowSize.X / 4, 9 * TurkeySmashGame.WindowSize.Y / 10);
            boutonRejouer.Texte = "Rejouer";
            boutonMainMenu.Texte = "Menu Principal";


            if (SelectionNiveau.niveauSelect == "level2")
                colorTexte = Color.White;
            else
                colorTexte = Color.White;

            if (Results.ResultsBoard[0][1] != -999)
            {
                affichagejoueur1 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2);
                affichagejoueur1.Texte = "J" + Results.ResultsBoard[0][0] + " / score : " + Results.ResultsBoard[0][1] + " / suicides : " + Results.ResultsBoard[0][2] +
                    " / details : (" + Results.ResultsBoard[0][3] + "/" + Results.ResultsBoard[0][4] + "/" + Results.ResultsBoard[0][5] + "/" + Results.ResultsBoard[0][6] + ")";
            }
            if (Results.ResultsBoard[1][1] != -999)
            {
                affichagejoueur2 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2 + 50);
                affichagejoueur2.Texte = "J" + Results.ResultsBoard[1][0] + " / score : " + Results.ResultsBoard[1][1] + " / suicides : " + Results.ResultsBoard[1][2] +
                    " / details : (" + Results.ResultsBoard[1][3] + "/" + Results.ResultsBoard[1][4] + "/" + Results.ResultsBoard[1][5] + "/" + Results.ResultsBoard[1][6] + ")";
            }
            if (Results.ResultsBoard[2][1] != -999)
            {
                affichagejoueur3 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2 + 100);
                affichagejoueur3.Texte = "J" + Results.ResultsBoard[2][0] + " / score : " + Results.ResultsBoard[2][1] + " / suicides : " + Results.ResultsBoard[2][2] +
                    " / details : (" + Results.ResultsBoard[2][3] + "/" + Results.ResultsBoard[2][4] + "/" + Results.ResultsBoard[2][5] + "/" + Results.ResultsBoard[2][6] + ")";
            }
            if (Results.ResultsBoard[3][1] != -999)
            {
                affichagejoueur4 = new Texte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y / 2 + 150);
                affichagejoueur4.Texte = "J" + Results.ResultsBoard[3][0] + " / score : " + Results.ResultsBoard[3][1] + " / suicides : " + Results.ResultsBoard[3][2] +
                    " / details : (" + Results.ResultsBoard[3][3] + "/" + Results.ResultsBoard[3][4] + "/" + Results.ResultsBoard[3][5] + "/" + Results.ResultsBoard[3][6] + ")";
            }
            affichagejoueurs[0] = affichagejoueur1;
            affichagejoueurs[1] = affichagejoueur2;
            affichagejoueurs[2] = affichagejoueur3;
            affichagejoueurs[3] = affichagejoueur4;
            for (int i = 0; i < Results.ResultsBoard.Length; i++)
            {
                if (Results.ResultsBoard[i][1] != -999)
                    affichagejoueurs[i].Color = colorTexte;
            }
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuGameOver");
            boutonRejouer.Load(TurkeySmashGame.content, boutons);
            boutonMainMenu.Load(TurkeySmashGame.content, boutons);

            for (int i = 0; i < Results.ResultsBoard.Length; i++)
            {

                if (Results.ResultsBoard[i][1] != -999)
                    affichagejoueurs[i].Load(TurkeySmashGame.content, textes);
            }
        }

        #endregion

        public override void Bouton1()
        {
            for (int i = 0; i < 4; i++)
                SelectionPersonnage.listPerso[i] = null;
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.SetScreen(new SelectionPersonnage());
        }

        public override void Bouton2()
        {
            for (int i = 0; i < 4; i++)
                SelectionPersonnage.listPerso[i] = null;
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
        }
    }
}
