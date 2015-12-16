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
    class DrawableObject
    {
        public void updateList(StaticModelManager staticModel, GameTime gameTime)
        {
            bool allExpiredStaticRemoved = true;
            while (allExpiredStaticRemoved == true)
                allExpiredStaticRemoved = staticModel.Clear();

            foreach (GenericModel mod in staticModel.getList)
            {
                mod.Update(gameTime);
            }
        }

        public void drawList(StaticModelManager staticModel, GameTime gameTime)
        {
            foreach (_3D_Model mod in staticModel.getList)
            {
                if (PointAt(Mouse.GetState(), mod).HasValue)
                    DebugShapeRenderer.AddBoundingSphere(mod.boundingSphere, Color.Red);
                else
                    DebugShapeRenderer.AddBoundingSphere(mod.boundingSphere, Color.White);
                DebugShapeRenderer.Draw(gameTime, GameState.camera.View, GameState.camera.Projection);
            }
            foreach (GenericModel mod in staticModel.getList)
            {
                mod.Draw(gameTime);
            }
        }

        public void updateParticleSystems(ThreeDParticleManager particleSystems, GameTime gameTime)
        {
            bool allExpiredParticleSystemsRemoved = true;
            while (allExpiredParticleSystemsRemoved == true)
                allExpiredParticleSystemsRemoved = particleSystems.Clear();
            foreach (ThreeD_ParticleSystem partSys in particleSystems.getList)
            {
                partSys.Update(gameTime);
            }
        }

        public void drawParticleSystems(ThreeDParticleManager particleSystems, GameTime gameTime)
        {
            foreach (ThreeD_ParticleSystem partSys in particleSystems.getList)
            {
                partSys.Draw(gameTime);
            }
        }


        public Nullable<float> PointAt(MouseState mouseState, _3D_Model model)
        {
            int x = mouseState.X;
            int y = mouseState.Y;
            Vector3 nearSource = new Vector3(x, y, 0f);
            Vector3 farSource = new Vector3(x, y, 1f);
            Matrix world = Matrix.Identity;

            Vector3 nearPoint = GameState.graphicsDevice.Viewport.Unproject(nearSource, GameState.camera.Projection, GameState.camera.View, world);
            Vector3 farPoint = GameState.graphicsDevice.Viewport.Unproject(farSource, GameState.camera.Projection, GameState.camera.View, world);

            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();
            Ray picker = new Ray(nearPoint, direction);
            Nullable<float> result = picker.Intersects(model.boundingSphere);

            return result;
        }
    }
}
