using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CISP370_XNA_GAME
{
    public class ModelManager
    {
        LinkedList<GenericModel> genericList; //stores any and all models, regardless of type
        int identifier = 1;

        public ModelManager()
        {
            genericList = new LinkedList<GenericModel>(); //initialize the retangleList linked list
        }

        public void addModel(GenericModel m_model)
        {
            //rectangleList.AddFirst(rectangle); //add a model to the top of the linked list
            genericList.AddFirst(m_model);
            identifier++;
        }

        public void deleteModel(GenericModel m_model)
        {
            genericList.Remove(new LinkedListNode<GenericModel>(m_model));
        }

        public void deleteAllModels() //used to clear the model manager
        {
            while (genericList.First != null)
            {
                genericList.RemoveFirst();
            }
        }

        public void randomModelAging() //used to test lifetime values by assigning a lifetime of 60 frames to a random model
        {
            LinkedListNode<GenericModel> node = genericList.First;
            if (node != null)
            {
                int random = new Random().Next(1, genericList.Count());
                for (int x = 1; x <= random; x++)
                {
                    if (x == random)
                    {
                        node.Value.Lifetime = 60;
                        x = genericList.Count + 1;
                    }
                    else
                        node = node.Next;

                }
            }
        }

        public bool Clear()
        {
            LinkedListNode<GenericModel> node;
            for (node = genericList.First; node != null; node = node.Next)
            {
                if ((node.Value.Lifetime <= 0.0f) && (node.Value.Lifetime != -999.0f))
                {
                    genericList.Remove(node);
                    return true;
                }
            }
            return false;
        }

        public LinkedList<GenericModel> getList
        {
            get
            {
                return genericList;
            }
        }
    }
}
