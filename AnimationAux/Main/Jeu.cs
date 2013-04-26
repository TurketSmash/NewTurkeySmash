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
    class Jeu : Screen
    {
        #region Fields

        private Camera camera;
        private AnimatedModel model;
        private AnimatedModel walk;
        private Level level;
        private List<Personnage> players = new List<Personnage>();
        private List<AnimatedModel> elements = new List<AnimatedModel>();
        private HUD hud = new HUD();
        public static SoundEffect sonEspace = TurkeySmashGame.content.Load<SoundEffect>("Sons\\sonEspace");
        public SoundEffectInstance sonInstance = sonEspace.CreateInstance();

        #endregion

        #region Construction and Initialization

        public Jeu()
        {
            state = SceneState.Active;
        }

        public override void Init()
        {
            camera = new Libraries.Camera(TurkeySmashGame.manager);

            #region player1
            model = new Joueur(PlayerIndex.One, MathHelper.ToRadians(-90), MathHelper.ToRadians(180));
            model.Load("Models\\dude", TurkeySmashGame.content);
            walk = new AnimatedModel();
            walk.Load("Models\\dude-walk", TurkeySmashGame.content);
            AnimationClip clip = walk.Clips[0];
            AnimationPlayer player = model.PlayClip(clip);
            player.Looping = true;
            model.Size = new Vector2(50, 350);

            elements.Add(model);
            #endregion

            if (SelectionNiveau.niveauSelect == "spacefarm")
                level = new Level("Jeu\\space", "Models\\farm", elements, TurkeySmashGame.content);
            else
                level = new Level("Jeu\\citybackground", "Models\\MapCity2", elements, TurkeySmashGame.content);

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

            level.Update(gameTime);
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
                level.Draw(TurkeySmashGame.spriteBatch, TurkeySmashGame.manager.GraphicsDevice, camera);
                TurkeySmashGame.manager.GraphicsDevice.BlendState = BlendState.Opaque; 
                TurkeySmashGame.manager.GraphicsDevice.DepthStencilState = DepthStencilState.Default;

                foreach (AnimatedModel element in elements)
                {
                    element.Draw(TurkeySmashGame.manager.GraphicsDevice, camera);
                }

                hud.Draw();
            }
        }

        #endregion
    }
}
