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
    class GameplayClass : GameScreen
    {
        Texture2D crosshair;
#if DEBUG
        static float _3DsoundVolume;
#endif
        Vector2 pos;
        Boolean canPlayerMove = true;
        int numOfShields = 0;
        float timeLeft = 200.00f;
        static float distance_traveled;
        SpriteFont fntConsolas14;
        public GameplayClass()
        {
            GameState.camera = new Camera(new Vector3(0, 0, 50), new Vector3(0, 0, -50), Vector3.Up, 1, -1.0f); //TODO: Change speed to variable from hard-coded constant
            crosshair = GameState.content.Load<Texture2D>(@"Textures\hud_sp_crosshair");
            GameState.setCenterUsingViewport();
#if DEBUG
            if(DebugShapeRenderer.doesGraphicsExist() == false)
                DebugShapeRenderer.Initialize(GameState.graphicsDevice);
#endif
            RasterizerState rasterState = new RasterizerState();
            rasterState.CullMode = CullMode.None;
            GameState.graphicsDevice.RasterizerState = rasterState;
            GameState.scriptManager.loadScript("Level4.script");
            GameState.player = new Player("Player", GameState.camera.getPosition(), 6.0f);
            GameState.setCenterUsingViewport();
            fntConsolas14 = GameState.content.Load<SpriteFont>(@"Consolas14");
        }
        
        public override void Draw(GameTime gameTime)
        {
            GameState.graphicsDevice.Clear(GameState.getColor());

            Mouse.SetPosition((int)GameState.screenCenter.X, (int)GameState.screenCenter.Y);
            // TODO: Add your drawing code here

            //Render crosshair
            pos.X = (GameState.screenCenter.X) - crosshair.Width / 2.0f;
            pos.Y = (GameState.screenCenter.Y) - crosshair.Height / 2.0f;
            GameState.drawableObject.drawList(GameState.staticManager, gameTime);
            
            GameState.spriteBatch.Begin();
            if (numOfShields > 0)
                GameState.spriteBatch.DrawString(fntConsolas14, "Shield(s) available: " + numOfShields, new Vector2(150, 150), Color.White);
            GameState.spriteBatch.DrawString(fntConsolas14, "Time Left: " + timeLeft, new Vector2(150, 250), Color.White);
            GameState.spriteBatch.End();
            GameState.game.Window.Title = "X: " + GameState.camera.getPosition().X + " Z: " + GameState.camera.getPosition().Z + " Dist: " + distanceRemaining();

            
        }

        public override void Update(GameTime gameTime)
        {
            Boolean PlayerObstacleColission = false;
            foreach (_3D_Model mod in GameState.staticManager.getList)
            {
                if (mod.boundingSphere.Intersects(GameState.player.boundingSphere))
                {
                    switch (mod.getName())
                    {
                        default:
                            GameState.setColor(Color.Green);
                            play3Dsound(mod);
                            break;
                        case "Powerup":
                            GameState.setColor(Color.Beige);
                            numOfShields++;
                            play3Dsound(mod);
                            mod.Lifetime = 1;
                            break;
                        case "Obstacle":
                            if (numOfShields > 0)
                            {
                                mod.Lifetime = 1;
                                numOfShields--;
                            }
                            else if (canPlayerMove == false && removeObstacle() == true)
                            {
                                timeLeft -= 100.00f;
                                mod.Lifetime = 1;
                                canPlayerMove = true;
                                play3Dsound(mod);
                            }
                            else
                            {
                                canPlayerMove = false;
                                PlayerObstacleColission = true;
                            }
                            GameState.setColor(Color.Chocolate);
                            if(canPlayerMove == true)
                                play3Dsound(mod);
                            break;
                        case "Enemy":
                            if (numOfShields > 0)
                            {
                                mod.Lifetime = 1;
                                numOfShields--;
                            }
                            else
                            {
                                timeLeft -= 1.5f;
                            }
                            GameState.setColor(Color.Red);
                            play3Dsound(mod);
                            break;

                    }
                }
                else
                {
                    GameState.setColor(Color.Black);
                }
            }
            if (PlayerObstacleColission == false)
                canPlayerMove = true;

            checkController(canPlayerMove);

            if (GameState.keyboard.isKeyPressed(Keys.Space))
                GameState.staticManager.randomModelAging();
            if (timeLeft > 0.000f)
                MathHelper.Clamp(timeLeft -= (float)Math.Round(gameTime.ElapsedGameTime.TotalMilliseconds, 3) * 0.001f, 0.000f, 999.000f);
            else
            {
                GameState.soundBank.PlayCue("explosion-01");
                GameState.screenManager.Pop();
                GameState.screenManager.Push(new MainMenu());
            }

            GameState.camera.Update(gameTime);
            GameState.player.Position = GameState.camera.getPosition();
            GameState.drawableObject.updateList(GameState.staticManager, gameTime);
            GameState.listener.Position = GameState.camera.getPosition();
            GameState.audioEngine.Update();

            

            //rectangle.Position += new Vector3(0.25f, 0, 0);

            GameState.drawableObject.updateList(GameState.staticManager, gameTime);
            if (isDistanceReached())
            {
                GameState.soundBank.PlayCue("Hit");
                GameState.screenManager.Pop();
                GameState.screenManager.Push(new VictoryScreen(timeLeft));
            }

        }

        private static void play3Dsound(_3D_Model model)
        {
            GameState.emitter.Position = model.getPosition();
            switch (model.getName())
            {
                case "Powerup":
                    GameState.cue = GameState.soundBank.GetCue("button-10");
                    break;
                case "Obstacle":
                    GameState.cue = GameState.soundBank.GetCue("button-10");
                    break;
                case "Enemy":
                    GameState.cue = GameState.soundBank.GetCue("button-10");
                    break;
                default:
                    GameState.cue = GameState.soundBank.GetCue("button-10");
                    break;
            }
            
            GameState.cue.Apply3D(GameState.listener, GameState.emitter);
            GameState.audioEngine.Update();
            GameState.cue.Play();
        }

        private static bool removeObstacle()
        {
            if (GameState.controllerType == false)
            {
                if(GameState.keyboard.isKeyPressed(Keys.Enter))
                    return true;
            }
            else if(GameState.gamepad.isButtonPressed(Buttons.RightStick))
            {
                return true;
            }
            return false;
        }

        private static void checkController(bool canPlayerMove)
        {
            if (canPlayerMove == true)
            {
                GameState.camera.setPosZ(GameState.camera.getPosition().Z - GameState.camera.Speed);
                GameState.camera.setLookatZ(GameState.camera.Lookat.Z - GameState.camera.Speed);
                distance_traveled = -(GameState.camera.getPosition().Z - GameState.camera.Speed);
            }
            if (GameState.controllerType == false)
            {
                if (GameState.keyboard.isKeyPressed(Keys.Escape))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new MainMenu());
                }
                if (GameState.keyboard.isKeyPressed(Keys.P))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new PauseClass());
                }
                if (GameState.keyboard.isKeyPressed(Keys.S))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new SoundClass(true));
                }
                if (GameState.keyboard.isKeyPressed(Keys.D))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new DisplayClass(true));
                }
                if (GameState.keyboard.isKeyPressed(Keys.C))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new ControllerClass(true));
                }

                if (canPlayerMove == true)
                {
                    if (GameState.keyboard.isKeyHeld(Keys.Left))
                    {
                        if ((GameState.camera.getPosition().X - GameState.camera.Speed) > -50f)
                        {
                            GameState.camera.setPosX(GameState.camera.getPosition().X - GameState.camera.Speed);
                            GameState.camera.setLookatX(GameState.camera.Lookat.X - GameState.camera.Speed);
                        }
                    }
                    if (GameState.keyboard.isKeyHeld(Keys.Right))
                    {
                        if ((GameState.camera.getPosition().X + GameState.camera.Speed) < 50f)
                        {
                            GameState.camera.setPosX(GameState.camera.getPosition().X + GameState.camera.Speed);
                            GameState.camera.setLookatX(GameState.camera.Lookat.X + GameState.camera.Speed);
                        }
                    }
                }
            }
            else
            {
                if (GameState.gamepad.isButtonPressed(Buttons.Back))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Pop();
                    GameState.screenManager.Push(new MainMenu());
                }
                if (GameState.gamepad.isButtonPressed(Buttons.Start))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new PauseClass());
                }
                if (GameState.gamepad.isButtonPressed(Buttons.A))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new SoundClass(true));
                }
                if (GameState.gamepad.isButtonPressed(Buttons.B))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new DisplayClass(true));
                }
                if (GameState.gamepad.isButtonPressed(Buttons.X))
                {
                    GameState.soundBank.PlayCue("Hit");
                    GameState.screenManager.Push(new ControllerClass(true));
                }
                if (canPlayerMove == true)
                {
                    if (GameState.gamepad.isButtonHeld(Buttons.DPadLeft))
                    {
                        if ((GameState.camera.getPosition().X - GameState.camera.Speed) > -50f)
                        {
                            GameState.camera.setPosX(GameState.camera.getPosition().X - GameState.camera.Speed);
                            GameState.camera.setLookatX(GameState.camera.Lookat.X - GameState.camera.Speed);
                        }
                    }
                    if (GameState.gamepad.isButtonHeld(Buttons.DPadRight))
                    {
                        if ((GameState.camera.getPosition().X + GameState.camera.Speed) < 50f)
                        {
                            GameState.camera.setPosX(GameState.camera.getPosition().X + GameState.camera.Speed);
                            GameState.camera.setLookatX(GameState.camera.Lookat.X + GameState.camera.Speed);
                        }
                    }
                }
            }
        }

        private bool isDistanceReached()
        {
            if (distance_traveled >= GameState.camera.TargetDistance)
                return true;
            return false;
        }

        private float distanceRemaining()
        {
            return GameState.camera.TargetDistance - distance_traveled;
        }
    }
}
