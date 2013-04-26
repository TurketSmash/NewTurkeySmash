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

        private StaticPhysicsObject level;
        private Libraries.Sprite levelSprite;
        private Character character;

        public Jeu()
        {
            state = SceneState.Active;
        }

        public override void Init()
        {
            world =new World(Vector2.UnitY * 100f);

            levelSprite = new Libraries.Sprite();
            levelSprite.Load(TurkeySmashGame.content, "level");
            level = new StaticPhysicsObject(world, new Vector2((float)TurkeySmashGame.WindowSize.X/2, (float)TurkeySmashGame.WindowSize.Y/2), 1f, levelSprite);
            
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
        }


        public override void Render()
        {
            if (state == SceneState.Active)
            {
            }
            //hud.Draw(); FIXME
        }
        
    
    
    }
}
