using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Basic Sight that triggers raycast obstacle detection
public class BaseSight : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "Player")
        {
            LineOfSight LOS = GetComponentInParent<LineOfSight>();
            LOS.ShootRaycast();
           
            
        }
    }
}
