using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class SelectionNiveau : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // level 1
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // level 2
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // Retour

        private Texte bouton3txt;
        private Texte antibug1 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);

        public static string niveauSelect;

        #endregion

        #region Consctruction & initialization

        public SelectionNiveau()
        {
            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2);
            bouton3txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);
            bouton3txt.Texte = "Retour";
            bouton3txt.NameFont = "MenuFont";
            texteBoutons.Add(bouton3txt);
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\FR-SelectionDuNiveau");
            nomMenu.Resize(TurkeySmashGame.manager.PreferredBackBufferWidth);
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(1000, 100);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonLevel1ON", "Menu1\\PersoLevel\\BoutonLevel1OFF", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\PersoLevel\\BoutonLevel2ON", "Menu1\\PersoLevel\\BoutonLevel2OFF", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.65f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);

            bouton3txt.Load(TurkeySmashGame.content, textes);
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
            for (int i = 0; i < 4; i++)
                SelectionPersonnage.listPerso[i] = null;
            Basic.Quit();
            Basic.Quit();
            Basic.SetScreen(new SelectionPersonnage());
        }

        #endregion
    }
}
