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

        #endregion

        #region Construction and Initialization

        public Resolution()
        {
            xPos = 3 * TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1 = new BoutonTexte(xPos, yPos + 100);
            bouton2 = new BoutonTexte(xPos, yPos + 200);
            bouton3 = new BoutonTexte(xPos, yPos + 300);
            bouton4 = new BoutonTexte(xPos, yPos + 450);
            bouton1.Texte = "1920x1080";
            bouton2.Texte = "1600x900";
            bouton3.Texte = "1280x720";
            bouton4.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuResolution");
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
