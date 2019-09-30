using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Require component for basic cube meshes
public class PathTest : MonoBehaviour
{
    public GameObject meshParent; //This gameobject should store all of the meshes used to construct a map
    //It would be great for this to be auto generated but that's not necessary.
    //Any additional features cosmetic or otherwise can be part of the prefab parent

        //Prefabing these gameobjects might be a bit tricky, so here's the structure I think works
        //[Chunk Name] -> Eldest parent of the prefabs. stores everything
            //[Cosmetic Parent] -> Parent Empty gameobject for all things cosmetic in the prefab
                //Obj
                //Obj
                //Obj
            //[MapParent] -> Parent empty gameobject for the interactable meshes on the grid. Only thing read by this code.
                //1
                //2
                //3
                //4...

    private void Start()
    {
        int[,] testMap = new int[10, 3]
    {
        { 0, 0, 1 },
        { 0, 0, 0 },
        { 1, 1, 1 },
        { 0, 1, 0 },
        { 0, 0, 0 },
        { 0, 1, 0 },
        { 0, 0, 1 },
        { 1, 0, 1 },
        { 0, 0, 0 },
        { 0, 0, 0 }

    };
        Graph testGrid = new Graph(testMap);

        //At this point, we should be generating meshes in accordance to the array
        MarkMeshes(testGrid, meshParent);
        
    }
    //Mark each mesh with a given color depending on passability
    private void MarkMeshes(Graph map, GameObject meshMap)
    {
        foreach(Transform child in meshMap.transform)
        {
            try
            {
                //Change the color of each block to red or green according to node adjacency.
                if(map.nodes[Int32.Parse(child.name)].adjacent.Count == 0)
                {
                    child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
                }
                else child.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            catch (System.Exception)
            {
                Console.WriteLine("Indexing Error In Color Change: Try changing the mesh labels");
                throw;
            }
        }
    }

}
