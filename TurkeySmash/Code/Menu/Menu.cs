#region Using
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
using Libraries;
#endregion


namespace TurkeySmash
{
    class Menu : Screen
    {
        #region Fields

        protected List<IBouton> boutons = new List<IBouton>();
        protected Sprite backgroundMenu = new Sprite();
        private int selecty = 1;
        private KeyboardState oldStateK;
        private GamePadState oldStateG;
        private SoundEffect soundSelect;
        private SoundEffect soundEnter;

        #endregion

        public Menu() { }

        public override void Update(GameTime gameTime, Input input)
        {

            soundSelect = TurkeySmashGame.content.Load<SoundEffect>("Sons\\menuSelect");
            SoundEffectInstance instanceSelect = soundSelect.CreateInstance();
            instanceSelect.Volume = 0.3f;
            instanceSelect.Pan = -0.9f;
            instanceSelect.Pitch = 0.9f;
            KeyboardState newStateK = Keyboard.GetState();
            GamePadState newStateG = GamePad.GetState(PlayerIndex.One);

            if ((oldStateK.IsKeyUp(Keys.Down) && newStateK.IsKeyDown(Keys.Down))
                            || (oldStateG.DPad.Down == ButtonState.Released && newStateG.DPad.Down == ButtonState.Pressed))
            {
                selecty++;
                instanceSelect.Play();
            }
            if (oldStateK.IsKeyUp(Keys.Up) && newStateK.IsKeyDown(Keys.Up)
                            || (oldStateG.DPad.Up == ButtonState.Released && newStateG.DPad.Up == ButtonState.Pressed))
            {
                selecty--;
                instanceSelect.Play();
            }

            if (selecty > boutons.Count)
                selecty = 1;

            if (selecty < 1)
                selecty = boutons.Count;

            foreach (IBouton bouton in boutons)
            {
                bouton.Etat = false;
            }
            boutons[selecty - 1].Etat = true;

            if (input.Enter() || GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
            {
                Thread.Sleep(200);

                soundEnter = TurkeySmashGame.content.Load<SoundEffect>("Sons\\latch_1");
                SoundEffectInstance instanceEnter = soundEnter.CreateInstance();
                instanceEnter.Volume = 0.2f;
                instanceEnter.Play();

                switch (selecty)
                {
                    case 1:
                        Bouton1();
                        break;
                    case 2:
                        Bouton2();
                        break;
                    case 3:
                        Bouton3();
                        break;
                    case 4:
                        Bouton4();
                        break;
                }
            }

            oldStateK = newStateK;
            oldStateG = newStateG;
        }

        public override void Render()
        {
            TurkeySmashGame.spriteBatch.Begin();

            backgroundMenu.Resize(TurkeySmashGame.manager.PreferredBackBufferWidth);
            backgroundMenu.Draw(TurkeySmashGame.spriteBatch);
            foreach (IBouton bouton in boutons)
            {
                bouton.Draw(TurkeySmashGame.spriteBatch);
            }

            TurkeySmashGame.spriteBatch.End();
        }

        public virtual void Bouton1() { }
        public virtual void Bouton2() { }
        public virtual void Bouton3() { }
        public virtual void Bouton4() { }
    }
}
