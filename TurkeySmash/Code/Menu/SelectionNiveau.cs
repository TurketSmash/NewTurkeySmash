using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class SelectionNiveau : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // level 1
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // level 2
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // level 3
        private BoutonImageMenu bouton4 = new BoutonImageMenu(); // level 4
        private BoutonImageMenu bouton5 = new BoutonImageMenu(); // Retour

        private Texte bouton5txt;
        private Texte antibug1 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug3 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug4 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);

        public static string niveauSelect;

        #endregion

        #region Consctruction & initialization

        public SelectionNiveau()
        {
            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2); texteBoutons.Add(antibug3); texteBoutons.Add(antibug4);
            bouton5txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.88f);
            bouton5txt.Texte = "Retour";
            bouton5txt.NameFont = "MenuFont";
            bouton5txt.SizeText = 1;
            texteBoutons.Add(bouton5txt);
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\FR-SelectionDuNiveau");
            //nomMenu.Resize(TurkeySmashGame.manager.PreferredBackBufferWidth);
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(760, 80);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonLevel1ON", "Menu1\\PersoLevel\\BoutonLevel1OFF", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.35f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonLevel2ON", "Menu1\\PersoLevel\\BoutonLevel2OFF", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.65f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.35f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonLevel3ON", "Menu1\\PersoLevel\\BoutonLevel3OFF", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.65f);
            bouton4.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonLevel4ON", "Menu1\\PersoLevel\\BoutonLevel4OFF", boutons);
            bouton4.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.65f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.65f);
            bouton5.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton5.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.88f);

            bouton5txt.Load(TurkeySmashGame.content, textes);
        }

        public override void Bouton1()
        {
            niveauSelect = "level1";
            MediaPlayer.Pause();
            Basic.SetScreen(new Jeu());
        }

        public override void Bouton2()
        {
            niveauSelect = "level2";
            MediaPlayer.Pause();
            Basic.SetScreen(new Jeu());
        }

        public override void Bouton3()
        {
            niveauSelect = "level3";
            MediaPlayer.Pause();
            Basic.SetScreen(new Jeu());
        }

        public override void Bouton4()
        {
            niveauSelect = "level4";
            MediaPlayer.Pause();
            Basic.SetScreen(new Jeu());
        }

        public override void Bouton5()
        {
            for (int i = 0; i < 4; i++)
                SelectionPersonnage.listPerso[i] = null;
            Basic.Quit();
            Basic.Quit();
            Basic.SetScreen(new SelectionPersonnage());
        }

        #endregion
    }
}
