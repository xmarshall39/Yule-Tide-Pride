using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Obstacle : MonoBehaviour
{
    public delegate void ObstacleCollision();
    public static event ObstacleCollision OnObstacleCollision;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player Collided with Obstacle");
            OnObstacleCollision?.Invoke();
        }
    }
}
