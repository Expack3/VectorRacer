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
    public class VictoryScreen : GameScreen
    {
        SpriteFont fntConsolas14;
        private float yourTime;

        public VictoryScreen(float time)
        {
            yourTime = time;
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if ((GameState.keyboard.isKeyPressed(Keys.Escape) && GameState.controllerType == false) || (GameState.gamepad.isButtonPressed(Buttons.Back) && GameState.controllerType == true))
            {
                GameState.soundBank.PlayCue("Hit");
                GameState.screenManager.Pop();
                GameState.screenManager.Push(new MainMenu());
            }

            if ((GameState.keyboard.isKeyPressed(Keys.P) && GameState.controllerType == false) || (GameState.gamepad.isButtonPressed(Buttons.Start) && GameState.controllerType == true))
            {
                GameState.soundBank.PlayCue("Hit");
                GameState.screenManager.Pop();
                GameState.screenManager.Push(new GameplayClass());
            }
        }

        public override void Draw(GameTime gameTime)
        {

            GameState.spriteBatch.Begin();
            GameState.spriteBatch.DrawString(fntConsolas14, "You won with " + yourTime + " seconds left!", new Vector2(150, 150), Color.White);
            if (GameState.controllerType == false)
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Esc to return to the main menu, or press P to try again!", new Vector2(150, 200), Color.White);
            else if (GameState.controllerType == true)
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Back o return to the main menu, or press Start to try again!", new Vector2(150, 200), Color.White);

            GameState.spriteBatch.End();
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
    }
}
