using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// IGME-106 - Game Development and Algorithmic Problem Solving
/// Practice exercise 20
/// Class Description   : Priority Queue implementation
/// Author              : Benjamin Kleynhans
/// Modified By         : Benjamin Kleynhans
/// Date                : April 23, 2018
/// Filename            : PriorityQueue.cs
/// </summary>

namespace DijkstrasAlgorithm
{
    class PriorityQueue : Graph
    {
        private List<Vertex> ObjectList { get; set; }
        private int count;
        
        /// <summary>
        /// Get the length/count of the queue
        /// </summary>
        public int Count
        {
            get { return ObjectList.Count; }
            set { count = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public PriorityQueue()
        {
            ObjectList = new List<Vertex>();
        }

        /// <summary>
        /// Add a vertex to the queue
        /// </summary>
        /// <param name="passedObject">Vertex to add to the queue</param>
        public void Enqueue(Vertex passedObject)
        {
            ObjectList.Add(passedObject);
            UpdatePriority();
        }

        /// <summary>
        /// Remove the first vertex from the list/queue
        /// </summary>
        /// <returns>First vertex in the list that was removed</returns>
        public Vertex Dequeue()
        {
            Vertex returnValue = null;

            returnValue = ObjectList[0];
            ObjectList.RemoveAt(0);

            UpdatePriority();                                                               // Organize the queue so the next priority element is in front

            return returnValue;
        }

        /// <summary>
        /// Returns the first element in the queue without removing it from the queue
        /// </summary>
        /// <returns>First vertex in the queue</returns>
        public Vertex Peek()
        {
            Vertex returnValue = null;

            returnValue = ObjectList[0];

            return returnValue;
        }

        /// <summary>
        /// Determine if the 
        /// </summary>
        private void UpdatePriority()
        {
            foreach (Vertex listObject in ObjectList)
            {
                if (listObject.Distance < ObjectList[0].Distance)
                {                    
                    ObjectList.Insert(0, listObject);
                }
            }
        }
    }
}
