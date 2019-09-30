using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public float speed = 1f;
    public Transform player;
    private Light spotlight;

    void Start()
    {
        spotlight = GetComponentInChildren<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        //Lower number means it will be yellow for longer
        float dist = Vector3.Distance(spotlight.transform.position, player.position) / 40;  
        //Expected Dist is gonna be somewhere around 40 for now. If that changes this value (40) should too. 

        spotlight.color = Color.Lerp(Color.red, Color.yellow, Mathf.Clamp01(dist));
    }
}
