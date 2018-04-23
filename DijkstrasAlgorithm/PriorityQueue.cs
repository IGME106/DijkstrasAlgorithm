using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DijkstrasAlgorithm
{
    class PriorityQueue : Graph
    {
        private List<Vertex> ObjectList { get; set; }
        private int count;
        
        private int Count
        {
            get { return ObjectList.Count; }
            set { count = value; }
        }

        public PriorityQueue(Vertex passedObject)
        {
            ObjectList = new List<Vertex>();
        }

        public void Enqueue(Vertex passedObject)
        {
            ObjectList.Add(passedObject);
            UpdatePriority();
        }

        public Vertex Dequeue()
        {
            Vertex returnValue = null;

            returnValue = ObjectList[0];
            ObjectList.RemoveAt(0);

            UpdatePriority();

            return returnValue;
        }

        private void UpdatePriority()
        {
            Vertex topVertex = ObjectList[0];

            foreach (Vertex listObject in ObjectList)
            {
                if (listObject.Distance < topVertex.Distance)
                {
                    topVertex = listObject;
                }
            }
        }
    }
}
