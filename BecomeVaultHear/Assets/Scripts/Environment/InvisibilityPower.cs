using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibilityPower : MonoBehaviour
{
    public delegate void Invisibility();
    public static event Invisibility OnInvisibility;
    [SerializeField]
    private bool isEnabled = true;    //DEBUG ONLY
    void DrawColoredRay(Color col)
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.back) * 2, col);
    }

    private void WatchPlayer()
    {
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out RaycastHit hit, 2))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                if (isEnabled) OnInvisibility?.Invoke();
                Destroy(this.gameObject);
            }
            else DrawColoredRay(Color.blue);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            OnInvisibility?.Invoke();
            Destroy(this.gameObject);
        }
    }
}
