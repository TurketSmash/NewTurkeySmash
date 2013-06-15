namespace TurkeySmash
{
    class Resolution : Menu
    {
        #region Fields 

        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private BoutonTexte bouton3;
        private BoutonTexte bouton4;
        private float xPos = 350;
        private float yPos = 200;

        private Texte antibug1 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug3 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug4 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);

        #endregion

        #region Construction and Initialization

        public Resolution()
        {
            xPos = 3 * TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton2 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.55f);
            bouton3 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton4 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);
            bouton1.Texte = "1920 x 1080";
            bouton2.Texte = "1600 x 900";
            bouton3.Texte = "1280 x 720";
            bouton4.Texte = "Retour";

            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2); texteBoutons.Add(antibug3); texteBoutons.Add(antibug4);

        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Options");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(400, 120);
            bouton1.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
            bouton3.Load(TurkeySmashGame.content, boutons);
            bouton4.Load(TurkeySmashGame.content, boutons);
        }

        #endregion


        public override void Bouton1()
        {
            TurkeySmashGame.manager.PreferredBackBufferWidth = 1920;
            TurkeySmashGame.manager.PreferredBackBufferHeight = 1080;
            TurkeySmashGame.manager.ApplyChanges();
            Basic.Exit();
            if (Basic.screens.Count - 1 > 3)
            {
                Basic.SetScreen(new Pause());
                Basic.SetScreen(new Options());
                Basic.SetScreen(new Resolution());
            }
            else
            {
                
                Basic.SetScreen(new Options());
                Basic.SetScreen(new Resolution());
            }
        }

        public override void Bouton2()
        {
            TurkeySmashGame.manager.PreferredBackBufferWidth = 1600;
            TurkeySmashGame.manager.PreferredBackBufferHeight = 900;
            TurkeySmashGame.manager.ApplyChanges();
            Basic.Exit();
            if (Basic.screens.Count - 1 > 3)
            {
                Basic.SetScreen(new Pause());
                Basic.SetScreen(new Options());
                Basic.SetScreen(new Resolution());
            }
            else
            {
                
                Basic.SetScreen(new Options());
                Basic.SetScreen(new Resolution());
            }
        }
        
        public override void Bouton3()
        {
            TurkeySmashGame.manager.PreferredBackBufferWidth = 1280;
            TurkeySmashGame.manager.PreferredBackBufferHeight = 720;
            TurkeySmashGame.manager.ApplyChanges();
            Basic.Exit();
            if (Basic.screens.Count - 1 > 3)
            {
                Basic.SetScreen(new Pause());
                Basic.SetScreen(new Options());
                Basic.SetScreen(new Resolution());
            }
            else
            {
                
                Basic.SetScreen(new Options());
                Basic.SetScreen(new Resolution());
            }
        }

        public override void Bouton4()
        {
            Basic.Quit();

        }
    }
}
