using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace TurkeySmash
{
    class Pause : Menu
    {
        #region Fields

        private BoutonTexte boutonRetour;
        private BoutonTexte boutonSelectionPerso;
        private BoutonTexte boutonMainMenu;
        private float xPos = 500;
        private float yPos = 200;

        #endregion

        #region Construction and Initialization

        public Pause()
        {
            xPos = TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            boutonRetour = new BoutonTexte(xPos, yPos + 100);
            boutonSelectionPerso = new BoutonTexte(xPos, yPos + 200);
            boutonMainMenu = new BoutonTexte(xPos, yPos + 300);
            boutonRetour.Texte = "Retour";
            boutonSelectionPerso.Texte = "Selection des personnages";
            boutonMainMenu.Texte = "Menu Principal";
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPause");
            boutonRetour.Load(TurkeySmashGame.content, boutons);
            boutonSelectionPerso.Load(TurkeySmashGame.content, boutons);
            boutonMainMenu.Load(TurkeySmashGame.content, boutons);
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
        }
    }
}
