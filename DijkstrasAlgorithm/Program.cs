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
            string startLocation = "a";
            string destination = null;

            try
            {
                myGraph.ShortestPath(startLocation);
                //myGraph.BreadthFirst("a");                                                    // Start at node "L" and print graph.
            }
            catch (IndexOutOfRangeException e)
            {
                Console.WriteLine(e.Message);
            }

            Boolean validInput = false;

            do
            {
                do
                {
                    Console.Write("You are in \"" + startLocation.ToUpper() + "\". Where are you going? (enter \"XX\" to exit)\n > ");
                    destination = Console.ReadLine().ToUpper();

                    if ((Graph.GraphDictionary.ContainsKey(destination)) || (destination.Equals("XX")))
                    {
                        validInput = true;
                    }
                    else
                    {
                        Console.WriteLine("\nThe value you entered does not exist in the graph, please try again");
                    }

                } while (!validInput);
                

                if (!destination.Equals("XX"))
                {
                    Console.WriteLine("\nThe shortest path is: ");

                    Console.WriteLine(myGraph.FindPath(startLocation, destination));

                    Console.WriteLine("\nPress enter to continue...");
                    Console.ReadLine();
                }

            } while (!destination.Equals("XX"));
            
        }
    }
}
