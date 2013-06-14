using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TurkeySmash
{
    public class ParticleEngine
    {
        private Random random;
        public Vector2 Location { get; set; }
        private Particle particle;
        private Texture2D texture;
        private bool matchWithPlayer; 

        /// <summary>
        /// Classe créant et gérant une particule
        /// </summary>
        /// <param name="texture">Texture de la particule</param>
        /// <param name="location">Position centrale de la particule</param>
        /// <param name="velocity">Vitesse de la particule, mettre à null si randomVelocity = true</param>
        /// <param name="color">Couleur de la particule, mettre à null si randomColor = true</param>
        /// <param name="randomVelocity">Movement de la particule géré de façon aléatoire, ne rien mettre si vrai</param>
        /// <param name="randomColor">Couleur de la particule géré de façon aléatoire, ne rien mettre si vrai</param>
        public ParticleEngine(Texture2D texture, Vector2 location, Vector2 velocity, Color color, bool randomVelocity = true, bool matchWithPlayer = false, bool randomColor = true)
        {
            Location = location;
            this.texture = texture;
            random = new Random();
            particle = GenerateNewParticle(velocity, color, randomVelocity, randomColor);
        }

        public void Update(Vector2 position)
        {
            if (particle.TTL < 1)
                particle.Alive = false;
            else
                particle.Update(position, matchWithPlayer);
        }

        private Particle GenerateNewParticle(Vector2 velocity, Color color, bool randomVelocity, bool randomColor)
        {
            if (randomVelocity)
                velocity = new Vector2(1f * (float)(random.NextDouble() * 2 - 1), 1f * (float)(random.NextDouble() * 2 - 1));
            if (randomColor)
                color = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            float angle = 0;
            float angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);
            float size = (float)random.NextDouble();
            int ttl = 20 + random.Next(40);

            return new Particle(texture, Location, velocity, angle, angularVelocity, color, size, ttl);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (particle.Alive)
                particle.Draw(spriteBatch);
        }
    }
}