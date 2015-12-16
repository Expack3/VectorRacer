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
    class ControllerClass : GameScreen
    {
        SpriteFont fntConsolas14;
        Boolean playingGame; //determines whether the player came from GameplayClass
        public ControllerClass(Boolean location)
        {
            LoadContent();
            playingGame = location;
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Controller Setup", new Vector2(250, 150), Color.White);
#if WINDOWS
            if (GameState.controllerType == false)
            {
                if(GameState.gamepad.isControllerDisconnected() == false)
                    GameState.spriteBatch.DrawString(fntConsolas14, "Press the Enter key to switch to an XBOX360 controller or compatible.", new Vector2(250, 250), Color.White);
                else
                    GameState.spriteBatch.DrawString(fntConsolas14, "No XBOX360 controller or compatible detected.", new Vector2(250, 350), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Escape to move back a screen.", new Vector2(250, 450), Color.White);
            }
            else if (GameState.controllerType == true)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Start to switch to the keyboard.", new Vector2(250, 350), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to move back a screen.", new Vector2(250, 450), Color.White);
            }
#elif XBOX360
            GameState.spriteBatch.DrawString(fntConsolas14, "This space for rent.", new Vector2(250, 350), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to move back a screen.", new Vector2(250, 450), Color.White);
#endif

            GameState.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
#if WINDOWS
            if (GameState.controllerType == false && GameState.gamepad.isControllerDisconnected() == false)
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
                if (GameState.gamepad.isButtonPressed(Buttons.Start))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.controllerType = false;
                }
#endif
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

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
    }
}
