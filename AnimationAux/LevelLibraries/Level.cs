#region Using Statement

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

#endregion

namespace Libraries
{
    public class Level
    {
        private Vector3[] spawnPoints = new Vector3[5];
        private Vector3 positionRespawn = new Vector3(0, 1100, 0);
        private Rectangle cadreDecor = new Rectangle(-3500, 2500, 7000, -5000);
        private Vector3 modelPosition = new Vector3(0, 0, 0);

        public Sprite Background { get; set; }
        public Vector3 Position { get { return modelPosition; } set { modelPosition = value; } }

    }
}
