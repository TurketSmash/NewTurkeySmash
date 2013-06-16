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
        private List<Particle> particles;
        private List<Texture2D> textures;
        private bool matchWithPlayer = false;
        private BlendState blending;

        /// <summary>
        /// Classe créant et gérant une particule
        /// </summary>
        /// <param name="texture">Texture de la particule</param>
        /// <param name="location">Position centrale de la particule</param>
        /// <param name="velocity">Vitesse de la particule, mettre à null si randomVelocity = true</param>
        /// <param name="color">Couleur de la particule, mettre à null si randomColor = true</param>
        /// <param name="randomVelocity">Movement de la particule géré de façon aléatoire, ne rien mettre si vrai</param>
        /// <param name="randomColor">Couleur de la particule géré de façon aléatoire, ne rien mettre si vrai</param>
        public ParticleEngine(List<Texture2D> textures, Vector2 location, Vector2 velocity, Color color, int nomberParticles,
            int dureeDeVie, float size = 1.0f, bool randomSize = true, bool randomVelocity = true, bool matchWithPlayer = false, bool randomColor = true, bool randomAngle = true)
        {
            Location = location;
            blending = BlendState.Additive;
            this.textures = textures;
            this.particles = new List<Particle>();
            random = new Random();
            this.matchWithPlayer = matchWithPlayer;
            
            for (int i = 0; i < nomberParticles; i++)
            {
                particles.Add(GenerateNewParticle(velocity, color, dureeDeVie, size, randomVelocity, randomColor, randomSize, randomAngle));
            }
        }

        public ParticleEngine(Texture2D texture, Vector2 location, Vector2 velocity, Color color, int nomberParticles, BlendState blendState,
            int dureeDeVie, float size = 1.0f, bool randomSize = true, bool randomVelocity = true, bool matchWithPlayer = false, bool randomColor = true, bool randomAngle = true)
        {
            Location = location;
            blending = blendState;
            textures = new List<Texture2D>();
            textures.Add(texture);
            this.particles = new List<Particle>();
            random = new Random();
            this.matchWithPlayer = matchWithPlayer;

            for (int i = 0; i < nomberParticles; i++)
            {
                particles.Add(GenerateNewParticle(velocity, color, dureeDeVie, size, randomVelocity, randomColor, randomSize, randomAngle));
            }
        }

        public void Update(GameTime gameTime, Vector2 position)
        {
            for (int index = 0; index < particles.Count; index++)
            {
                if (particles[index].TTL < 0)
                {
                    particles.RemoveAt(index);
                    index--;
                }
                else
                {
                    particles[index].Update(position, matchWithPlayer);
                    particles[index].TTL -= gameTime.ElapsedGameTime.Milliseconds;
                }
            }
        }

        private Particle GenerateNewParticle(Vector2 velocity, Color color, int dureeDeVie, float size, bool randomVelocity, bool randomColor, bool randomSize, bool randomAngle)
        {
            Texture2D texture = textures[random.Next(textures.Count)];
            if (randomVelocity)
                velocity = new Vector2((float)random.NextDouble() * random.Next(-1, 1), (float)random.NextDouble() * random.Next(-1, 1));
            if (randomColor)
                color = new Color((float)random.NextDouble(), (float)random.NextDouble(), (float)random.NextDouble());
            if (randomSize)
                size = (float)random.NextDouble() * 2;
            float angle = 0;
            float angularVelocity = 0.0f;
            if (randomAngle)
                angularVelocity = 0.1f * (float)(random.NextDouble() * 2 - 1);

            return new Particle(texture, Location, velocity, angle, angularVelocity, color, size, dureeDeVie);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, blending);
            foreach (Particle particle in particles)
                particle.Draw(spriteBatch);
            spriteBatch.End();
            spriteBatch.Begin();
        }
    }
}