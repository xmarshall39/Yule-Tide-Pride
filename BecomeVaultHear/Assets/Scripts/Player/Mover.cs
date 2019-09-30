using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ChangeParentLocation(float loc)
    {
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, loc);
    }

    public void StrafeParentLeft(float loc)
    {
        //gameObject.transform.position = new Vector3(gameObject.GetComponentInChildren<Transform>().transform.position.x, transform.position.y, transform.position.z);
        gameObject.transform.position = new Vector3(loc, transform.position.y, transform.position.z);
    }

    public void StrafeParentRight(float loc)
    {
        gameObject.transform.position = new Vector3(loc, transform.position.y, transform.position.z);
    }

    //Both strafing functions are essentially the same, so they will be combined later to clean the code
}
