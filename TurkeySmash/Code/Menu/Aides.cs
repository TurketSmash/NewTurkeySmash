using Microsoft.Xna.Framework.Audio;
using System.Threading;
using Microsoft.Xna.Framework.Media;
namespace TurkeySmash
{
    class Aides : Menu
    {
        #region Fields

        private int compteurNbPages = 0;
        private BoutonImageMenu bouton1 = new BoutonImageMenu();
        private Texte continuRetour;
        private string nomImage = "Menu1\\Aides-manette xbox";

        #endregion

        #region Construction and Initialization

        public Aides()
        {
            continuRetour = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);
            continuRetour.Texte = "Continuer";
            texteBoutons.Add(continuRetour);
            aidesImages = new Sprite();

            continuRetour.NameFont = "MenuFont";
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, "Menu1\\FR-Aides");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(280, 100);
            aidesImages.Load(TurkeySmashGame.content, nomImage);
            aidesImages.Position = new Microsoft.Xna.Framework.Vector2(900, 450);

            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON", "Menu1\\BoutonOFF", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.2f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.85f);

            continuRetour.Load(TurkeySmashGame.content, textes);
            continuRetour.SizeText = 1.0f;

        }

        #endregion

        public override void Bouton1()
        {
            if (compteurNbPages == 1)
                Basic.Quit();
            else
            {
                if (compteurNbPages == 0)
                {
                    nomImage = "Menu1\\Aides-controlsPC";
                    aidesImages.Load(TurkeySmashGame.content, nomImage);
                    compteurNbPages++;
                    continuRetour.Texte = "Quitter";
                }
            }
        }
    }
}
