using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace TurkeySmash
{
    class Pause : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // Retour
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // Selection Personnages
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // Menu Principal

        private Texte bouton1txt;
        private Texte bouton2txt;
        private Texte bouton3txt;

        #endregion

        #region Construction and Initialization

        public Pause()
        {
            bouton1txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton1txt.Texte = "Retour";

            bouton2txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.6f);
            bouton2txt.Texte = "Selection Personnages";

            bouton3txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.8f);
            bouton3txt.Texte = "Menu Principal";

            bouton1txt.NameFont = bouton2txt.NameFont = bouton3txt.NameFont = "MenuFont";
            texteBoutons.Add(bouton1txt); texteBoutons.Add(bouton2txt); texteBoutons.Add(bouton3txt);

            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenuPause");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Pause");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(350, 100);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.6f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 2", "Menu1\\BoutonOFF Allongé 2", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.3f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.8f);

            foreach (Texte txt in texteBoutons)
            {
                txt.SizeText = 1;
                txt.Load(TurkeySmashGame.content, textes);
            }
        }

        #endregion

        public override void Bouton1()
        {
            MediaPlayer.Pause();
            Basic.Quit();
            MediaPlayer.Pause();
        }

        public override void Bouton2()
        {
            for (int i = 0; i < 4; i++)
                SelectionPersonnage.listPerso[i] = null;
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.SetScreen(new SelectionPersonnage());
        }

        public override void Bouton3()
        {
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
        }
    }
}
