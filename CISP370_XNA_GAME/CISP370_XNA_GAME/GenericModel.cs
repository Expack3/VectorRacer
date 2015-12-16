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
    public abstract class GenericModel
    {
        float lifetime;
        int identifier;
        public GenericModel()
        {
            lifetime = -999.0f;
        }
        public GenericModel(float life)
        {
            lifetime = life;
        }

        public void asignIdentifier(int identify)
        {
            identifier = identify;
        }

        public float Lifetime
        {
            get
            {
                return lifetime;
            }
            set
            {
                lifetime = value;
            }
        }

        public abstract void Update(GameTime gameTime);
        public abstract void Draw(GameTime gameTime);
    }
}
