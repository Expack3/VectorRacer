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
    public class Player
    {
        private String m_name;
        private Vector3 m_position;
        private float m_radius;
        private int m_health;

        public Player(String name, Vector3 position, float height)
        {
            m_name = name;
            m_position = position;
            m_radius = (float)(height / 2.0);
            m_health = 100;
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

        public BoundingSphere boundingSphere
        {
            get
            {
                BoundingSphere bs = new BoundingSphere();
                bs.Center = m_position;
                bs.Radius = m_radius;

                return bs;

                //return new BoundingSphere(m_position, m_radius);
            }
        }
    }
}

