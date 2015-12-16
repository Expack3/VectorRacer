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
    public class Camera
    {
        private Matrix m_view;
        private Matrix m_projection;
        private Matrix m_rotation;
        private float m_scale;

        private const int NEAR_PLANE = 1;
        private const int FAR_PLANE = 10000;

        private float m_speed; //how fast the camera is moving
        private Vector3 m_position; //the camera's current position
        private Vector3 m_originalPosition;
        private Vector3 m_lookat; //the camera's current target
        private Vector3 m_originalLookat;
        private Vector3 m_up;
        private float target_distance;

        public Camera(Vector3 position, Vector3 target, Vector3 up, float speed, float targetDis)
        {
            m_originalPosition = m_position = position;
            m_originalLookat = m_lookat = target;
            m_up = up;
            m_speed = speed;
            m_scale = 1.0f;
            m_rotation = Matrix.Identity;

            m_view = Matrix.CreateLookAt(m_position, m_lookat, m_up);
            m_projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.PiOver4, GameState.graphicsDevice.Viewport.AspectRatio, NEAR_PLANE, FAR_PLANE);
            target_distance = targetDis;
        }

        public Matrix View
        {
            get
            {
                return m_view;
            }
        }

        public Matrix Projection
        {
            get
            {
                return m_projection;
            }
        }

        public Vector3 Lookat
        {
            get
            {
                return m_lookat;
            }
            set
            {
                m_lookat = value;
            }
        }

        public float TargetDistance
        {
            get
            {
                return target_distance;
            }
            set
            {
                target_distance = value;
            }
        }

        public void setLookatX(float value)
        {
            m_lookat.X = value;
        }

        public void setLookatY(float value)
        {
            m_lookat.Y = value;
        }

        public void setLookatZ(float value)
        {
            m_lookat.Z = value;
        }

        public float Speed
        {
            get
            {
                return m_speed;
            }
            set
            {
                m_speed = value;
            }
        }

        public Vector3 getPosition()
        {
        return m_position;
        }

        public void setPosition(Vector3 value)
        {
                m_position = value;
        }

        public void setPosX(float value)
        {
            m_position.X = value;
        }

        public void setPosY(float value)
        {
            m_position.Y = value;
        }

        public void setPosZ(float value)
        {
            m_position.Z = value;
        }

        public Vector3 getOriginalPos()
        {
            return m_originalPosition;
        }

        public Vector3 getOriginalLookat()
        {
            return m_originalLookat;
        }

        public void setScale(float x)
        {
            m_scale = x;
        }

        public void Update(GameTime gameTime)
        {
            m_view = Matrix.CreateLookAt(m_position, m_lookat, m_up);
        }
    }
}
