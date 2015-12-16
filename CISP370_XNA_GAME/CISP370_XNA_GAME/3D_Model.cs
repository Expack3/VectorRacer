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
    public class _3D_Model : GenericModel
    {
        private Model m_model;
        private String m_name;
        private Matrix[] m_boneTransforms;
        private Vector3 m_position;
        private Matrix m_rotation;
        private float m_scale; //scale of the object
        private float m_originalScale;
        private Vector3 m_rotationValues = new Vector3(0.0f, 0.0f, 0.0f);
        private Effect vectorEffect;
        private Texture2D texture;
        private ThreeD_ParticleSystem p_system;
        private ThreeD_ParticleSystem p_system2;
        private Boolean useParticleSystem = false;
        private SpriteBatch spriteBatch = new SpriteBatch(GameState.graphicsDevice);

        public _3D_Model(String Name, Model model, Vector3 position, Vector3 rotation, float scale, float lifetime)
            : base(lifetime)
        {
            m_name = Name;
            m_model = model;
            m_position = position;
            m_rotation = Matrix.Identity;
            m_originalScale = m_scale = scale;
            if ((rotation.X != 0.0f) || (rotation.Y != 0.0f) || (rotation.Z != 0.0f))
                m_rotationValues = rotation;

            m_boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(m_boneTransforms);
            switch (Name.ToLower())
            {
                case "powerup":
                    texture = GameState.content.Load<Texture2D>("Models/Sphere_Texture2");
                    break;
                case "obstacle":
                    texture = GameState.content.Load<Texture2D>("Models/O_tex");
                    break;
                case "enemy":
                    texture = GameState.content.Load<Texture2D>("Models/Pyramid_tex");
                    break;
            }
            vectorEffect = GameState.content.Load<Effect>("Shaders/VectorEmulation");
        }

        public _3D_Model(String Name, Model model, Vector3 position, Vector3 rotation, float scale, float lifetime, Boolean useParticles)
            : base(lifetime)
        {
            m_name = Name;
            m_model = model;
            m_position = position;
            m_rotation = Matrix.Identity;
            m_originalScale = m_scale = scale;
            if ((rotation.X != 0.0f) || (rotation.Y != 0.0f) || (rotation.Z != 0.0f))
                m_rotationValues = rotation;

            m_boneTransforms = new Matrix[model.Bones.Count];
            model.CopyAbsoluteBoneTransformsTo(m_boneTransforms);
            switch (Name.ToLower())
            {
                case "powerup":
                    texture = GameState.content.Load<Texture2D>("Models/Sphere_Texture2");
                    break;
                case "obstacle":
                    texture = GameState.content.Load<Texture2D>("Models/O_tex");
                    break;
                case "enemy":
                    texture = GameState.content.Load<Texture2D>("Models/Pyramid_tex");
                    break;
            }
            vectorEffect = GameState.content.Load<Effect>("Shaders/VectorEmulation");
            if (useParticles == true)
            {
                useParticleSystem = useParticles;
                p_system = new ThreeD_ParticleSystem(55555, position, GameState.content.Load<Texture2D>("Textures/squareWhite"));
                p_system2 = new ThreeD_ParticleSystem(44444, position, GameState.content.Load<Texture2D>("Textures/square"));
                p_system.Start();
                p_system2.Start();
            }
        }

        public BoundingSphere boundingSphere
        {
            get
            {
                BoundingSphere sphere = new BoundingSphere();

                foreach (ModelMesh mesh in m_model.Meshes)
                {
                    sphere = mesh.BoundingSphere;
                    sphere = sphere.Transform(m_boneTransforms[mesh.ParentBone.Index] * Matrix.Identity * Matrix.CreateScale(m_scale) * m_rotation * Matrix.CreateTranslation(m_position)); //ISROT = Identity * Scale * ROtate * Translate);
                }

                return sphere;
            }
        }

        public Model getModel()
        {
            return m_model;
        }

        public void setModel(Model model)
        {
            m_model = model;
        }

        public String getName()
        {
            return m_name;
        }

        public void setName(String name)
        {
            m_name = name;
        }

        public Vector3 getPosition()
        {
            return m_position;
        }

        public Matrix[] getBoneTransforms()
        {
            return m_boneTransforms;
        }

        public override void Update(GameTime gameTime)
        {
            if (Lifetime != -999.0f)
            {
                Lifetime -= 1.0f;
            }
            if ((m_rotationValues != null) && (GameState.canObjectsRotate == true))
            {
                if (m_rotationValues.X != 0f)
                    m_rotation *= Matrix.CreateRotationX(MathHelper.PiOver4 / m_rotationValues.X);
                if (m_rotationValues.Y != 0f)
                    m_rotation *= Matrix.CreateRotationY(MathHelper.PiOver4 / m_rotationValues.Y);
                if (m_rotationValues.Z != 0f)
                    m_rotation *= Matrix.CreateRotationZ(MathHelper.PiOver4 / m_rotationValues.Z);
            }
            if (useParticleSystem == true)
            {
                p_system.position = p_system.position * (MathHelper.PiOver4 / m_rotationValues.X);
                p_system.position = p_system.position * (MathHelper.PiOver4 / m_rotationValues.Y);
                p_system.position = p_system.position * (MathHelper.PiOver4 / m_rotationValues.Z);
                p_system.Update(gameTime, m_rotation);
                p_system2.position = m_position;
                p_system2.Update(gameTime, m_rotation);
            }
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.graphicsDevice.BlendState = BlendState.AlphaBlend;
            GameState.graphicsDevice.DepthStencilState = DepthStencilState.Default;
            //GameState.graphicsDevice.RenderState.DepthBufferWriteEnable = true;
            GameState.graphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            Matrix[] transforms = new Matrix[m_model.Bones.Count];
            m_model.CopyAbsoluteBoneTransformsTo(transforms);

            RasterizerState backupState = GameState.game.GraphicsDevice.RasterizerState;

            GameState.game.GraphicsDevice.RasterizerState = RasterizerState.CullNone;

            foreach (ModelMesh mesh in m_model.Meshes)
            {
                foreach (ModelMeshPart part in mesh.MeshParts)
                {
                    part.Effect = vectorEffect;
                    vectorEffect.Parameters["World"].SetValue(m_boneTransforms[mesh.ParentBone.Index] * Matrix.Identity * Matrix.CreateScale(m_scale) * m_rotation * Matrix.CreateTranslation(m_position)); //ISROT = Identity * Scale * Rotation * Translate
                    vectorEffect.Parameters["View"].SetValue(GameState.camera.View);
                    vectorEffect.Parameters["Projection"].SetValue(GameState.camera.Projection);
                    vectorEffect.Parameters["xTexture"].SetValue(texture);
                }
                mesh.Draw();
            }
            if (useParticleSystem == true)
            {
                p_system.Draw(gameTime);
                p_system2.Draw(gameTime);
            }
            GameState.game.GraphicsDevice.RasterizerState = backupState;
        }
    }
}
