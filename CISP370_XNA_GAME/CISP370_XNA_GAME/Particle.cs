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
    public class Particle
    {
        public Boolean alive = false;
        public Vector2 position = Vector2.Zero;
        public Vector2 velocity = Vector2.Zero;
        public int lifeTime = 0;
    }
}
