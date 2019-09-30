using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Perhaps change list to an array of size 4 where each index corresponds to a direction
public enum Direction{
    Up,
    Left,
    Down,
    Right
    }

//This class provides the basic units for groups of moveable tiles in-game
class Node
{
    public List<Node> adjacent = new List<Node>();
    public Node prev = null; //Used for pathfinding exclusively
    public string label = ""; //Name of associated mesh

    public void Clear()
    {
        prev = null;
    }
}
