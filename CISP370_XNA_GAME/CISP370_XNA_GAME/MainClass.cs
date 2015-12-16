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
using XNA_Library;

namespace CISP370_XNA_GAME
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MainClass : Microsoft.Xna.Framework.Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        public MainClass()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
            Components.Add(GameState.keyboard);
            Components.Add(GameState.gamepad);
            Components.Add(GameState.bloom);

        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            //initialize GameState variables
            spriteBatch = new SpriteBatch(GraphicsDevice);

            GameState.screenManager = new ScreenManager();
            GameState.keyboard = new KeyboardHandler(this);
            GameState.gamepad = new GamepadHandler(this);

            GameState.graphicsManager = graphics;
            GameState.graphicsDevice = GraphicsDevice;
            GameState.spriteBatch = spriteBatch;

            GameState.audioEngine = new AudioEngine(@"Content\ScreensAudio.xgs");
            GameState.waveBank = new WaveBank(GameState.audioEngine, @"Content\Wave Bank.xwb");
            GameState.soundBank = new SoundBank(GameState.audioEngine, @"Content\Sound Bank.xsb");

            //add bloom shader GameComponent

            GameState.bloom = new BloomPostprocess.BloomComponent(this);
            GameState.bloom.Settings = new BloomPostprocess.BloomSettings(null, 0.25f, 4, 2, 1, 1.5f, 1);

            GameState.content = Content;
            GameState.setWindowSize(800, 600);

            GameState.modelManager = new ModelManager();
            GameState.scriptManager = new ScriptManager();
            GameState.staticManager = new StaticModelManager();
            GameState.drawableObject = new DrawableObject();

            GameState.listener = new AudioListener();
            GameState.soundPos = new Vector3(0, 0, 0);
            GameState.emitter = new AudioEmitter();
            GameState.soundVolume = 1.0f;

            GameState.game = this;

#if XBOX360
            GameState.controllerType = true;

#elif WINDOWS
            GameState.controllerType = false;
#endif

            // Create a new SpriteBatch, which can be used to draw textures.
            GameState.spriteBatch = new SpriteBatch(GraphicsDevice);
            GameState.content = Content;

            graphics.PreferredBackBufferHeight = GameState.getWindowHeight();
            graphics.PreferredBackBufferWidth = GameState.getWindowWidth();

            graphics.ApplyChanges();
            
            GameState.screenManager.Push(new Screens.SplashScreen());

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here
            GameState.screenManager.Top().Update(gameTime);
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GameState.bloom.BeginDraw();
            GraphicsDevice.Clear(GameState.getColor());
            GameState.screenManager.Top().Draw(gameTime);

            // TODO: Add your drawing code here

            base.Draw(gameTime);
        }
    }
}
