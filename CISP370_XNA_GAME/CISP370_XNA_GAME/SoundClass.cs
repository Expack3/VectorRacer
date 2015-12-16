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
    class SoundClass : GameScreen
    {
        SpriteFont fntConsolas14;
        Boolean playingGame; //determines whether the player came from GameplayClass
        float musicVolume = GameState.soundVolume;
        float musicVolumeExternal = GameState.soundVolume * 100.0f; //used to represent musicVolume to the user

        public SoundClass(Boolean location)
        {
            LoadContent();
            playingGame = location;
        }

        public override void Draw(GameTime gameTime)
        {
            GameState.spriteBatch.Begin();

            GameState.spriteBatch.DrawString(fntConsolas14, "Sound", new Vector2(250, 150), Color.White);
            
            GameState.spriteBatch.DrawString(fntConsolas14, (int)musicVolumeExternal + "%", new Vector2(250, 300), Color.White);

            if (GameState.controllerType == false)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Use Left and Right arrow keys to adjust sound volume.", new Vector2(250, 450), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Esc to go back a screen.", new Vector2(250, 500), Color.White);
            }
            else if (GameState.controllerType == true)
            {
                GameState.spriteBatch.DrawString(fntConsolas14, "Use the Left and Right D-Pad buttons to adjust sound volume.", new Vector2(250, 450), Color.White);
                GameState.spriteBatch.DrawString(fntConsolas14, "Press Back to go back a screen.", new Vector2(250, 500), Color.White);
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

                if (GameState.keyboard.isKeyHeld(Keys.Right))
                {
                    GameState.soundBank.PlayCue("button-10");
                    musicVolume = MathHelper.Clamp(musicVolume + 0.01f, 0.0f, 1.0f);
                    musicVolumeExternal = MathHelper.Clamp(musicVolumeExternal + 1f, 0f, 100f);
                    GameState.audioEngine.GetCategory("Default").SetVolume(musicVolume);
                }
                if (GameState.keyboard.isKeyHeld(Keys.Left))
                {
                    GameState.soundBank.PlayCue("button-10");
                    musicVolume = MathHelper.Clamp(musicVolume - 0.01f, 0.0f, 1.0f);
                    musicVolumeExternal = MathHelper.Clamp(musicVolumeExternal - 1f, 0f, 100f);
                    GameState.audioEngine.GetCategory("Default").SetVolume(musicVolume);
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

                if (GameState.gamepad.isButtonHeld(Buttons.DPadLeft))
                {
                    GameState.soundBank.PlayCue("button-10");
                    musicVolume = MathHelper.Clamp(musicVolume - 0.01f, 0.0f, 1.0f);
                    musicVolumeExternal = MathHelper.Clamp(musicVolumeExternal - 1f, 0f, 100f);
                    GameState.audioEngine.GetCategory("Default").SetVolume(musicVolume);
                }

                if (GameState.gamepad.isButtonHeld(Buttons.DPadRight))
                {
                    GameState.soundBank.PlayCue("button-10");
                    musicVolume = MathHelper.Clamp(musicVolume + 0.01f, 0.0f, 1.0f);
                    musicVolumeExternal = MathHelper.Clamp(musicVolumeExternal + 1f, 0f, 100f);
                    GameState.audioEngine.GetCategory("Default").SetVolume(musicVolume);
                }

            }
            GameState.soundVolume = musicVolume;
            GameState.audioEngine.Update();
        }

        public void LoadContent()
        {
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
    }
}
