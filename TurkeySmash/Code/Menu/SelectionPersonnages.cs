using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurkeySmash
{
    class SelectionPersonnage : Menu
    {
        #region Fields

        private BoutonTexte boutonOptions;
        private BoutonImage boutonNaruto;
        private BoutonImage boutonSakura;
        private BoutonImage boutonSai;
        private BoutonImage boutonSuigetsu;
        private BoutonTexte boutonRetour;
        private Texte affichQuiChoisitSonPersonnage;
        public static string[] listPerso = new string[4];
        private int persoAChoisirRestant;
        int i = 0;

        #endregion

        #region Consctruction & initialization

        public SelectionPersonnage()
        {
            persoAChoisirRestant = ChoixNombrePersonnage.nombreJoueur + ChoixNombrePersonnage.nombreIA;
            boutonOptions = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.25f);
            boutonOptions.Texte = "Options de Combat";
            boutonNaruto = new BoutonImage();
            boutonSakura = new BoutonImage();
            boutonSai = new BoutonImage();
            boutonSuigetsu = new BoutonImage();
            boutonRetour = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.WindowSize.Y * 0.85f);
            boutonRetour.Texte = "Retour";
            affichQuiChoisitSonPersonnage = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.WindowSize.Y * 0.55f);
            if (ChoixNombrePersonnage.nombreJoueur != 0)
                affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
            else
                affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            boutonOptions.Load(TurkeySmashGame.content, boutons);

            boutonNaruto.Load(TurkeySmashGame.content, "Menu\\BoutonNaruto", boutons);
            boutonNaruto.Position = new Microsoft.Xna.Framework.Vector2(4 * TurkeySmashGame.manager.PreferredBackBufferWidth / 10, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.45f);            
            boutonSakura.Load(TurkeySmashGame.content, "Menu\\BoutonSakura", boutons);
            boutonSakura.Position = new Microsoft.Xna.Framework.Vector2(7 * TurkeySmashGame.manager.PreferredBackBufferWidth / 10, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.45f);            
            boutonSai.Load(TurkeySmashGame.content, "Menu\\BoutonSai", boutons);
            boutonSai.Position = new Microsoft.Xna.Framework.Vector2(4 * TurkeySmashGame.manager.PreferredBackBufferWidth / 10, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.75f);
            boutonSuigetsu.Load(TurkeySmashGame.content, "Menu\\BoutonSuigetsu", boutons);
            boutonSuigetsu.Position = new Microsoft.Xna.Framework.Vector2(7 * TurkeySmashGame.manager.PreferredBackBufferWidth / 10, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.75f);
           
            boutonRetour.Load(TurkeySmashGame.content, boutons);
            affichQuiChoisitSonPersonnage.Load(TurkeySmashGame.content, textes);
        }

        public override void Bouton1()
        {
            i = 0;
            if (ChoixNombrePersonnage.nombreJoueur != 0)
                affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
            else
                affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
            Basic.Quit();
            Basic.SetScreen(new OptionsCombat());
        }

        public override void Bouton2()
        {
            listPerso[i] = "naruto";
            i = i + 1;
            persoAChoisirRestant = persoAChoisirRestant - 1;
            if (persoAChoisirRestant == 0)
            {
                i = 0;
                if (ChoixNombrePersonnage.nombreJoueur != 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
                Basic.SetScreen(new SelectionNiveau());
            }
            else
                if (ChoixNombrePersonnage.nombreJoueur - i > 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur " + (i + 1) + ":";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur " + (i + 1 - ChoixNombrePersonnage.nombreJoueur) + ":";


        }

        public override void Bouton3()
        {
            listPerso[i] = "sakura";
            i = i + 1;
            persoAChoisirRestant = persoAChoisirRestant - 1;
            if (persoAChoisirRestant == 0)
            {
                i = 0;
                if (ChoixNombrePersonnage.nombreJoueur != 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
                Basic.SetScreen(new SelectionNiveau());
            }
            else
                if (ChoixNombrePersonnage.nombreJoueur - i > 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur " + (i + 1) + ":";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur " + (i + 1 - ChoixNombrePersonnage.nombreJoueur) + ":";
        }

        public override void Bouton4()
        {
            listPerso[i] = "sai";
            i = i + 1;
            persoAChoisirRestant = persoAChoisirRestant - 1;
            if (persoAChoisirRestant == 0)
            {
                i = 0;
                if (ChoixNombrePersonnage.nombreJoueur != 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
                Basic.SetScreen(new SelectionNiveau());
            }
            else
                if (ChoixNombrePersonnage.nombreJoueur - i > 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur " + (i + 1) + ":";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur " + (i + 1 - ChoixNombrePersonnage.nombreJoueur) + ":";
        }

        public override void Bouton5()
        {
            listPerso[i] = "suigetsu";
            i = i + 1;
            persoAChoisirRestant = persoAChoisirRestant - 1;
            if (persoAChoisirRestant == 0)
            {
                i = 0;
                if (ChoixNombrePersonnage.nombreJoueur != 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
                Basic.SetScreen(new SelectionNiveau());
            }
            else
                if (ChoixNombrePersonnage.nombreJoueur - i > 0)
                    affichQuiChoisitSonPersonnage.Texte = "Joueur " + (i + 1) + ":";
                else
                    affichQuiChoisitSonPersonnage.Texte = "Ordinateur " + (i + 1 - ChoixNombrePersonnage.nombreJoueur) + ":";
        }

        public override void Bouton6()
        {
            i = 0;
            if (ChoixNombrePersonnage.nombreJoueur != 0)
                affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
            else
                affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";
            Basic.Quit();
        }

        #endregion
    }
}
