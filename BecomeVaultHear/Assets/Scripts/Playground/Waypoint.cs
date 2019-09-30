using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoint : MonoBehaviour
{
    //List different prefabs for instantiation

    public float targetDistance;

    Vector2Int gridPos;

    const int gridSize = 1;

    private void Update()
    {
        RaycastHit hit;

        if (Physics.Raycast (transform.position, transform.TransformDirection(Vector3.up), out hit))
        {
            try {
                gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }
            catch (MissingComponentException)
            {
                Debug.Log("No Renderer Found. Raycast Collision Hit.");
            }
            
        }
    }

    public int GetGridSize()
    {
        return gridSize;
    }

    public Vector2Int GetGridPos()
    {
        return new Vector2Int(
            Mathf.RoundToInt(transform.position.x / gridSize),
            Mathf.RoundToInt(transform.position.z / gridSize)
            );
    }
}
