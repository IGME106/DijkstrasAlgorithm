using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// IGME-106 - Game Development and Algorithmic Problem Solving
/// Practice exercise 20
/// Class Description   : Implementation of Dijkstras Algoritm
/// Author              : Benjamin Kleynhans
/// Modified By         : Benjamin Kleynhans
/// Date                : April 23, 2018
/// Filename            : Graph.cs
/// </summary>

namespace DijkstrasAlgorithm
{
    class Graph
    {
        public static Dictionary<string, Vertex> GraphDictionary { get; set; }
        public static List<Vertex> GraphList { get; set; }
        private static List<Vertex> VisitedList { get; set; }

        /// <summary>
        /// Create a pretty large adjacency matrix
        /// </summary>
        private int[,] adjMatrix = new int[26, 26]
            {   //A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z
          /*A*/ { 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*B*/ { 1, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*C*/ { 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*D*/ { 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*E*/ { 0, 0, 1, 0, 0, 0, 1, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*F*/ { 0, 1, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*G*/ { 0, 0, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*H*/ { 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*I*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*J*/ { 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*K*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*L*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*M*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*N*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*O*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0 },
          /*P*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 1, 0, 0 },
          /*Q*/ { 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0 },
          /*R*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
          /*S*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 1, 0, 0, 0, 0, 0 },
          /*T*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0 },
          /*U*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 1, 0, 0, 0 },
          /*V*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0 },
          /*W*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 0, 0, 0, 1 },
          /*X*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0 },
          /*Y*/ { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1 },
          /*Z*/ { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 0, 1, 0 }
            };

        /// <summary>
        /// Constructor
        /// </summary>
        public Graph()
        {
            GraphDictionary = new Dictionary<string, Vertex>();
            GraphList = new List<Vertex>();
            VisitedList = new List<Vertex>();

            InstantiateVertexes();
        }

        /// <summary>
        /// Create the vertices (this is pretty long)
        /// </summary>
        public void InstantiateVertexes()
        {
            Vertex vA = new Vertex("A");
            Vertex vB = new Vertex("B");
            Vertex vC = new Vertex("C");
            Vertex vD = new Vertex("D");
            Vertex vE = new Vertex("E");
            Vertex vF = new Vertex("F");
            Vertex vG = new Vertex("G");
            Vertex vH = new Vertex("H");
            Vertex vI = new Vertex("I");
            Vertex vJ = new Vertex("J");
            Vertex vK = new Vertex("K");
            Vertex vL = new Vertex("L");
            Vertex vM = new Vertex("M");
            Vertex vN = new Vertex("N");
            Vertex vO = new Vertex("O");
            Vertex vP = new Vertex("P");
            Vertex vQ = new Vertex("Q");
            Vertex vR = new Vertex("R");
            Vertex vS = new Vertex("S");
            Vertex vT = new Vertex("T");
            Vertex vU = new Vertex("U");
            Vertex vV = new Vertex("V");
            Vertex vW = new Vertex("W");
            Vertex vX = new Vertex("X");
            Vertex vY = new Vertex("Y");
            Vertex vZ = new Vertex("Z");

            GraphList.Add(vA);
            GraphDictionary.Add("A", vA);
            
            GraphList.Add(vB);
            GraphDictionary.Add("B", vB);

            GraphList.Add(vC);
            GraphDictionary.Add("C", vC);

            GraphList.Add(vD);
            GraphDictionary.Add("D", vD);

            GraphList.Add(vE);
            GraphDictionary.Add("E", vE);

            GraphList.Add(vF);
            GraphDictionary.Add("F", vF);

            GraphList.Add(vG);
            GraphDictionary.Add("G", vG);

            GraphList.Add(vH);
            GraphDictionary.Add("H", vH);

            GraphList.Add(vI);
            GraphDictionary.Add("I", vI);

            GraphList.Add(vJ);
            GraphDictionary.Add("J", vJ);

            GraphList.Add(vK);
            GraphDictionary.Add("K", vK);

            GraphList.Add(vL);
            GraphDictionary.Add("L", vL);

            GraphList.Add(vM);
            GraphDictionary.Add("M", vM);

            GraphList.Add(vN);
            GraphDictionary.Add("N", vN);

            GraphList.Add(vO);
            GraphDictionary.Add("O", vO);

            GraphList.Add(vP);
            GraphDictionary.Add("P", vP);

            GraphList.Add(vQ);
            GraphDictionary.Add("Q", vQ);

            GraphList.Add(vR);
            GraphDictionary.Add("R", vR);

            GraphList.Add(vS);
            GraphDictionary.Add("S", vS);

            GraphList.Add(vT);
            GraphDictionary.Add("T", vT);

            GraphList.Add(vU);
            GraphDictionary.Add("U", vU);

            GraphList.Add(vV);
            GraphDictionary.Add("V", vV);

            GraphList.Add(vW);
            GraphDictionary.Add("W", vW);

            GraphList.Add(vX);
            GraphDictionary.Add("X", vX);

            GraphList.Add(vY);
            GraphDictionary.Add("Y", vY);

            GraphList.Add(vZ);
            GraphDictionary.Add("Z", vZ);
        }

        /// <summary>
        /// Reset the search tree
        /// </summary>
        public void Reset()
        {
            foreach (Vertex vertex in GraphList)
            {
                vertex.ResetVertex();
            }
        }

        public void ResetVisited()
        {
            foreach (Vertex vertex in GraphList)
            {
                vertex.ResetVisited();
            }
        }

        /// <summary>
        /// Dijkstra's algorithm - calculate shortest path
        /// </summary>
        /// <param name="name">Starting vertex to calculate from</param>
        public void ShortestPath(string name)
        {
            PriorityQueue searchQueue = new PriorityQueue();            
            Vertex nextVertex;

            List<Vertex> currentVertices = new List<Vertex>();

            Reset();                                                                        // Reset all graph weight information

            name = name.ToUpper();                                                          // Set input data to uppercase

            if (!GraphDictionary.ContainsKey(name))                                         // If the supplied index doesn't exist, throw exception
            {
                throw new IndexOutOfRangeException();
            }

            GraphDictionary[name].Distance = 0;                                             // Set first node properties
            GraphDictionary[name].Final = true;

            int distanceBetweenNeighbors;
            int distanceToSource;

            try
            {
                GraphDictionary[name].Visited = true;                                       // Set first vertex as visited
                searchQueue.Enqueue(GraphDictionary[name]);                                 // Add first vertex to priority queue

                do
                {
                    nextVertex = GetAdjacentUnvisited(searchQueue.Peek().Name);             // Get next adjacent vertex

                    if (nextVertex != null)
                    {
                        distanceBetweenNeighbors = adjMatrix[                               // Calculate distance between focus and neighbor
                                                        GraphList.IndexOf(GraphDictionary[searchQueue.Peek().Name]),
                                                        GraphList.IndexOf(nextVertex)
                                                    ];

                        distanceToSource = (distanceBetweenNeighbors + searchQueue.Peek().Distance);// Calculate distance to head

                        if ((
                                (nextVertex.Neighbor == null) ||                            // IF neighbor is null
                                (nextVertex.Distance > distanceToSource)                    // OR it has a larger distance than the new vertex
                            ) && (
                                !nextVertex.Final                                           // AND it is not set to final
                            ))
                        {
                            nextVertex.Neighbor = searchQueue.Peek();                       // Set neighbor's neighbor to head
                            nextVertex.Distance = distanceToSource;                         // Set new distance

                            searchQueue.Enqueue(nextVertex);                                // Add the neighbor to the priority queue
                        }

                        distanceBetweenNeighbors = 0;
                        distanceToSource = 0;

                        if (!nextVertex.Final)                                              // If the neighbor is not yet set to final
                        {
                            currentVertices.Add(nextVertex);                                // add it to the current list
                        }
                    }
                    else                                                                    // If the neighbor is null (does not exist)
                    {
                        if (currentVertices.Count != 0)                                     // If vertices were added to the current list
                        {
                            UpdateFinals(currentVertices);                                  // Update final properties
                            currentVertices.Clear();                                        // Clear the list
                        }

                        ResetVisited();                                                     // Reset visited properties
                        searchQueue.Dequeue();                                              // Remove the vertex from the queue
                        
                    }

                    nextVertex = null;                                                      // Set neighbor to null

                } while (searchQueue != null);
            }
            catch (Exception DepthFirstException)                                           // Catch possible exceptions
            {
                if ((DepthFirstException is KeyNotFoundException) ||
                    (DepthFirstException is IndexOutOfRangeException))
                {
                    throw new IndexOutOfRangeException("The specified index does not exist");
                }
            }
        }

        /// <summary>
        /// Loops through the list of vertices that were visited to determine which
        /// vertex has the shortest distance to head
        /// </summary>
        /// <param name="currentVertices">List of vertices visited this cycle</param>
        private void UpdateFinals(List<Vertex> currentVertices)
        {
            Vertex smallestVertex = currentVertices[0];
            List<Vertex> sameDistance = new List<Vertex>();

            foreach (Vertex vertex in currentVertices)                                      // For each vertex in the list
            {
                if (vertex.Distance < smallestVertex.Distance)                              // If the distance of one is less than the distance of another
                {
                    smallestVertex = vertex;                                                // Set the smallest vertex equal to that vertex
                }
                else if (vertex.Distance.Equals(smallestVertex.Distance))                   // If there are multiple vertices with the same distance
                {
                    if (!sameDistance.Contains(vertex))
                    {
                        sameDistance.Add(vertex);                                           // Add them to a list
                    }
                }
            }

            if (smallestVertex.Distance.Equals(sameDistance[0].Distance))                   // If the items in the list have the same value as the
            {                                                                               // item with the smallest distance
                foreach(Vertex vertex in sameDistance)
                {
                    vertex.Final = true;                                                    // Set all items in the list to final
                }
            }
            else
            {
                smallestVertex.Final = true;                                                // If they are not the same size as the smallest vertex
            }                                                                               // set only the smallest vertex final
        }

        /// <summary>
        /// Return the name (key) of the next unvisited vertex
        /// </summary>
        /// <param name="name">Name/Key of the Dictionary item to look for</param>
        /// <returns>Vertex of the next, previously unvisited vertex</returns>
        public Vertex GetAdjacentUnvisited(String name)
        {
            bool foundVisited = false;
            int indexOfVertexInList = 0;
            int incrementor = 0;
            Vertex returnValue = null;

            if (GraphDictionary.ContainsKey(name))                                          // Test if the key exists in the dictionary
            {
                indexOfVertexInList = GraphList.IndexOf(GraphDictionary[name]);             // Get the index of the vertex

                do                                                                          // Loop through vertices until one is found that
                {                                                                           // wasn't found before
                    if ((adjMatrix[indexOfVertexInList, incrementor] != 0) &&
                        (!GraphList[incrementor].Visited) &&
                        (!VisitedList.Contains(GraphList[incrementor])))
                    {
                        returnValue = GraphList[incrementor];                               // Return the next vertex
                        GraphList[incrementor].Visited = true;                              // Set it's "visited" property to true
                        foundVisited = true;                                                // Set the "found" variable to true to break the loop
                    }

                    incrementor++;                                                          // do/while incrementor

                } while ((!foundVisited) && (incrementor < adjMatrix.GetLength(1)));        // Once we find a vertex, or get to the end of the matrix, break
            }

            return returnValue;
        }

        /// <summary>
        /// Search through the updated list and return the shortest path
        /// </summary>
        /// <param name="startLocation">Start location entered by user</param>
        /// <param name="destination">End locatino entered by user</param>
        /// <returns>A string containing the path to follow</returns>
        public string FindPath(string startLocation, string destination)
        {
            string returnValue = null;

            startLocation = startLocation.ToUpper();
            destination = destination.ToUpper();

            List<string> pathList = new List<string>();

            do
            {
                pathList.Add(destination);

                try
                {
                    destination = GraphDictionary[destination].Neighbor.Name;
                } catch (Exception KeyException)
                {
                    if ((KeyException is IndexOutOfRangeException) ||
                        (KeyException is KeyNotFoundException))
                    {
                        throw new IndexOutOfRangeException(KeyException.Message);
                    }                    
                }
                

            } while (destination != startLocation);
            
            StringBuilder returnString = new StringBuilder();

            foreach (string pathString in pathList)
            {
                returnString.Append(pathString + "\n");
            }

            returnString.Append(startLocation);

            returnValue = returnString.ToString();

            return returnValue;
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
