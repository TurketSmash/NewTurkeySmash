#region Using
using System;
using System.ComponentModel;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;

#endregion

namespace Libraries
{
    public class Screen
    {
        public enum SceneState
        {
            TransitionOn,
            Active,
            TransitionOff,
            Hidden
        }

        public Screen() { }

        public SceneState state = SceneState.Active;
        public virtual void Init() { }
        public virtual void Update(GameTime gameTime, Input input) { }
        public virtual void Render() { }
        public virtual void Quit() { } 
    }
}