using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeNoise : MonoBehaviour
{
    // Start is called before the first frame update
    public AudioSource feet;
    public AudioClip footfalls;
    void Start()
    {
        InvokeRepeating("StippyStep", 1, 7);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void StippyStep()
    {
        feet.PlayOneShot(footfalls, 0.198f);
    }
}
