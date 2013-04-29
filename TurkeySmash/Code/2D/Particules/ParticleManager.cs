using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    public class ParticlesMgr
    {
        private readonly ParticleSettings settings;
        private Texture2D texture;
        private readonly List<Particle> particles;
        public Vector2 Pos;
        private double elapsed;

        public ParticlesMgr(ParticleSettings settings)
        {
            this.settings = settings;
            particles = new List<Particle>();
            for (int i = 0; i < settings.Max; i++)
                particles.Add(new Particle(this));
        }

        public ParticleSettings Settings { get { return settings; } }

        protected void Load()
        {
            texture = TurkeySmashGame.content.Load<Texture2D>("smoke");
        }

        public void Update(GameTime gameTime)
        {
            elapsed += gameTime.ElapsedGameTime.TotalMilliseconds;
            foreach (var particle in particles)
                particle.Update(gameTime);

            int nb = settings.Max - particles.Count(p => p.Alive);
            int add = settings.ParticlesPerAdd == 0 ? nb : settings.ParticlesPerAdd;

            if (settings.ParticlesPerAdd != 0 && nb < settings.ParticlesPerAdd)
                return;

            if (settings.AddFrequence == 0)
                for (int i = 0; i < add; i++)
                    particles.Find(p => !p.Alive).Reset();
            else
                if(elapsed > settings.AddFrequence && nb > 0)
                {
                    foreach (var particle in particles.Where(p => !p.Alive).Take(add))
                        particle.Reset();
                    elapsed = 0;
                }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach (var particle in particles)
                particle.Draw(spriteBatch, texture);
        }
    }
}
