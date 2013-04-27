#region Using Statement
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using Libraries;
using FarseerPhysics.Dynamics;
#endregion

namespace TurkeySmash.Code.Main
{
    class Jeu : Screen
    {
        World world;
        private HUD hud = new HUD();
        public static SoundEffect sonEspace = TurkeySmashGame.content.Load<SoundEffect>("Sons\\sonEspace");
        public SoundEffectInstance sonInstance = sonEspace.CreateInstance();

        private Sprite background;
        private StaticPhysicsObject level;
        private Libraries.Sprite levelSprite;
        private Character character;
        private Libraries.Sprite charSprite;
        //Level level;

        public Jeu()
        {
            state = SceneState.Active;
        }

        public override void Init()
        {
            world =new World(Vector2.UnitY * 20f);

            //level = new Level(world, "Jeu\\background", TurkeySmashGame.content);

            background = new Sprite();
            background.Load(TurkeySmashGame.content, "Jeu\\background");
            levelSprite = new Libraries.Sprite();
            levelSprite.Load(TurkeySmashGame.content, "Jeu\\ground");
            level = new StaticPhysicsObject(world, new Vector2(TurkeySmashGame.WindowSize.X/2, TurkeySmashGame.WindowSize.Y - levelSprite.Height) , 1f, levelSprite);
            charSprite = new Libraries.Sprite();
            charSprite.Load(TurkeySmashGame.content, "Jeu\\naruto");
            character = new Character(world, new Vector2(TurkeySmashGame.WindowSize.X/2, TurkeySmashGame.WindowSize.Y/2), 1f, charSprite);

            sonInstance.Volume = 0.5f;
            sonInstance.IsLooped = true;
            sonInstance.Resume();
        }

        public override void Update(GameTime gameTime, Input input)
        {

            if (input.Escape())
            {
                sonInstance.Pause();
                Basic.SetScreen(new Pause());
            }
            character.Update(gameTime);

            //Mise a jour du world en 30 FPS
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
        }


        public override void Render()
        {
            if (state == SceneState.Active)
            {
                TurkeySmashGame.spriteBatch.Begin();

                //level.Draw(TurkeySmashGame.spriteBatch);
                background.DrawAsBackground(TurkeySmashGame.spriteBatch);
                level.Draw(levelSprite, TurkeySmashGame.spriteBatch);
                character.Draw(charSprite, TurkeySmashGame.spriteBatch);

                TurkeySmashGame.spriteBatch.End();
            }
            //hud.Draw(); FIXME
        }
        
    
    
    }
}
