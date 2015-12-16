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

namespace CISP370_XNA_GAME
{
    public class ParticleSystem
    {

        public int totalParticles = 50;
        public Vector2 position = Vector2.Zero;
        public Texture2D sprite;
        public Particle[] parts;

        public int current = 0;
        public int end = 60;
        public Boolean started = false;
        public Texture2D internal_Texture; //used only if part of a particle manager
        
        public int Lifetime = -999;

        public ParticleSystem(int seed)
        {
            position = new Vector2(200, 200);
            Init(seed);
        }

        public ParticleSystem(int seed, Vector2 newPosition)
        {
            position = newPosition;
            Init(seed);
        }

        public ParticleSystem(int seed, Vector2 newPosition, String spriteLocation, int Lifetime)
        {
            sprite = GameState.content.Load<Texture2D>(spriteLocation);
            position = newPosition;
            Init(seed);
        }

        public void Init(int seed)
        {
            Random rnd = new Random(seed);
            parts = new Particle[totalParticles];

            for (int i = 0; i < totalParticles; i++)
            {
                parts[i] = new Particle();

                parts[i].velocity = new Vector2(0 + (float)rnd.Next(-100, 100) / 100f,
                    3 + (float)rnd.Next(-200, 200) / 100f);
            }

        }

        public void Start(Texture2D texture)
        {
            started = true;
            if(texture != null)
                this.sprite = texture;
        }

        public void setPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public void Update(GameTime gameTime)
        {
            if (started)
            {
                current++;
                if (current < totalParticles)
                {
                    parts[current].alive = true;
                    parts[current].position = position;
                }

                for (int i = 0; i < totalParticles; i++)
                {
                    if (parts[i].alive)
                    {
                        parts[i].lifeTime++;

                        if (parts[i].lifeTime > end)
                            parts[i].alive = false;
                        else
                        {
                            parts[i].velocity.Y += 9.8f / 50f;
                            parts[i].position = parts[i].position + parts[i].velocity;
                        }
                    }
                }
            }
        } // End Update()

        public void Draw(SpriteBatch sb)
        {
            for (int i = 0; i < totalParticles; i++)
            {
                if (parts[i].alive)
                {
                    Color c = Color.White;
                    c.A = (byte)((1 - (parts[i].lifeTime / (float)end)) * 255f);

                    sb.Draw(sprite, parts[i].position, null, c, 0f, new Vector2(12, 12),
                        (parts[i].lifeTime / (float)end) / 1.0f, SpriteEffects.None, 0);

                }
            }
        }
    }
}
