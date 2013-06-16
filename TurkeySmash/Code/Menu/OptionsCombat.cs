using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class OptionsCombat : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // Mode de jeu
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // Nombre de vie / timer
        //private BoutonImageMenu bouton3 = new BoutonImageMenu();
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // Retour

        private Texte bouton1txt;
        private Texte bouton2txt;
        private Texte bouton3txt;

        public static string TypePartieSelect = "vie";
        public static int NombreVies = 5;
        public static int TempsPartie = 1;

        #endregion

        #region Construction and Initialization

        public OptionsCombat()
        {

            bouton1txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton1txt.Texte = "Type de partie : Vies";
            texteBoutons.Add(bouton1txt);

            bouton2txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton2txt.Texte = "Nombre de vies: 5";
            texteBoutons.Add(bouton2txt);

            bouton3txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton3txt.Texte = "Retour";
            texteBoutons.Add(bouton3txt);

            bouton1txt.NameFont = bouton2txt.NameFont = bouton3txt.NameFont = "MenuFont";
            bouton1txt.Load(TurkeySmashGame.content, textes); bouton2txt.Load(TurkeySmashGame.content, textes); bouton3txt.Load(TurkeySmashGame.content, textes);
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\FR-OptionsDeJeu");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(630, 100);


            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);

        }

        #endregion


        public override void Bouton1()
        {
            if (TypePartieSelect == "vie")
            {
                TypePartieSelect = "temps";
                bouton1txt.Texte = "Type de Partie : Temps";
                bouton2txt.Texte = "Temps par Partie: " + TempsPartie + " min";
            }
            else
            {
                TypePartieSelect = "vie";
                bouton1txt.Texte = "Type de Partie : Vies";
                bouton2txt.Texte = "Nombre de Vies: " + NombreVies;
            }
        }

        public override void Bouton2()
        {
            if (TypePartieSelect == "vie")
            {
                switch (NombreVies)
                {
                    case 1:
                        NombreVies = 2;
                        bouton2txt.Texte = "Nombre de Vies : 2";
                        break;

                    case 2:
                        NombreVies = 3;
                        bouton2txt.Texte = "Nombre de Vies : 3";
                        break;

                    case 3:
                        NombreVies = 5;
                        bouton2txt.Texte = "Nombre de Vies : 5";
                        break;

                    case 5:
                        NombreVies = 10;
                        bouton2txt.Texte = "Nombre de Vies : 10";
                        break;

                    case 10:
                        NombreVies = 1;
                        bouton2txt.Texte = "Nombre de Vies : 1";
                        break;
                }
            }
            else
            {
                switch (TempsPartie)
                {
                    case 1:
                        TempsPartie = 2;
                        bouton2txt.Texte = "Temps par Partie : 2 min";
                        break;

                    case 2:
                        TempsPartie = 3;
                        bouton2txt.Texte = "Temps par Partie : 3 min";
                        break;

                    case 3:
                        TempsPartie = 5;
                        bouton2txt.Texte = "Temps par Partie : 5 min";
                        break;

                    case 5:
                        TempsPartie = 10;
                        bouton2txt.Texte = "Temps par Partie : 10 min";
                        break;

                    case 10:
                        TempsPartie = 1;
                        bouton2txt.Texte = "Temps par Partie : 1 min";
                        break;
                }
            }

        }

        public override void Bouton3()
        {
            Basic.Quit();
            Basic.SetScreen(new SelectionPersonnage());
        }
    }
}
