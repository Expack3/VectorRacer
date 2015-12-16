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
    public class Rectangle : GenericModel
    {
        private VertexPositionTexture[] m_vpt;
        private VertexBuffer m_buffer;
        private Texture2D m_texture;
        private BasicEffect m_effect;
        private Vector3 m_position;
        private Matrix m_rotation;
        private float m_scale; //scale of the object
        private float m_originalScale;
        private bool doRotate; //can this object be rotated?; legacy value
        private bool doScale; //can this object be scaled?; legacy value
        private Vector3 m_rotationValues = new Vector3(0.0f, 0.0f, 0.0f);
        private Vector3 velocity = new Vector3(0.0f,0.0f,0.0f); //only used if the rectangle is part of a particle system
        private Boolean useVelocity = false;
        public Boolean alive = false;


        public Rectangle(Vector3 posVector, String texturePosition, Vector2 size, Vector2 UVsize, bool rotate, bool scale, float lifetime)
            : base(lifetime)
        {
            //create the rectangle's vertexes
            m_vpt = new VertexPositionTexture[4];
            m_vpt[0] = new VertexPositionTexture(new Vector3(-size.X, size.Y, 0), new Vector2(0, 0));
            m_vpt[1] = new VertexPositionTexture(new Vector3(size.X, size.Y, 0), new Vector2(UVsize.X, 0));
            m_vpt[2] = new VertexPositionTexture(new Vector3(-size.X, -size.Y, 0), new Vector2(0, UVsize.Y));
            m_vpt[3] = new VertexPositionTexture(new Vector3(size.X, -size.Y, 0), new Vector2(UVsize.X, UVsize.Y));

            m_buffer = new VertexBuffer(GameState.graphicsDevice, typeof(VertexPositionTexture), m_vpt.Length, BufferUsage.None);
            m_buffer.SetData(m_vpt);
            m_texture = GameState.content.Load<Texture2D>(texturePosition);
            m_effect = new BasicEffect(GameState.graphicsDevice);

            Position = posVector;
            m_rotation = Matrix.Identity;
            m_scale = 1.0f;
            doRotate = rotate;
            doScale = scale;
        }

        public Rectangle(Vector3 posVector, String texturePosition, Vector2 size, Vector2 UVsize, bool rotate, bool scale)
            : base()
        {
            //create the rectangle's vertexes
            m_vpt = new VertexPositionTexture[4];
            m_vpt[0] = new VertexPositionTexture(new Vector3(-size.X, size.Y, 0), new Vector2(0, 0));
            m_vpt[1] = new VertexPositionTexture(new Vector3(size.X, size.Y, 0), new Vector2(UVsize.X, 0));
            m_vpt[2] = new VertexPositionTexture(new Vector3(-size.X, -size.Y, 0), new Vector2(0, UVsize.Y));
            m_vpt[3] = new VertexPositionTexture(new Vector3(size.X, -size.Y, 0), new Vector2(UVsize.X, UVsize.Y));

            m_buffer = new VertexBuffer(GameState.graphicsDevice, typeof(VertexPositionTexture), m_vpt.Length, BufferUsage.None);
            m_buffer.SetData(m_vpt);
            m_texture = GameState.content.Load<Texture2D>(texturePosition);
            m_effect = new BasicEffect(GameState.graphicsDevice);

            Position = posVector;
            m_rotation = Matrix.Identity;
            m_scale = 1.0f;
            doRotate = rotate;
            doScale = scale;
        }

        public Rectangle(Vector3 posVector, Texture2D textureRef, Vector2 size, Vector2 UVsize, bool scale, float lifetime, Vector3 velVector)
            : base(lifetime)
        {
            //create the rectangle's vertexes
            m_vpt = new VertexPositionTexture[4];
            m_vpt[0] = new VertexPositionTexture(new Vector3(-size.X, size.Y, 0), new Vector2(0, 0));
            m_vpt[1] = new VertexPositionTexture(new Vector3(size.X, size.Y, 0), new Vector2(UVsize.X, 0));
            m_vpt[2] = new VertexPositionTexture(new Vector3(-size.X, -size.Y, 0), new Vector2(0, UVsize.Y));
            m_vpt[3] = new VertexPositionTexture(new Vector3(size.X, -size.Y, 0), new Vector2(UVsize.X, UVsize.Y));

            m_buffer = new VertexBuffer(GameState.graphicsDevice, typeof(VertexPositionTexture), m_vpt.Length, BufferUsage.None);
            m_buffer.SetData(m_vpt);
            m_texture = textureRef;
            m_effect = new BasicEffect(GameState.graphicsDevice);

            Position = posVector;
            m_rotation = Matrix.Identity;
            m_scale = 1.0f;
            doRotate = false;
            doScale = scale;
            velocity = velVector;
            useVelocity = true;
        }

        public Rectangle(Vector3 posVector, String texturePosition, Vector2 size, Vector2 UVsize, Vector3 rotate, float scale, float lifetime)
            : base(lifetime)
        {
            m_vpt = new VertexPositionTexture[4];
            m_vpt[0] = new VertexPositionTexture(new Vector3(-size.X, size.Y, 0), new Vector2(0, 0));
            m_vpt[1] = new VertexPositionTexture(new Vector3(size.X, size.Y, 0), new Vector2(UVsize.X, 0));
            m_vpt[2] = new VertexPositionTexture(new Vector3(-size.X, -size.Y, 0), new Vector2(0, UVsize.Y));
            m_vpt[3] = new VertexPositionTexture(new Vector3(size.X, -size.Y, 0), new Vector2(UVsize.X, UVsize.Y));

            m_buffer = new VertexBuffer(GameState.graphicsDevice, typeof(VertexPositionTexture), m_vpt.Length, BufferUsage.None);
            m_buffer.SetData(m_vpt);
            m_texture = GameState.content.Load<Texture2D>(texturePosition);
            m_effect = new BasicEffect(GameState.graphicsDevice);

            Position = posVector;
            m_rotation = Matrix.Identity;
            m_originalScale = m_scale = scale;
            if ((rotate.X != 0.0f) || (rotate.Y != 0.0f) || (rotate.Z != 0.0f))
                m_rotationValues = rotate;
        }

        public Rectangle(Vector3 posVector, String texturePosition, Vector2 size, Vector2 UVsize, Vector3 rotate, float scale)
            : base()
        {
            m_vpt = new VertexPositionTexture[4];
            m_vpt[0] = new VertexPositionTexture(new Vector3(-size.X, size.Y, 0), new Vector2(0, 0));
            m_vpt[1] = new VertexPositionTexture(new Vector3(size.X, size.Y, 0), new Vector2(UVsize.X, 0));
            m_vpt[2] = new VertexPositionTexture(new Vector3(-size.X, -size.Y, 0), new Vector2(0, UVsize.Y));
            m_vpt[3] = new VertexPositionTexture(new Vector3(size.X, -size.Y, 0), new Vector2(UVsize.X, UVsize.Y));

            m_buffer = new VertexBuffer(GameState.graphicsDevice, typeof(VertexPositionTexture), m_vpt.Length, BufferUsage.None);
            m_buffer.SetData(m_vpt);
            m_texture = GameState.content.Load<Texture2D>(texturePosition);
            m_effect = new BasicEffect(GameState.graphicsDevice);

            Position = posVector;
            m_rotation = Matrix.Identity;
            m_originalScale = m_scale = scale;
            if ((rotate.X != 0.0f) || (rotate.Y != 0.0f) || (rotate.Z != 0.0f))
                m_rotationValues = rotate;
        }

        public Vector3 Position
        {
            get
            {
                return m_position;
            }
            set
            {
                m_position = value;
            }
        }

        public Vector3 Velocity
        {
            get
            {
                return velocity;
            }
            set
            {
                velocity = value;
            }
        }

        public void setVelocityX(float xValue)
        {
            velocity.X += xValue;
        }

        public void setVelocityY(float yValue)
        {
            velocity.Y += yValue;
        }

        public void setVelocityZ(float zValue)
        {
            velocity.Z += zValue;
        }

        public float v_Lifetime
        {
            get
            {
                return Lifetime;
            }
            set
            {
                Lifetime = value;
            }
        }
        public float returnIntegerLifetime()
        {
            return (int)Lifetime;
        }

        public Matrix Rotation
        {
            get
            {
                return m_rotation;
            }
            set
            {
                m_rotation = value;
            }
        }

        public void movePosition(Vector3 vector)
        {
            m_position = m_position + vector;
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.graphicsDevice.SetVertexBuffer(m_buffer);

            m_effect.World = Matrix.Identity * Matrix.CreateScale(m_scale) * m_rotation * Matrix.CreateTranslation(m_position + velocity); //ISROT = Identity * Scale * ROtate * Translate
            m_effect.View = GameState.camera.View;
            m_effect.Projection = GameState.camera.Projection;
            m_effect.Texture = m_texture;
            m_effect.TextureEnabled = true;

            foreach (EffectPass pass in m_effect.CurrentTechnique.Passes)
            {
                pass.Apply();
                GameState.graphicsDevice.SamplerStates[0] = SamplerState.LinearClamp; //clamps texture to power of 2
                GameState.graphicsDevice.DrawUserPrimitives<VertexPositionTexture>(PrimitiveType.TriangleStrip, m_vpt, 0, 2);
                //NOTE: In the future, make textures with power of 2!!
            }
        }

        public override void Update(GameTime gameTime)
        {
            if (Lifetime != -999.0f && useVelocity == false)
            {
                Lifetime -= 1.0f;
            }
            if (doRotate == true)
            {
                m_rotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / 60.0f);
                m_rotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / 60.0f);
                m_rotation *= Matrix.CreateRotationZ(MathHelper.PiOver4 / 60.0f);
            }
            else if ((m_rotationValues != null) && (GameState.canObjectsRotate == true))
            {
                if (m_rotationValues.X != 0f)
                    m_rotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / m_rotationValues.X);
                if (m_rotationValues.Y != 0f)
                    m_rotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / m_rotationValues.Y);
                if (m_rotationValues.Z != 0f)
                    m_rotation *= Matrix.CreateRotationZ(MathHelper.PiOver4 / m_rotationValues.Z);
            }
        }
    }
}
