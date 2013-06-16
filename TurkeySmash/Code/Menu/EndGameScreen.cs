﻿using System;
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
        private ImageMenu goldFrame;
        private ImageMenu silverFrame;
        private ImageMenu bronzeFrame;
        private ImageMenu bronzeFrame2;

        float Xpos = TurkeySmashGame.WindowSize.X;
        float Ypos = TurkeySmashGame.WindowSize.Y;
        float[] posFrames = new float[4];

        float[] posClassement = new float[4];
        Texte[] classement = new Texte[4];

        float[] posResults = new float[4];
        Texte[] results = new Texte[4];

        float[] posJoueurs = new float[4];
        Texte[] joueurs = new Texte[4];

        float[] posSuicide = new float[4];
        Texte[] suicide = new Texte[4];

        float[] posKills = new float[4];
        Texte[] kills1 = new Texte[4];
        Texte[] kills2 = new Texte[4];
        Texte[] kills3 = new Texte[4];
        Texte[][] kills = new Texte[3][];

        #region Code de porc
        private Texte antibug1 = new Texte(0, 0);
        private Texte antibug2 = new Texte(0, 0);
        #endregion
        #endregion

        #region Construction and Initialization

        public EndGameScreen()
        {
            boutonRejouer = new BoutonTexte(TurkeySmashGame.WindowSize.X / 4, TurkeySmashGame.WindowSize.Y / 10);
            boutonMainMenu = new BoutonTexte(3 * TurkeySmashGame.WindowSize.X / 4, TurkeySmashGame.WindowSize.Y / 10);
            boutonRejouer.Texte = "Rejouer";
            boutonMainMenu.Texte = "Menu Principal";

            #region affichage joueurs
            float YjoueurPos = Ypos * 0.333f;

            for (int i = 0; i < 4; i++)
                if (Results.ResultsBoard[i][1] != -999) // Si il y a 3 joueurs
                {
                    joueurs[i] = new Texte((Xpos / 8) + ((Xpos / 4) * (Results.ResultsBoard[i][0] - 1)), YjoueurPos);
                    joueurs[i].Texte = "Joueur " + Results.ResultsBoard[i][0];
                }

            #endregion
            #region affichage frames
            float YframePos = Ypos * 0.205f;
            posFrames[0] = -5;
            posFrames[1] = posFrames[0] + Xpos / 4;
            posFrames[2] = posFrames[1] + Xpos / 4;
            posFrames[3] = posFrames[2] + Xpos / 4;

            goldFrame = new ImageMenu(posFrames[Results.ResultsBoard[0][0]/* = num du joueur qui est 1er au classement*/ - 1], YframePos);
            silverFrame = new ImageMenu(posFrames[Results.ResultsBoard[1][0] - 1], YframePos);
            if(Results.ResultsBoard[2][1] != -999) // Si il y a 3 joueurs
                bronzeFrame = new ImageMenu(posFrames[Results.ResultsBoard[2][0] - 1], YframePos);

            if (Results.ResultsBoard[3][1] != -999) // Si il y a 4 joueurs
                bronzeFrame2 = new ImageMenu(posFrames[Results.ResultsBoard[3][0] - 1], YframePos);
            #endregion  
            #region affichage classementTxt

            float YclassPos = Ypos * 0.455f;
            posClassement[0] = Xpos * 0.155f;
            posClassement[1] = posClassement[0] + Xpos / 4;
            posClassement[2] = posClassement[1] + Xpos / 4;
            posClassement[3] = posClassement[2] + Xpos / 4;

            classement[0] = new Texte(posClassement[Results.ResultsBoard[0][0] - 1], YclassPos);
            classement[0].Texte = "1er";

            classement[1] = new Texte(posClassement[Results.ResultsBoard[1][0] - 1], YclassPos);
            classement[1].Texte = "2nd";

            if (Results.ResultsBoard[2][1] != -999) // Si il y a 3 joueurs
            {
                classement[2] = new Texte(posClassement[Results.ResultsBoard[2][0] - 1], YclassPos);
                classement[2].Texte = "3e";
            }

            if (Results.ResultsBoard[3][1] != -999) // Si il y a 4 joueurs
            {
                classement[3] = new Texte(posClassement[Results.ResultsBoard[3][0] - 1], YclassPos);
                classement[3].Texte = "4e";
            }
            #endregion
            #region affichage results
            float YresultPos = Ypos * 0.565f;
            posResults[0] = Xpos * 0.130f;
            posResults[1] = posResults[0] + Xpos/4;
            posResults[2] = posResults[1] + Xpos / 4;
            posResults[3] = posResults[2] + Xpos / 4;
            
            for (int i = 0; i < 4; i++)
                if (Results.ResultsBoard[i][1] != -999)
                {
                    results[i] = new Texte(posResults[Results.ResultsBoard[i][0] - 1], YresultPos);
                    results[i].Texte = "Score : " + Results.ResultsBoard[i][1];
                }
            #endregion
            #region affichage suicides

            float YautokilltPos = Ypos * 0.657f;
            posSuicide[0] = Xpos * 0.130f;
            posSuicide[1] = posSuicide[0] + Xpos / 4;
            posSuicide[2] = posSuicide[1] + Xpos / 4;
            posSuicide[3] = posSuicide[2] + Xpos / 4;

            for (int i = 0; i < 4; i++)
                if (Results.ResultsBoard[i][1] != -999)
                {
                    suicide[i] = new Texte(posSuicide[Results.ResultsBoard[i][0] - 1], YautokilltPos);
                    suicide[i].Texte = "Suicide : " + Results.ResultsBoard[i][2];
                }
            #endregion
            #region affichage kills
            float YkillsPos = Ypos * 0.730f;
            posKills[0] = Xpos * 0.130f;
            posKills[1] = posKills[0] + Xpos / 4;
            posKills[2] = posKills[1] + Xpos / 4;
            posKills[3] = posKills[2] + Xpos / 4;
            kills[0] = kills1;
            kills[1] = kills2;
            kills[2] = kills3;

            for (int i = 0; i < 4; i++)
            {
                int con = 0;
                for (int j = 0; j < 3; j++)
                {
                    if (Results.ResultsBoard[i][1] != -999 & Results.ResultsBoard[j + 1][1] != -999)
                    {
                        int n = j + 1;
                        if ((Results.ResultsBoard[i][0]) == n)
                            con = 1;
                        n += con;
                        kills[j][i] = new Texte(posKills[Results.ResultsBoard[i][0] - 1], Ypos * (0.730f + (0.092f * j)));
                        kills[j][i].Texte = "Joueur " + n + " tues : " + Results.ResultsBoard[i][3 + (n - 1)];
                    }
                }
            }
            #endregion
            #region Code de porc
            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2);
            #endregion
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\Scores\\fondMenuFin");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Scores\\Scores");
            nomMenu.Position = new Vector2(TurkeySmashGame.WindowSize.X / 2, nomMenu.Height / 2 + 10);
            boutonRejouer.Load(TurkeySmashGame.content, boutons);
            boutonMainMenu.Load(TurkeySmashGame.content, boutons);

            #region affichage frames
            goldFrame.Load(TurkeySmashGame.content, "Menu1\\Scores\\or", ImagesMenu);
            silverFrame.Load(TurkeySmashGame.content, "Menu1\\Scores\\argent", ImagesMenu);

            if (Results.ResultsBoard[2][1] != -999) // Si il y a 3 joueurs
                bronzeFrame.Load(TurkeySmashGame.content, "Menu1\\Scores\\bronze", ImagesMenu);
            if (Results.ResultsBoard[3][1] != -999) // Si il y a 4 joueurs
                bronzeFrame2.Load(TurkeySmashGame.content, "Menu1\\Scores\\bronze", ImagesMenu);
            #endregion 
            
            for (int i = 0; i < 4; i++)
            {
                if (joueurs[i] != null)
                {
                    joueurs[i].NameFont = "pourcent";
                    joueurs[i].Load(TurkeySmashGame.content, textes);
                }
                if (classement[i] != null)
                {
                    classement[i].NameFont = "pourcent";
                    classement[i].SizeText = 2f;
                    classement[i].Load(TurkeySmashGame.content, textes);
                }
                if (results[i] != null)
                {
                    results[i].NameFont = "pourcent";
                    results[i].Load(TurkeySmashGame.content, textes);
                    results[i].SizeText = 1.1f;
                }
                if (suicide[i] != null)
                {
                    suicide[i].SizeText = 1.1f;
                    suicide[i].NameFont = "pourcent";
                    suicide[i].Load(TurkeySmashGame.content, textes);
                }
                for (int j = 0; j < 3;j++ )
                    if (kills[j][i] != null)
                    {
                        kills[j][i].NameFont = "pourcent";
                        kills[j][i].Load(TurkeySmashGame.content, textes);
                        kills[j][i].SizeText = 0.7f;
                    }
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
