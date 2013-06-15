#region Using
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
#endregion
namespace TurkeySmash
{
    class OptionsSon : Menu
    {
        #region Fields

        private BoutonTexte bouton1MOINS;
        private BoutonTexte bouton1PLUS;
        private BoutonTexte bouton1Change;
        private BoutonTexte bouton2;
        private float xPos = 350;
        private float yPos = 300;

        private Texte antibug1 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug2 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug3 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
        private Texte antibug4 = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);

        Song song2 = TurkeySmashGame.content.Load<Song>("Sons\\musique2");
        Song song3 = TurkeySmashGame.content.Load<Song>("Sons\\Halo");
        bool aDejaChangéGROSBULLSHITDeNikeurDeControleur = false;

        #endregion

        #region Construction and Initialization

        public OptionsSon()
        {
            xPos = 3 * TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1MOINS = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.45f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton1PLUS = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.55f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.4f);
            bouton1Change = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.55f);
            bouton2 = new BoutonTexte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.5f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton1MOINS.Texte = "Vol-";
            bouton1PLUS.Texte = "Vol+";
            bouton1Change.Texte = "Change de musique";
            bouton2.Texte = "Retour";

            texteBoutons.Add(antibug1); texteBoutons.Add(antibug2); texteBoutons.Add(antibug3); texteBoutons.Add(antibug4);
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\Options");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(400, 120);
            bouton1MOINS.Load(TurkeySmashGame.content, boutons);
            bouton1PLUS.Load(TurkeySmashGame.content, boutons);
            bouton1Change.Load(TurkeySmashGame.content, boutons);
            bouton2.Load(TurkeySmashGame.content, boutons);
        }

        #endregion




        public override void Bouton1()
        {
            MediaPlayer.Volume = MediaPlayer.Volume - 0.2f;
        }

        public override void Bouton2()
        {
            MediaPlayer.Volume = MediaPlayer.Volume + 0.2f;
        }

        public override void Bouton3()
        {
            if (aDejaChangéGROSBULLSHITDeNikeurDeControleur == false)
            {
                MediaPlayer.Play(song2);
                aDejaChangéGROSBULLSHITDeNikeurDeControleur = true;
            }
            else
                MediaPlayer.Play(song3);

        }

        public override void Bouton4()
        {
            Basic.Quit();
        }
    }
}
