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
    class OptionsClass : GameScreen
    {
        SpriteFont fntConsolas14;

        public OptionsClass()
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Options", new Vector2(250, 150), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Press A for sound.", new Vector2(250, 250), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Press B for display.", new Vector2(250, 350), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Press X for control.", new Vector2(250, 400), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Press Y for credits.", new Vector2(250, 450), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to go back a screen.", new Vector2(250, 500), Color.White);

            GameState.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameState.controllerType == false)
            {
                if (GameState.keyboard.isKeyPressed(Keys.Escape))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new MainMenu());
                }

                if (GameState.keyboard.isKeyPressed(Keys.S))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new SoundClass(false));
                }
                if (GameState.keyboard.isKeyPressed(Keys.D))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new DisplayClass(false));
                }
                if (GameState.keyboard.isKeyPressed(Keys.C))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new ControllerClass(false));
                }
                if (GameState.keyboard.isKeyPressed(Keys.T))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new CreditsClass(false));
                }
            }
            else if (GameState.controllerType == true)
            {
                if (GameState.gamepad.isButtonPressed(Buttons.Back))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new MainMenu());
                }

                if (GameState.gamepad.isButtonPressed(Buttons.A))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new SoundClass(false));
                }
                if (GameState.gamepad.isButtonPressed(Buttons.B))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new DisplayClass(false));
                }
                if (GameState.gamepad.isButtonPressed(Buttons.X))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new ControllerClass(false));
                }
                if (GameState.gamepad.isButtonPressed(Buttons.Y))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new CreditsClass(false));
                }
            }
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
    }
}
