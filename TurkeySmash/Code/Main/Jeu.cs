#region Using Statement
using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Audio;
using FarseerPhysics.Dynamics;
#endregion

namespace TurkeySmash
{
    class Jeu : Screen
    {
        World world;
        HUD hud = new HUD();
        public static SoundEffect sonEspace = TurkeySmashGame.content.Load<SoundEffect>("Sons\\sonEspace");
        public SoundEffectInstance sonInstance = sonEspace.CreateInstance();
        Character character;
        Sprite charSprite;
        Character[] personnages = new Character[4];
        Level level;

        public Jeu()
        {
            state = SceneState.Active;
        }

        public override void Init()
        {
            world =new World(Vector2.UnitY * 20f);

            charSprite = new Sprite();
            charSprite.Load(TurkeySmashGame.content, "Jeu\\naruto");
            personnages[0] = new Character(world, new Vector2(TurkeySmashGame.WindowSize.X/2, TurkeySmashGame.WindowSize.Y/2), 1f, charSprite);
            level = new Level(world, "Jeu\\background", "Jeu\\ground", personnages, TurkeySmashGame.content);
            //level = new StaticPhysicsObject(world, new Vector2(TurkeySmashGame.WindowSize.X / 2, TurkeySmashGame.WindowSize.Y - levelSprite.Height), 1f, levelSprite);

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
            level.Update(gameTime);

            //Mise a jour du world en 30 FPS
            world.Step(Math.Min((float)gameTime.ElapsedGameTime.TotalSeconds, (1f / 30f)));
        }


        public override void Render()
        {
            if (state == SceneState.Active)
            {
                TurkeySmashGame.spriteBatch.Begin();

                level.Draw(TurkeySmashGame.spriteBatch);

                TurkeySmashGame.spriteBatch.End();
            }
            //hud.Draw(); FIXME
        }
        
    
    
    }
}
