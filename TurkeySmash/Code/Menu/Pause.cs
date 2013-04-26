using Microsoft.Xna.Framework.Media;
using Microsoft.Xna.Framework.Audio;
namespace TurkeySmash
{
    class Pause : Menu
    {
        #region Fields

        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private BoutonTexte bouton3;
        private float xPos = 500;
        private float yPos = 200;

        #endregion

        #region Construction and Initialization

        public Pause()
        {
            xPos = TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1 = new BoutonTexte(xPos, yPos + 100);
            bouton2 = new BoutonTexte(xPos, yPos + 200);
            bouton3 = new BoutonTexte(xPos, yPos + 300);
            bouton1.Texte = "Retour";
            bouton2.Texte = "Selection des personnages";
            bouton3.Texte = "Menu Principal";
            MediaPlayer.Resume();
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPause");
            bouton1.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
            bouton3.Load(TurkeySmashGame.content, boutons);
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
            Basic.Quit();
            Basic.Quit();
            Basic.Quit();
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
