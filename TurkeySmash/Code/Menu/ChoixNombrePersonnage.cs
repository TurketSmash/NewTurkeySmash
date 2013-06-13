using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class ChoixNombrePersonnage : Menu
    {
        #region Fields

        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private BoutonTexte bouton3;
        private BoutonTexte bouton4;
        public static int nombreJoueur = 1;
        public static int nombreIA = 1;
        

        #endregion

        #region Construction and Initialization

        public ChoixNombrePersonnage()
        {
            bouton1 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 0.25f + TurkeySmashGame.WindowSize.Y / 4);
            bouton2 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 0.5f + TurkeySmashGame.WindowSize.Y / 4);
            bouton3 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 0.75f + TurkeySmashGame.WindowSize.Y / 4);
            bouton4 = new BoutonTexte(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y * 0.5f * 1f + TurkeySmashGame.WindowSize.Y / 4);
            
            bouton1.Texte = "Nombre de Joueur : 1";
            bouton2.Texte = "Nombre d'ordinateur : 1";
            bouton3.Texte = "Continuer";
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
            if (nombreJoueur + nombreIA < 4)
            {
                nombreJoueur = nombreJoueur + 1;
                bouton1.Texte = "Nombre de Joueur : " + nombreJoueur;
            }
            else
            {
                nombreJoueur = 0;
                bouton1.Texte = "Nombre de Joueur : " + nombreJoueur;
            }
        }

        public override void Bouton2()
        {
            if (nombreJoueur + nombreIA < 4)
            {
                nombreIA = nombreIA + 1;
                bouton2.Texte = "Nombre d'ordinateur : " + nombreIA;
            }
            else
            {
                nombreIA = 0;
                bouton2.Texte = "Nombre d'ordinateur : " + nombreIA;
            }
        }

        public override void Bouton3()
        {
            if (nombreJoueur + nombreIA > 1)
                Basic.SetScreen(new SelectionPersonnage());
        }

        public override void Bouton4()
        {
            Basic.Quit();
        }

    }
}
