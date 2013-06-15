﻿using Microsoft.Xna.Framework;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Media;

namespace TurkeySmash
{
    static class Basic
    {
        public static List<Screen> screens = new List<Screen>();

        public static void SetUp()
        {
            screens.Add(new Jeu()); // debug
            //screens.Add(new Accueil()); // normal
            screens[0].Init();
        }

        public static void Update(GameTime gameTime, Input input)
        {
            screens[screens.Count - 1].Update(gameTime, input);
        }

        public static void Render()
        {
            if (screens.Count > 1)
                screens[screens.Count - 2].Render();
            screens[screens.Count - 1].Render();
        }

        public static void SetScreen(Screen newScreen)
        {
            screens.Add(newScreen);
            screens[screens.Count - 1].Init();
        }

        public static void Quit()
        {
            Song song = TurkeySmashGame.content.Load<Song>("Sons\\musique1");
            MediaPlayer.Resume();
            screens.Remove(screens[screens.Count - 1]);
        }

        public static void Exit()
        {
            screens.Clear();
            Basic.SetScreen(new Accueil());
        }
    }
}
