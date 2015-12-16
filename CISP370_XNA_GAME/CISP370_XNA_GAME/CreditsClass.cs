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
    class CreditsClass : GameScreen
    {
        SpriteFont fntConsolas14;
        Boolean playingGame; //determines whether the player came from GameplayClass
        public CreditsClass(Boolean location)
        {
            LoadContent();
            playingGame = location;
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Game programmed, designed, etc by James McDermott", new Vector2(250, 150), Color.White);

            if (GameState.controllerType == false)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Escape to move back a screen.", new Vector2(250, 450), Color.White);
            }
            else if (GameState.controllerType == true)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to move back a screen.", new Vector2(250, 450), Color.White);
            }

            GameState.spriteBatch.End();
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }

        public override void Update(GameTime gameTime)
        {
            if (GameState.controllerType == false)
            {
                if (GameState.keyboard.isKeyPressed(Keys.Enter))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.controllerType = true;
                }
                if (GameState.keyboard.isKeyPressed(Keys.Escape))
                {
                    if (playingGame == false)
                    {
                        GameState.soundBank.PlayCue("Hit");
                        GameState.screenManager.Pop();
                        GameState.screenManager.Push(new OptionsClass());
                    }
                    else
                    {
                        GameState.soundBank.PlayCue("Hit");
                        GameState.screenManager.Pop();
                    }
                }
            }
            else if (GameState.controllerType == true)
            {
                if (GameState.gamepad.isButtonPressed(Buttons.Back))
                {
                    if (playingGame == false)
                    {
                        GameState.soundBank.PlayCue("Hit");
                        GameState.screenManager.Pop();
                        GameState.screenManager.Push(new OptionsClass());
                    }
                    else
                    {
                        GameState.soundBank.PlayCue("Hit");
                        GameState.screenManager.Pop();
                    }
                }
            }
        }
    }
}
