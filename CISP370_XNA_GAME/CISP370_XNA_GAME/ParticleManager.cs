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

namespace CISP370_XNA_GAME
{
    class ThreeDParticleManager
    {
        LinkedList<ThreeD_ParticleSystem> particleSystemList;

        public ThreeDParticleManager()
        {
            particleSystemList = new LinkedList<ThreeD_ParticleSystem>();
        }

        public void addSystem(ThreeD_ParticleSystem system)
        {
            particleSystemList.AddFirst(system);
        }

        public void deleteSystem(ThreeD_ParticleSystem system)
        {
            particleSystemList.Remove(system);
        }

        public void deleteAllSystems()
        {
            while (particleSystemList.First != null)
                particleSystemList.RemoveFirst();
        }

        public LinkedList<ThreeD_ParticleSystem> getList
        {
            get
            {
                return particleSystemList;
            }
        }

        public bool Clear()
        {
            LinkedListNode<ThreeD_ParticleSystem> node;
            for (node = particleSystemList.First; node != null; node = node.Next)
            {
                if ((node.Value.Lifetime <= 0.0) && (node.Value.Lifetime != -999))
                {
                    particleSystemList.Remove(node);
                    return true;
                }
            }
            return false;
        }
    }
}
