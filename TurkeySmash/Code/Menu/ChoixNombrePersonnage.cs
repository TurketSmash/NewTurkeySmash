using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class ChoixNombrePersonnage : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // nombre de joueur
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // nombre d'IA
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // Continuer
        private BoutonImageMenu bouton4 = new BoutonImageMenu(); // Retour

        private Texte bouton1txt;
        private Texte bouton2txt;
        private Texte bouton3txt;
        private Texte bouton4txt;

        public static int nombreJoueur;
        public static int nombreIA;
        

        #endregion

        #region Construction and Initialization

        public ChoixNombrePersonnage()
        {
            nombreJoueur = 1;
            nombreIA = 1;

            bouton1txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton1txt.Texte = "Nombre de joueur : " + nombreJoueur;
            texteBoutons.Add(bouton1txt);

            bouton2txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton2txt.Texte = "Nombre d'IA : " + nombreIA;
            texteBoutons.Add(bouton2txt);

            bouton3txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton3txt.Texte = "Continuer";
            texteBoutons.Add(bouton3txt);

            bouton4txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);
            bouton4txt.Texte = "Retour";
            texteBoutons.Add(bouton4txt);

            bouton1txt.NameFont = bouton2txt.NameFont = bouton3txt.NameFont = bouton4txt.NameFont = "MenuFont";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\FR-SelectionDesPersonnages");
            nomMenu.Resize(TurkeySmashGame.manager.PreferredBackBufferWidth);
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(1000, 100);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton4.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton4.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);

            foreach (Texte txt in texteBoutons)
                txt.Load(TurkeySmashGame.content, textes);
        }

        #endregion


        public override void Bouton1()
        {
            if (nombreJoueur + nombreIA < 4)
            {
                nombreJoueur = nombreJoueur + 1;
                bouton1txt.Texte = "Nombre de Joueur : " + nombreJoueur;
            }
            else
            {
                nombreJoueur = 0;
                bouton1txt.Texte = "Nombre de Joueur : " + nombreJoueur;
            }
        }

        public override void Bouton2()
        {
            if (nombreJoueur + nombreIA < 4)
            {
                nombreIA = nombreIA + 1;
                bouton2txt.Texte = "Nombre d'IA : " + nombreIA;
            }
            else
            {
                nombreIA = 0;
                bouton2txt.Texte = "Nombre d'IA : " + nombreIA;
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
