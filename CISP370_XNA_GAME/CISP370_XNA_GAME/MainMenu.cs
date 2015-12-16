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
    public class MainMenu : GameScreen
    {
        SpriteFont fntConsolas14;

        public MainMenu()
        {
            LoadContent();
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Main Menu", new Vector2(250, 250), Color.White);

            if (GameState.controllerType == false)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press P to play!", new Vector2(250, 350), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press O for options.", new Vector2(250, 400), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Esc to quit.", new Vector2(250, 450), Color.White);
            }
            else if (GameState.controllerType == true)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Start to play!", new Vector2(250, 350), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press A for options.", new Vector2(250, 400), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to quit.", new Vector2(250, 450), Color.White);
            }

            GameState.spriteBatch.End();
        }

        public override void Update(GameTime gameTime)
        {
            if (GameState.controllerType == false)
            {
                if (GameState.keyboard.isKeyPressed(Keys.Escape))
                {
                    GameState.game.Exit();
                }

                if (GameState.keyboard.isKeyPressed(Keys.P))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new GameplayClass());
                }
                if (GameState.keyboard.isKeyPressed(Keys.O))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new OptionsClass());
                }
            }
            else if (GameState.controllerType == true)
            {
                if (GameState.gamepad.isButtonPressed(Buttons.Start))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new GameplayClass());
                }

                if(GameState.gamepad.isButtonPressed(Buttons.A))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new OptionsClass());
                }

                if (GameState.gamepad.isButtonPressed(Buttons.Back))
                {
                    GameState.game.Exit();
                }

            }
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }

    }
}
