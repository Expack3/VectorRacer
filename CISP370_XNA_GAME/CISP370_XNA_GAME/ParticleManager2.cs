using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISP370_XNA_GAME
{
    class TwoDParticleManager
    {
        LinkedList<ParticleSystem> particleSystemList;

        public TwoDParticleManager()
        {
            particleSystemList = new LinkedList<ParticleSystem>();
        }

        public void addSystem(ParticleSystem system)
        {
            particleSystemList.AddFirst(system);
        }

        public void deleteSystem(ParticleSystem system)
        {
            particleSystemList.Remove(system);
        }

        public void deleteAllSystems()
        {
            while (particleSystemList.First != null)
                particleSystemList.RemoveFirst();
        }

        public LinkedList<ParticleSystem> getList
        {
            get
            {
                return particleSystemList;
            }
        }

        public bool Clear()
        {
            LinkedListNode<ParticleSystem> node;
            for (node = particleSystemList.First; node != null; node = node.Next)
            {
                if ((node.Value.Lifetime <= 0) && (node.Value.Lifetime != -999))
                {
                    particleSystemList.Remove(node);
                    return true;
                }
            }
            return false;
        }
    }
}
