#region Using Statement
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Libraries;
#endregion

namespace TurkeySmash
{
    class Multi : Screen
    {
        #region Fields

        private Sprite background;
        private Camera camera;
        private List<AnimatedModel> elements = new List<AnimatedModel>();
        private GameTime gameTime;
        private HUD hud = new HUD();
        public static SoundEffect sonEspace = TurkeySmashGame.content.Load<SoundEffect>("Sons\\sonEspace");
        public SoundEffectInstance sonInstance = sonEspace.CreateInstance();

        #endregion

        #region Construction and Initialization

        public Multi()
        {
            gameTime = new GameTime();
            state = SceneState.Active;
        }

        public override void Init()
        {
            camera = new Camera(TurkeySmashGame.manager);
            background = new Sprite();
            //players.Add(new Joueur(PlayerIndex.One));
            //players.Add(new IA(PlayerIndex.Two));
            background.Load(TurkeySmashGame.content, "Jeu\\space");
            //players[0].Load(TurkeySmashGame.content, "Models\\dude", elements);
            //players[0].Size = new Vector2(50, 375);
            //players[1].Load(TurkeySmashGame.content, "Models\\dude", elements);
            //players[1].Size = new Vector2(80, 400);
            //level.Load(TurkeySmashGame.content, "Models\\farm");
            hud.Load(elements);
            camera.Initialize();

            sonInstance.Volume = 0.5f;
            sonInstance.IsLooped = true;
            sonInstance.Resume();
        }

        #endregion

        #region Update

        public override void Update(GameTime gameTime, Input input)
        {
            camera.Update(TurkeySmashGame.manager.GraphicsDevice, gameTime);
            //level.Update();
            //hud.Update(elements);
            sonInstance.Resume();

            //
            // fin de partie
            // si les joueurs sont morts, alors le dernier est vainqueurs et la partie est terminée.
            // les joueurs sont stockés dans une liste : players de type Player 
            // la mort du personnage se vérifié grace à la Propriété "Mort" (bool).
            //

            if (input.Escape())
            {
                sonInstance.Pause();
                Basic.SetScreen(new Pause());
            }
        }

        #endregion

        #region Drawing

        public override void Render()
        {
            if (state == SceneState.Active)
            {
                TurkeySmashGame.manager.GraphicsDevice.BlendState = BlendState.Opaque; //rendre les textures opaques
                TurkeySmashGame.manager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                //level.Draw(camera);
                //foreach (AnimatedModel element in elements)
                //{
                //    element.Draw(camera);
                //}

                hud.Draw();
            }
        }

        #endregion
    }
}
