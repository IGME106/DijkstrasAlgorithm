using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// IGME-106 - Game Development and Algorithmic Problem Solving
/// Practice exercise 19
/// Class Description   : Main program
/// Author              : Benjamin Kleynhans
/// Modified By         : Benjamin Kleynhans
/// Date                : April 19, 2018
/// Filename            : Program.cs
/// </summary>
namespace DijkstrasAlgorithm
{
    class Program
    {
        static void Main(string[] args)
        {
            Graph myGraph = new Graph();                                                    // Create graph object

            try
            {
                myGraph.ShortestPath("a");
                myGraph.BreadthFirst("a");                                                    // Start at node "L" and print graph.
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("\nPress enter to continue...");
            Console.ReadLine();
        }
    }
}
