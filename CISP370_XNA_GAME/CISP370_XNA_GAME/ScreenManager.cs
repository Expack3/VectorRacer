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
    public class ScreenManager
    {
        private Stack<Screens.GameScreen> m_screen;

        public ScreenManager()
        {
            m_screen = new Stack<Screens.GameScreen>();
        }

        public void Push(Screens.GameScreen screen)
        {
            m_screen.Push(screen);
        }

        public void Pop()
        {
            m_screen.Pop();
        }

        public Screens.GameScreen Top()
        {
            return m_screen.Peek();
        }
    }
}