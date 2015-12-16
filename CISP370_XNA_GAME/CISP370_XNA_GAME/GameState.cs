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
using XNA_Library;

namespace CISP370_XNA_GAME
{
    class GameState
    {
        public static AudioEngine audioEngine;
        public static WaveBank waveBank;
        public static SoundBank soundBank;
        public static SpriteBatch spriteBatch;
        public static float soundVolume;

        public static ContentManager content;
        public static ScreenManager screenManager;

        public static KeyboardHandler keyboard;
        public static GamepadHandler gamepad;
        public static BloomPostprocess.BloomComponent bloom;
        public static Game game;
        public static GraphicsDeviceManager graphicsManager;
        public static GraphicsDevice graphicsDevice;
        public static ModelManager modelManager;
        public static StaticModelManager staticManager;
        public static ScriptManager scriptManager;
        public static Camera camera;
        public static Boolean canObjectsRotate = true;

        public static ThreeDParticleManager threeDparticles;
        public static TwoDParticleManager twoDparticles;

        public static AudioEmitter emitter;
        public static AudioListener listener;
        public static Cue cue;
        public static Vector3 soundPos;
        public static AudioCategory audioCategory;

        public static Vector2 viewport;

        public static DrawableObject drawableObject;
        public static Vector2 screenCenter;

        public static int desiredResolution = 2; //used to determine desired screen resolution

        public static Boolean controllerType; //False = keyboard; True = XBOX360 controller or compatible

        private static int windowWidth = 0;
        private static int windowHeight = 0;

        private static Color bgColor = Color.Black;

        private static readonly Vector2[] screenRes = { new Vector2(640.0f, 480.0f), new Vector2(640.0f, 360.0f), new Vector2(800f, 600f), new Vector2(1280f, 720f), new Vector2(1920f, 1080f) };

        public static Player player;

        public static void setWindowSize(int width, int height)
        {
            windowWidth = width;
            windowHeight = height;
        }

        public GraphicsDeviceManager graphicsManger
        {
            get
            {
                return graphicsManager;
            }
            set
            {
                graphicsManager = value;
            }

        }

        public static int getWindowWidth()
        {
            return windowWidth;
        }

        public static int getWindowHeight()
        {
            return windowHeight;
        }

        public static Color getColor()
        {
            return bgColor;
        }

        public static void setColor(Color color)
        {
            bgColor = color;
        }

        public static int[] getScreenRes(int desiredResolution)
        {
            int[] screenReso = new int[2];
            screenReso[0] = (int)screenRes[desiredResolution].X;
            screenReso[1] = (int)screenRes[desiredResolution].Y;
            return screenReso;
        }

        public static void setCenterUsingViewport()
        {
            screenCenter = new Vector2(GameState.graphicsDevice.Viewport.Bounds.Width / 2, GameState.graphicsDevice.Viewport.Bounds.Height / 2);
        }
    }
}
