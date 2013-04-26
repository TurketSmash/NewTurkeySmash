using Microsoft.Xna.Framework.Audio;
using System.Threading;
using Microsoft.Xna.Framework.Media;
namespace TurkeySmash
{
    class Accueil : Menu
    {
        #region Fields

        private float xPos = 300;
        private float yPos = 200;
        private BoutonTexte bouton1;
        private BoutonTexte bouton2;
        private BoutonTexte bouton3;
        private BoutonTexte bouton4;
        private SoundEffect soundByebye;
        public static string modeDeJeu;

        #endregion

        #region Construction and Initialization

        public Accueil()
        {
            xPos = TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1 = new BoutonTexte(xPos, yPos + 100);
            bouton2 = new BoutonTexte(xPos, yPos + 200);
            bouton3 = new BoutonTexte(xPos, yPos + 300);
            bouton4 = new BoutonTexte(xPos, yPos + 450);
            bouton1.Texte = "Jouer";
            bouton2.Texte = "Multijoueur";
            bouton3.Texte = "Options";
            bouton4.Texte = "Quitter";
        }

        public override void Init()
        {
            Song song = TurkeySmashGame.content.Load<Song>("Sons\\musique1");
            MediaPlayer.Volume = 0.5f;
            MediaPlayer.IsRepeating = true;
            //MediaPlayer.Play(song);
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuPrincipal");
            bouton1.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
            bouton3.Load(TurkeySmashGame.content, boutons);
            bouton4.Load(TurkeySmashGame.content, boutons);
            soundByebye = TurkeySmashGame.content.Load<SoundEffect>("Sons\\byebye");
        }

        #endregion

        public override void Bouton1()
        {
            modeDeJeu = "1j";
            Basic.SetScreen(new SelectionPersonnage());
        }

        public override void Bouton2()
        {
            modeDeJeu = "2j";
            Basic.SetScreen(new SelectionPersonnage());
        }

        public override void Bouton3()
        {
            Basic.SetScreen(new Options());
        }

        public override void Bouton4()
        {
            MediaPlayer.Volume = 0.2f;
            soundByebye.Play();
            Thread.Sleep(620);
            Basic.Quit();
        }
    }
}
