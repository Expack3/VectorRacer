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
    public class ThreeD_ParticleSystem
    {

        public int totalParticles = 100;
        public Vector3 position = Vector3.Zero;
        public Texture2D sprite;
        public Rectangle[] parts;
        public LinkedList<Rectangle> parts2;

        public int current = 0;
        public int end = 100;
        public Boolean started = false;

        private int v_lifetime = -1;
        private Random rnd;

        private Matrix interal_Rotation; //only used if created independently of a 3D model

        public ThreeD_ParticleSystem(int seed)
        {
            position = new Vector3(200, 200, 200);
            Init(seed);
        }

        public ThreeD_ParticleSystem(int seed, Vector3 newPosition, Texture2D texture)
        {
            position = newPosition;
            this.sprite = texture;
            Init(seed);
        }

        public ThreeD_ParticleSystem(int seed, Vector3 newPosition, String textureLocation)
        {
            position = newPosition;
            this.sprite = GameState.content.Load<Texture2D>(textureLocation);
            Init(seed);
        }

        public ThreeD_ParticleSystem(int seed, Vector3 newPosition, String textureLocation, Boolean standaloneSystem, int newLifetime, int maxPolys)
        {
            position = newPosition;
            this.sprite = GameState.content.Load<Texture2D>(textureLocation);
            if(standaloneSystem == true && !(newLifetime <= 0))
                v_lifetime = end = newLifetime;
            totalParticles = maxPolys;
            Init(seed);
        }

        public ThreeD_ParticleSystem(int seed, Vector3 newPosition, String textureLocation, int newLifetime, Matrix rotation)
        {
            position = newPosition;
            this.sprite = GameState.content.Load<Texture2D>(textureLocation);
            v_lifetime = newLifetime;
            interal_Rotation = rotation;
            Init(seed);
        }

        public void Init(int seed)
        {
            parts2 = new LinkedList<Rectangle>();
            rnd = new Random(seed);

        }

        public void Start()
        {
            started = true;
        }

        public void setPosition(Vector3 newPosition)
        {
            position = newPosition;
        }

        public int Lifetime
        {
            get
            {
                return v_lifetime;
            }
            set
            {
                v_lifetime = value;
            }
        }

        public void Update(GameTime gameTime, Matrix rotation)
        {
            if (started)
            {
                if (parts2.Count < totalParticles)
                {
                    parts2.AddFirst(new Rectangle(position, sprite, new Vector2(1.0f, 1.0f), new Vector2(1.0f, 1.0f), false, 0.0f, new Vector3(0.0f, 0.0f, 0.0f)));
                    parts2.First.Value.Velocity = new Vector3(0 + (float)rnd.Next(-50, 50) / 100f,
                    1 + (float)rnd.Next(-100, 100) / 100f, 2 + (float)rnd.Next(-50, 50));
                    parts2.First.Value.Position = position;
                }

                LinkedListNode<Rectangle> node;
                for (node = parts2.First; node != null; node = node.Next)
                {
                    if (node.Value != null)
                    {
                        node.Value.v_Lifetime = node.Value.v_Lifetime + 1.0f;
                        if (node.Value.returnIntegerLifetime() > end)
                        {
                            node.Value = null;
                        }
                        else
                        {
                            node.Value.setVelocityY(9.8f / 50f);
                        }
                    }
                }
                Clean();
            }
        } // End Update()

        public void Update(GameTime gameTime)
        {
            if (started)
            {
                if (parts2.Count < totalParticles)
                {
                    parts2.AddFirst(new Rectangle(position, sprite, new Vector2(1.0f, 1.0f), new Vector2(1.0f, 1.0f), false, 0.0f, new Vector3(0.0f, 0.0f, 0.0f)));
                    parts2.First.Value.alive = true;
                    parts2.First.Value.Velocity = new Vector3(0 + (float)rnd.Next(-25, 25) / 100f,
                    1 + (float)rnd.Next(-50, 50) / 100f, 2 + (float)rnd.Next(-25, 25));
                    parts2.First.Value.Position = position;
                }

                LinkedListNode<Rectangle> node;
                for (node = parts2.First; node != null; node = node.Next)
                {
                    if (node.Value != null && node.Value.alive)
                    {
                        node.Value.v_Lifetime = node.Value.v_Lifetime + 1.0f;
                        if (node.Value.returnIntegerLifetime() > end)
                        {
                            node.Value = null;
                        }
                        else
                        {
                            node.Value.setVelocityY(9.8f / 50f);
                        }
                    }
                }
                Clean();
            }
        }

        public void Draw(GameTime gameTime)
        {
            foreach (Rectangle rec in parts2)
            {
                if (rec != null)
                {

                    rec.Draw(gameTime);
                }
            }
        }

        private void Clean()
        {
            LinkedListNode<Rectangle> node = parts2.First;
            while (node != null)
            {
                if (node.Value == null)
                {
                    LinkedListNode<Rectangle> nextNode = node.Next;
                    parts2.Remove(node);
                    node = nextNode;
                }
                else
                {
                    node = node.Next;
                }
            }
        }
    }
}
