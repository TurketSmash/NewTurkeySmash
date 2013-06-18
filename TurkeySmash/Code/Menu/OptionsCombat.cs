using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    class OptionsCombat : Menu
    {
        #region Fields

        private BoutonImageMenu bouton1 = new BoutonImageMenu(); // Mode de jeu
        private BoutonImageMenu bouton2 = new BoutonImageMenu(); // Nombre de vie / timer
        private BoutonImageMenu bouton3 = new BoutonImageMenu(); // frequence spawn objet;
        private BoutonImageMenu bouton4 = new BoutonImageMenu(); // Retour

        private Texte bouton1txt;
        private Texte bouton2txt;
        private Texte bouton3txt;
        private Texte bouton4txt;

        public static string TypePartieSelect = "vie";
        public static int NombreVies = 5;
        public static int TempsPartie = 1;
        public static int itemSpawnMin = 10;
        public static int itemSpawnMax = 15;
        public static string frequenceObjetsBonus = "Normal";

        #endregion

        #region Construction and Initialization

        public OptionsCombat()
        {
            if (TypePartieSelect == "vie")
            {
                bouton1txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
                bouton1txt.Texte = Langue.French ? "Type de partie : Vies" : "Game's Mode : Lifes";
                bouton1txt.SizeText = 1;
                texteBoutons.Add(bouton1txt);

                bouton2txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
                bouton2txt.Texte = (Langue.French ? "Nombre de vies : " : "Lifes : ") + NombreVies;
                bouton2txt.SizeText = 1;
                texteBoutons.Add(bouton2txt);
            }
            else
            {
                bouton1txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
                bouton1txt.Texte = Langue.French ? "Type de Partie : Temps" : "Game's Mode : Time";
                bouton1txt.SizeText = 1;
                texteBoutons.Add(bouton1txt);

                bouton2txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
                bouton2txt.Texte = (Langue.French ? "Temps par Partie : " : "Game's Duration : ") + TempsPartie + " min";
                bouton2txt.SizeText = 1;
                texteBoutons.Add(bouton2txt);
            }

            bouton3txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.35f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton3txt.Texte = (Langue.French ? "Objets et bonus : " : "Items and Bonuses : ") + frequenceObjetsBonus;
            bouton3txt.SizeText = 1;
            texteBoutons.Add(bouton3txt);

            bouton4txt = new Texte(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);
            bouton4txt.Texte = Langue.French ? "Retour" : "Back";
            bouton4txt.SizeText = 1;
            texteBoutons.Add(bouton4txt);

            bouton1txt.NameFont = bouton2txt.NameFont = bouton3txt.NameFont = bouton4txt.NameFont = "MenuFont";
            bouton1txt.Load(TurkeySmashGame.content, textes); bouton2txt.Load(TurkeySmashGame.content, textes); bouton3txt.Load(TurkeySmashGame.content, textes); bouton4txt.Load(TurkeySmashGame.content, textes);
        }

        public override void Init()
        {
            backgroundMenu.Load(TurkeySmashGame.content, "Menu1\\fondMenu");
            nomMenu.Load(TurkeySmashGame.content, Langue.French ? "Menu1\\FR-OptionsDeJeu" : "Menu1\\EN-GameOptions");
            nomMenu.Position = new Microsoft.Xna.Framework.Vector2(630, 100);


            bouton1.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton1.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.3f);
            bouton2.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton2.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.5f);
            bouton3.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton3.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.37f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.7f);
            bouton4.Load(TurkeySmashGame.content, "Menu1\\BoutonON Allongé 3", "Menu1\\BoutonOFF Allongé 3", boutons);
            bouton4.Position = new Microsoft.Xna.Framework.Vector2(TurkeySmashGame.manager.PreferredBackBufferWidth * 0.42f, TurkeySmashGame.manager.PreferredBackBufferHeight * 0.9f);

        }

        #endregion


        public override void Bouton1()
        {
            if (TypePartieSelect == "vie")
            {
                TypePartieSelect = "temps";
                bouton1txt.Texte = Langue.French ? "Type de Partie : Temps" : "Game's Mode : Time";
                bouton2txt.Texte = (Langue.French ? "Temps par Partie : " : "Game's Duration : ") + TempsPartie + " min";
            }
            else
            {
                TypePartieSelect = "vie";
                bouton1txt.Texte = Langue.French ? "Type de Partie : Vies": "Game's Mode : Lifes";
                bouton2txt.Texte = (Langue.French ? "Nombre de Vies : " : "Lifes : ") + NombreVies;
            }
        }

        public override void Bouton2()
        {
            if (TypePartieSelect == "vie")
            {
                string gogole = Langue.French ? "Nombre de Vies : " : "Lifes : ";
                switch (NombreVies)
                {
                    case 1:
                        NombreVies = 2;
                        bouton2txt.Texte = gogole + 2;
                        break;

                    case 2:
                        NombreVies = 3;
                        bouton2txt.Texte = gogole +3;
                        break;

                    case 3:
                        NombreVies = 5;
                        bouton2txt.Texte = gogole + 5;
                        break;

                    case 5:
                        NombreVies = 10;
                        bouton2txt.Texte = gogole + 10;
                        break;

                    case 10:
                        NombreVies = 1;
                        bouton2txt.Texte = gogole + 1;
                        break;
                }
            }
            else
            {
                string GOGOOOLE = Langue.French ? "Temps par Partie : " : "Game's Duration : ";
                switch (TempsPartie)
                {
                    case 1:
                        TempsPartie = 2;
                        bouton2txt.Texte = GOGOOOLE +"2 min";
                        break;

                    case 2:
                        TempsPartie = 3;
                        bouton2txt.Texte = GOGOOOLE +"3 min";
                        break;

                    case 3:
                        TempsPartie = 5;
                        bouton2txt.Texte = GOGOOOLE +"5 min";
                        break;

                    case 5:
                        TempsPartie = 10;
                        bouton2txt.Texte = GOGOOOLE +"10 min";
                        break;

                    case 10:
                        TempsPartie = 1;
                        bouton2txt.Texte = GOGOOOLE +"1 min";
                        break;
                }
            }

        }

        public override void Bouton3()
        {
            switch (frequenceObjetsBonus)
            {
                case "Normal":
                    itemSpawnMin = 10;
                    itemSpawnMax = 15;
                    frequenceObjetsBonus = Langue.French ? "Souvent" : "Often";
                    bouton3txt.Texte = Langue.French ? "Objets et bonus : Souvent" : "Items and Bonuses : Often";
                    break;

                case "Souvent":
                case "Often":
                    itemSpawnMin = 90000;
                    itemSpawnMax = 90010;
                    frequenceObjetsBonus = Langue.French ? "Jamais" : "Never";
                    bouton3txt.Texte = Langue.French ? "Objets et bonus : Jamais" : "Items and Bonuses : Never";
                    break;

                case "Jamais":
                case "Never":
                    itemSpawnMin = 30;
                    itemSpawnMax = 40;
                    frequenceObjetsBonus = "Rare";
                    bouton3txt.Texte = Langue.French ? "Objets et bonus : Rare" : "Items and Bonuses : Rare";
                    break;

                case "Rare":
                    itemSpawnMin = 20;
                    itemSpawnMax = 30;
                    frequenceObjetsBonus = "Normal";
                    bouton3txt.Texte = Langue.French ? "Objets et bonus : Normal" : "Items and Bonuses : Normal";
                    break;
            }
        }

        public override void Bouton4()
        {
            Basic.Quit();
            Basic.SetScreen(new SelectionPersonnage());
        }
    }
}
