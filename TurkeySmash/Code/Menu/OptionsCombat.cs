using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class OptionsCombat : Menu
    {
        #region Fields

        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private BoutonTexte bouton3;
        private BoutonTexte bouton4;
        public static string TypePartieSelect = "vie";
        public static int NombreVies = 5;
        public static int TempsPartie = 1;

        #endregion

        #region Construction and Initialization

        public OptionsCombat()
        {
            bouton1 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 0.25f + TurkeySmashGame.WindowSize.Y/4);
            bouton2 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 0.5f + TurkeySmashGame.WindowSize.Y / 4);
            bouton3 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 0.75f + TurkeySmashGame.WindowSize.Y / 4);
            bouton4 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f + TurkeySmashGame.WindowSize.Y / 4);
            if(TypePartieSelect == "temps")

                bouton1.Texte = "Type de Partie : Temps";
            else
                bouton1.Texte = "Type de Partie : Vies";

            bouton2.Texte = "Nombre de Vies : 5";
            bouton3.Texte = "Temps de la Partie : 3 min";
            bouton4.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            bouton1.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
            bouton3.Load(TurkeySmashGame.content, boutons);
            bouton4.Load(TurkeySmashGame.content, boutons);
        }

        #endregion


        public override void Bouton1()
        {
            if (bouton1.Texte == "Type de Partie : Vies")
            {
                TypePartieSelect = "temps";
                bouton1.Texte = "Type de Partie : Temps";
            }
            else
            {
                TypePartieSelect = "vie";
                bouton1.Texte = "Type de Partie : Vies";
            }
        }

        public override void Bouton2()
        {
            if (NombreVies == 5)
            {
                NombreVies = 10;
                bouton2.Texte = "Nombre de Vies : 10";
            }
            else if (NombreVies == 10)
            {
                NombreVies = 5;
                bouton2.Texte = "Nombre de Vies : 5";
            }
        }

        public override void Bouton3()
        {
            if (TempsPartie == 3)
            {
                TempsPartie = 5;
                bouton3.Texte = "Temps de la Partie : 5 min";
            }
            else if (TempsPartie == 5)
            {
                TempsPartie = 3;
                bouton3.Texte = "Temps de la Partie : 3 min";
            }
        }

        public override void Bouton4()
        {
            Basic.Quit();
        }
    }
}
