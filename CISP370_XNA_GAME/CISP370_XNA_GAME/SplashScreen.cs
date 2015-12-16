using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace CISP370_XNA_GAME.Screens
{
    public class SplashScreen : GameScreen
    {
        SpriteFont fntConsolas14;
        Texture2D titleScreen;

        public SplashScreen()
        {
            LoadContent();
        }

        public override void Update(GameTime gameTime)
        {
            if ((GameState.keyboard.isKeyPressed(Keys.Escape) && GameState.controllerType == false) || (GameState.gamepad.isButtonPressed(Buttons.Start) && GameState.controllerType == true))
            {
                GameState.soundBank.PlayCue("Hit");
                GameState.screenManager.Pop();
                GameState.screenManager.Push(new MainMenu());
            }
        }

        public override void Draw(GameTime gameTime)
        {
            
            GameState.spriteBatch.Begin();
            GameState.spriteBatch.Draw(titleScreen, GameState.screenCenter, Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Splash Screen!", new Vector2(150, 150), Color.White);
        if(GameState.controllerType == false)
            GameState.spriteBatch.DrawString(fntConsolas14, "Press Esc now...", new Vector2(150, 200), Color.White);
        else if(GameState.controllerType == true)
            GameState.spriteBatch.DrawString(fntConsolas14, "Press Start now...", new Vector2(150, 200), Color.White);

            GameState.spriteBatch.End();
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
            titleScreen = GameState.content.Load<Texture2D>(@"Textures\TitleScreen");
        }
    }
}
