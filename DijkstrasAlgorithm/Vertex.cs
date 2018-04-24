using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// IGME-106 - Game Development and Algorithmic Problem Solving
/// Practice exercise 20
/// Class Description   : Vertex holds data
/// Author              : Benjamin Kleynhans
/// Modified By         : Benjamin Kleynhans
/// Date                : April 23, 2018
/// Filename            : Vertex.cs
/// </summary>

namespace DijkstrasAlgorithm
{
    class Vertex
    {
        public string   Name        { get; set; }
        public bool     Visited     { get; set; }
        public int      Distance    { get; set; }
        public Vertex   Neighbor    { get; set; }
        public bool     Final       { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the vertex to add</param>
        public Vertex (string name)
        {
            Name = name;

            ResetVertex();
        }

        /// <summary>
        /// Resets the properties of the vertex
        /// </summary>
        public void ResetVertex()
        {            
            Final       = false;
            Distance    = int.MaxValue;
            Neighbor    = null;

            ResetVisited();
        }

        /// <summary>
        /// Resets the visited property of the vertex
        /// </summary>
        public void ResetVisited()
        {
            Visited = false;
        }

        /// <summary>
        /// ToString
        /// </summary>
        /// <returns>Returns all the values in this class</returns>
        public override string ToString()
        {
            return base.ToString();
        }
    }
}
