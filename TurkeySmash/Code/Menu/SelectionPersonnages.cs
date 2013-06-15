using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TurkeySmash
{
    class SelectionPersonnage : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // Options combats
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // Naruto
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // Sakura
        private BoutonImageMenu bouton4 = new BoutonImageMenu(); // Sai
        private BoutonImageMenu bouton5 = new BoutonImageMenu(); // Suigetsu
        private BoutonImageMenu bouton6 = new BoutonImageMenu(); // Retour

        private Texte bouton1txt;
        private Texte bouton6txt;

        private Texte antibug1 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug3 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug4 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);

        private Texte affichQuiChoisitSonPersonnage;
        public static string[] listPerso = new string[4];
        private int persoAChoisirRestant;
        int i = 0;

        #endregion

        #region Consctruction & initialization

        public SelectionPersonnage()
        {
            persoAChoisirRestant = ChoixNombrePersonnage.nombreJoueur + ChoixNombrePersonnage.nombreIA;

            bouton1txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton1txt.Texte = "Options";
            texteBoutons.Add(bouton1txt);

            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2); texteBoutons.Add(antibug3); texteBoutons.Add(antibug4);

            bouton6txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);
            bouton6txt.Texte = "Retour";
            texteBoutons.Add(bouton6txt);

            affichQuiChoisitSonPersonnage = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.WindowSize.Y * 0.55f);
            if (ChoixNombrePersonnage.nombreJoueur != 0)
                affichQuiChoisitSonPersonnage.Texte = "Joueur 1:";
            else
                affichQuiChoisitSonPersonnage.Texte = "Ordinateur 1:";


            bouton1txt.NameFont = affichQuiChoisitSonPersonnage.NameFont = bouton6txt.NameFont = "MenuFont";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\FR-SelectionDesPersonnages");
            nomMenu.Resize(TurkeySmashGame.manager.PreferredBackBufferWidth);
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(1000, 100);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonNarutoON", "Menu1\\PersoLevel\\BoutonNarutoOFF", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.55f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonSakuraON", "Menu1\\PersoLevel\\BoutonSakuraOFF", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.85f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton4.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonSaiON", "Menu1\\PersoLevel\\BoutonSaiOFF", boutons);
            bouton4.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.55f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton5.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonSuigetsuON", "Menu1\\PersoLevel\\BoutonSuigetsuOFF", boutons);
            bouton5.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.85f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton6.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton6.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);

            bouton1txt.Load(TurkeySmashGame.content, textes); bouton6txt.Load(TurkeySmashGame.content, textes);

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
