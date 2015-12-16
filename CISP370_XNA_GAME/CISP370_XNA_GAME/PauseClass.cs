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

namespace CISP370_XNA_GAME.Screens
{
    class PauseClass : GameScreen
    {
        SpriteFont fntConsolas14;
        public PauseClass()
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Game Paused", new Vector2(250, 150), Color.White);

            if (GameState.controllerType == false)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press P to unpause.", new Vector2(250, 450), Color.White);
            }
            else if (GameState.controllerType == true)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Start to unpause.", new Vector2(250, 450), Color.White);
            }

            GameState.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameState.controllerType == false)
            {
                if (GameState.keyboard.isKeyPressed(Keys.P))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                }
            }
            else
            {
                if (GameState.gamepad.isButtonPressed(Buttons.Start))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                }
            }
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
    }
}
