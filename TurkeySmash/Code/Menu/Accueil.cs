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


        private BoutonImageMenu bouton1 = new BoutonImageMenu();
        private BoutonImageMenu bouton2 = new BoutonImageMenu();
        private BoutonImageMenu bouton3 = new BoutonImageMenu();
        private BoutonImageMenu bouton4 = new BoutonImageMenu();
        private Texte jouer;
        private Texte options;
        private Texte aides;
        private Texte quitter;

        private SoundEffect soundByebye;

        #endregion

        #region Construction and Initialization

        public Accueil()
        {
            xPos = TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;

            jouer = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            jouer.Texte = Langue.French ? "Jouer" : "Play";
            texteBoutons.Add(jouer);

            options = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            options.Texte = "Options";
            texteBoutons.Add(options);

            aides = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            aides.Texte =Langue.French ? "Aides" : "Help" ;
            texteBoutons.Add(aides);

            quitter = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.25f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);
            quitter.Texte =Langue.French ? "Quitter" : "Quit";
            texteBoutons.Add(quitter);

            jouer.NameFont = options.NameFont = aides.NameFont = quitter.NameFont = "MenuFont";
        }

        public override void Init()
        {
            Song song = TurkeySmashGame.content.Load<Song>("Sons\\Musiques\\MusicMenu");
            MediaPlayer.Volume = 0.35f;
            MediaPlayer.IsRepeating = true;
            MediaPlayer.Play(song);
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, Langue.French ? "Menu1\\FR-MenuPrincipal" : "Menu1\\EN-MainMenu");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(730, 120);

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
                txt.SizeText = 1.0f;
                txt.Load(TurkeySmashGame.content, textes);
            }

            soundByebye = TurkeySmashGame.content.Load<SoundEffect>("Sons\\byebye");
        }

        #endregion

        public override void Bouton1()
        {
            Basic.SetScreen(new ChoixNombrePersonnage());
        }

        public override void Bouton2()
        {
            Basic.SetScreen(new Options());
        }

        public override void Bouton3()
        {
            Basic.SetScreen(new Aides());
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
