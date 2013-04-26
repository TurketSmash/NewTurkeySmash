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

        private Font bouton1;
        private BoutonTexte bouton1MOINS;
        private BoutonTexte bouton1PLUS;
        private BoutonTexte bouton2;
        private float xPos = 350;
        private float yPos = 300;

        #endregion

        #region Construction and Initialization

        public OptionsSon()
        {
            xPos = 3 * TurkeySmashGame.manager.PreferredBackBufferWidth / 4;
            yPos = TurkeySmashGame.manager.PreferredBackBufferHeight / 4;
            bouton1 = new Font(xPos, yPos + 100);
            bouton1MOINS = new BoutonTexte(xPos - 75, yPos + 100);
            bouton1PLUS = new BoutonTexte(xPos + 75, yPos + 100);
            bouton2 = new BoutonTexte(xPos, yPos + 200);
            bouton1MOINS.Texte = "Vol-";
            bouton1PLUS.Texte = "Vol+";
            bouton2.Texte = "Retour";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu\\MenuOption");
            bouton1MOINS.Load(TurkeySmashGame.content, boutons);
            bouton1PLUS.Load(TurkeySmashGame.content, boutons);
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
            Basic.Quit();
        }
    }
}
