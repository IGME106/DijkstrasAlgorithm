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
            string startLocation = null;
            string destination = null;

            Console.WriteLine("This is a Graph showing the implementation of Dijkstra's algorithm.");
            Console.WriteLine("The Graph consists of the entire alphabet (A to Z)\n");

            Boolean validInput;                                                             // Test if input is valid
            Boolean breakLoop = false;                                                      // Test if exit is requested

            do
            {
                validInput = false;

                do                                                                          // Loop until valid input is received
                {
                    try
                    {
                        Console.Write("Please enter the point you would like to start from? (enter \"XX\" to exit)\n > ");
                        startLocation = Console.ReadLine().ToUpper();

                        if (startLocation.Equals("XX"))                                     // If xx is entered, input is valid, but loop needs to be broken
                        {
                            breakLoop = true;
                        }
                        else
                        {
                            myGraph.ShortestPath(startLocation);                            // Run the algorithm with supplied input
                        }

                        validInput = true;
                    }
                    catch (IndexOutOfRangeException e)                                      // If the input doesn't exist in the list, handle exception
                    {
                        Console.WriteLine("\n" + e.Message + "\n");
                    }

                } while (!validInput);

                validInput = false;                                                         // Reset valid input property

                if (!startLocation.Equals("XX"))                                            // If an exit was not already requested, run the following
                {
                    do                                                                      // As for the destination
                    {
                        Console.Write("You are in \"" + startLocation.ToUpper() + "\". Where are you going? (enter \"XX\" to exit)\n > ");
                        destination = Console.ReadLine().ToUpper();

                        if ((Graph.GraphDictionary.ContainsKey(destination)) || (destination.Equals("XX")))
                        {
                            validInput = true;                                              // Mark input as valid if it is
                        }
                        else
                        {                                                                   // Handle input error without throwing exception
                            Console.WriteLine("\nThe value you entered does not exist in the graph, please try again");
                        }

                    } while (!validInput);                                                  // As for input until valid input is received
                    
                    if (!destination.Equals("XX"))                                          // If exit was not requested, continue
                    {
                        Console.WriteLine("\nThe shortest path is: ");

                        try
                        {
                            Console.WriteLine(myGraph.FindPath(startLocation, destination));// Try to print the required path
                        }
                        catch (IndexOutOfRangeException InputException)
                        {
                            Console.WriteLine(InputException.Message);
                        }

                        Console.WriteLine("\nPress enter to continue...");
                        Console.ReadLine();
                    }
                    else
                    {
                        breakLoop = true;
                    }
                }
                

            } while (!breakLoop);                                                           // End the program            
        }
    }
}
