using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class for interpreting raw map data and generating Node association
class Graph
{
    public int rows = 0;
    public int cols = 0;
    public Node[] nodes;

    //Constructor: Converts a multi-dim array to an 1D array of associated nodes
    public Graph(int[,] grid)
    {
        //For multidimensional arrays, getLength requires dimensions 
        rows = grid.GetLength(0); //Size of external array
        cols = grid.GetLength(1); //Size of each internal array
        
        //Nodes can be a 1D array for efficiency. With a bit of math, we can mimic 2-D array behavior
        nodes = new Node[grid.Length];  //grid.Length is the total number of values in the array
        //Populate nodes array with grid values (1 or 0)
        for (int i = 0; i < nodes.Length; ++i)
        {
            Node node = new Node
            {
                label = i.ToString()
            };
            nodes[i] = node;
        }

        //Index the grid and build node association
        for (int r = 0; r < rows; ++r)
        {
            for (int c = 0; c < cols; ++c)
            {
                //This will grab the correct node from on the 1D array and convert it to 2D
                Node node = nodes[cols * r + c];
                //Checks if we've hit a wall. 0 is open, 1 is impassable
                if (grid[r, c] == 1) continue;

                //If node is passable, append its neighbors list

                //Up
                if (r > 0) { node.adjacent.Add(nodes[cols * (r - 1) + c]); }

                //Right
                if (c < cols - 1) { node.adjacent.Add(nodes[cols * r + 1 + c]); }

                //Down
                if (r < rows - 1) { node.adjacent.Add(nodes[cols * (r + 1) + c]); }

                //Left
                if (c > 0) { node.adjacent.Add(nodes[cols * r - 1 + c]); }
            }

        }
    }
}

