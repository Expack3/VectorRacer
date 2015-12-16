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
    class DisplayClass : GameScreen
    {
        SpriteFont fntConsolas14;
        Boolean playingGame; //determines whether the player came from GameplayClass
        String[] displayResNames = { "480p Normal", "480p Widescreen", "Default (600p)", "720p HD", "1080p Full HD" };

        public DisplayClass(Boolean location)
        {
            LoadContent();
            playingGame = location;
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Display Options", new Vector2(250, 150), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, displayResNames[GameState.desiredResolution], new Vector2(250, 200), Color.White);

            if (GameState.graphicsManager.IsFullScreen == true)
                GameState.spriteBatch.DrawString(fntConsolas14, "Fullscreen", new Vector2(250, 300), Color.White);
            else
                GameState.spriteBatch.DrawString(fntConsolas14, "Windowed", new Vector2(250, 300), Color.White);

            if (GameState.controllerType == false)
            {

                GameState.spriteBatch.DrawString(fntConsolas14, "Use the Left and Right arrow keys to select screen resolution.", new Vector2(250, 400), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Enter to switch between windowed and fullscreen.", new Vector2(250, 450), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Escape to move back a screen.", new Vector2(250, 500), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Space to change resolution.", new Vector2(250, 550), Color.White);
            }
            else if (GameState.controllerType == true)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Use the Left and Right D-Pad buttons to select screen resolution.", new Vector2(250, 400), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Start to switch between windowed and fullscreen.", new Vector2(250, 450), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to move back a screen.", new Vector2(250, 500), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press A to change resolution.", new Vector2(250, 550), Color.White);
            }

            GameState.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameState.controllerType == false)
            {
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

                if (GameState.keyboard.isKeyPressed(Keys.Left))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.desiredResolution--;
                    if (GameState.desiredResolution < 0)
                        GameState.desiredResolution = 4;
                }
                if (GameState.keyboard.isKeyPressed(Keys.Right))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.desiredResolution++;
                    if (GameState.desiredResolution > 4)
                        GameState.desiredResolution = 0;
                    
                }
                if (GameState.keyboard.isKeyPressed(Keys.Enter))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.graphicsManager.ToggleFullScreen();
                }
                if (GameState.keyboard.isKeyPressed(Keys.Space))
                {
                    try
                    {
                        GameState.soundBank.PlayCue("button-10");
                        GameState.setWindowSize(GameState.getScreenRes(GameState.desiredResolution)[0], GameState.getScreenRes(GameState.desiredResolution)[1]);
                        GameState.graphicsManager.PreferredBackBufferHeight = GameState.getWindowHeight();
                        GameState.graphicsManager.PreferredBackBufferWidth = GameState.getWindowWidth();
                        GameState.graphicsManager.ApplyChanges();
                        GameState.setCenterUsingViewport();
                    }
                    catch(IndexOutOfRangeException err)
                    {
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

                if (GameState.gamepad.isButtonPressed(Buttons.DPadLeft))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.desiredResolution--;
                    if (GameState.desiredResolution < 0)
                        GameState.desiredResolution = 4;
                    GameState.setWindowSize(GameState.getScreenRes(GameState.desiredResolution)[0], GameState.getScreenRes(GameState.desiredResolution)[1]);
                }
                if (GameState.gamepad.isButtonPressed(Buttons.DPadRight))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.desiredResolution++;
                    if (GameState.desiredResolution > 4)
                        GameState.desiredResolution = 0;
                    GameState.setWindowSize(GameState.getScreenRes(GameState.desiredResolution)[0], GameState.getScreenRes(GameState.desiredResolution)[1]);
                }
                if (GameState.gamepad.isButtonPressed(Buttons.Start))
                {
                    GameState.soundBank.PlayCue("button-10");
                    GameState.graphicsManager.ToggleFullScreen();
                }
                if (GameState.gamepad.isButtonPressed(Buttons.A))
                {
                    try
                    {
                        GameState.soundBank.PlayCue("button-10");
                        GameState.setWindowSize(GameState.getScreenRes(GameState.desiredResolution)[0], GameState.getScreenRes(GameState.desiredResolution)[1]);
                        GameState.graphicsManager.PreferredBackBufferHeight = GameState.getWindowHeight();
                        GameState.graphicsManager.PreferredBackBufferWidth = GameState.getWindowWidth();
                        GameState.graphicsManager.ApplyChanges();
                        GameState.setCenterUsingViewport();
                    }
                    catch (IndexOutOfRangeException err)
                    {
                    }
                }

            }
            GameState.graphicsManager.ApplyChanges();
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
    }
}
