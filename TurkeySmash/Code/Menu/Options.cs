namespace TurkeySmash
{
    class Options : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu();
        private BoutonImageMenu bouton2 = new BoutonImageMenu();
        private BoutonImageMenu bouton3 = new BoutonImageMenu();
        private BoutonImageMenu bouton4 = new BoutonImageMenu();
        private Texte son;
        private Texte affichage;
        private Texte langue;
        /*
        private Texte resolution;
        private Texte pleinEcran;*/
        private Texte retour;
        private float xPos = 350;
        private float yPos = 300;

        #endregion

        #region Construction and Initialization

        public Options()
        {
            xPos = 3 * TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;

            son = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            son.Texte = Langue.French ? "Son" : "Sound";
            texteBoutons.Add(son);

            affichage = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            affichage.Texte = Langue.French ? "Affichage" : "Display";
            texteBoutons.Add(affichage);

            langue = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            langue.Texte = Langue.French ? "Langue" : "Language";
            texteBoutons.Add(langue);

            retour = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);
            retour.Texte = Langue.French ? "Retour" : "Back";
            texteBoutons.Add(retour);

            son.NameFont = affichage.NameFont = langue.NameFont = retour.NameFont = "MenuFont";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Options");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(400, 120);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton4.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton4.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);

            foreach (Texte txt in texteBoutons)
            {
                txt.SizeText = 1f;
                txt.Load(TurkeySmashGame.content, textes);
            }
        }

        #endregion


        public override void Bouton1()
        {
            Basic.SetScreen(new OptionsSon());
        }

        public override void Bouton2()
        {
            Basic.SetScreen(new Resolution());
        }

        public override void Bouton3()
        {
            Basic.SetScreen(new Langue());
        }

        public override void Bouton4()
        {
            Basic.Quit();
        }
    }
}