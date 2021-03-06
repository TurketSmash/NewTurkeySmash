﻿using Microsoft.Xna.Framework;
namespace TurkeySmash
{
    class Resolution : Menu
    {
        #region Fields

        private BoutonTexte bouton0;
        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private BoutonTexte bouton3;
        private BoutonTexte bouton4;
        private float xPos = 350;
        private float yPos = 200;

        private Texte antibug1 = new Texte(TurkeySmashGame.WindowSize.X * 0.2f, TurkeySmashGame.WindowSize.Y * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.WindowSize.X * 0.2f, TurkeySmashGame.WindowSize.Y * 0.3f);
        private Texte antibug3 = new Texte(TurkeySmashGame.WindowSize.X * 0.2f, TurkeySmashGame.WindowSize.Y * 0.3f);
        private Texte antibug4 = new Texte(TurkeySmashGame.WindowSize.X * 0.2f, TurkeySmashGame.WindowSize.Y * 0.3f);
        private Texte antibug5 = new Texte(TurkeySmashGame.WindowSize.X * 0.2f, TurkeySmashGame.WindowSize.Y * 0.3f);

        #endregion

        #region Construction and Initialization

        public Resolution()
        {
            xPos = 3 * TurkeySmashGame.WindowSize.X / 4;
            yPos = TurkeySmashGame.WindowSize.Y / 4;
            bouton0 = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.25f);
            bouton1 = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.4f);
            bouton2 = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.55f);
            bouton3 = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.7f);
            bouton4 = new BoutonTexte(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.85f);
            bouton0.Texte = Langue.French ? "Plein Ecran" : "Full Screen";
            bouton1.Texte = "1920 x 1080";
            bouton2.Texte = "1600 x 900";
            bouton3.Texte = "1280 x 720";
            bouton4.Texte = Langue.French ? "Retour" : "Back";

            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2); texteBoutons.Add(antibug3); texteBoutons.Add(antibug4); texteBoutons.Add(antibug5);

        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Options");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(400, 120);
            bouton0.Load(TurkeySmashGame.content, boutons);
            bouton1.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
            bouton3.Load(TurkeySmashGame.content, boutons);
            bouton4.Load(TurkeySmashGame.content, boutons);
        }

        #endregion


        public override void Bouton1()
        {
            TurkeySmashGame.manager.ToggleFullScreen();
        }

        public override void Bouton2()
        {
            TurkeySmashGame.manager.PreferredBackBufferWidth = 1920;
            TurkeySmashGame.manager.PreferredBackBufferHeight = 1080;
            TurkeySmashGame.manager.ApplyChanges();
            bouton0.SizeText = 1.3f;
            bouton1.SizeText = 1.3f;
            bouton2.SizeText = 1.3f;
            bouton3.SizeText = 1.3f;
            bouton4.SizeText = 1.3f;
            bouton0.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.25f);
            bouton1.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.4f);
            bouton2.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.55f);
            bouton3.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.7f);
            bouton4.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.85f);
        }

        public override void Bouton3()
        {
            TurkeySmashGame.manager.PreferredBackBufferWidth = 1600;
            TurkeySmashGame.manager.PreferredBackBufferHeight = 900;
            TurkeySmashGame.manager.ApplyChanges();
            bouton0.SizeText = 1;
            bouton1.SizeText = 1;
            bouton2.SizeText = 1;
            bouton3.SizeText = 1;
            bouton4.SizeText = 1;
            bouton0.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.25f);
            bouton1.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.4f);
            bouton2.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.55f);
            bouton3.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.7f);
            bouton4.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.85f);
        }

        public override void Bouton4()
        {
            TurkeySmashGame.manager.PreferredBackBufferWidth = 1280;
            TurkeySmashGame.manager.PreferredBackBufferHeight = 720;
            TurkeySmashGame.manager.ApplyChanges();
            bouton0.SizeText = 0.8f;
            bouton1.SizeText = 0.8f;
            bouton2.SizeText = 0.8f;
            bouton3.SizeText = 0.8f;
            bouton4.SizeText = 0.8f;
            bouton0.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.25f);
            bouton1.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.4f);
            bouton2.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.55f);
            bouton3.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.7f);
            bouton4.Position = new Vector2(TurkeySmashGame.WindowSize.X * 0.5f, TurkeySmashGame.WindowSize.Y * 0.85f);
        }

        public override void Bouton5()
        {
            Basic.Quit();
        }
    }
}
