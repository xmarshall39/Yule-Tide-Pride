using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Analyzer : MonoBehaviour
{
    bool isPlayerOn = false;
    //how will isPlayerOn become true???
    //when isPlayerOn == true --> call death function in Player

    // Start is called before the first frame update
    void Start()
    {
        print(gameObject.name);
        foreach (Transform child in gameObject.transform)
        {
            //add all tags of children to a list
            print(child.name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
