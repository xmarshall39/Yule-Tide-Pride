using UnityEngine;

public class LineOfSight : MonoBehaviour
{
    public delegate void Detected();
    public static event Detected OnDetected;
    public Transform target;

    [SerializeField]
    private bool killEnabled = true; //DEBUG ONLY

    private void Start()
    {
        InvokeRepeating("ShootRaycast", 0, .09f);
    }

    private void Update()
    {
        transform.LookAt(target);
    }


    //invisible cone shaped object and do collision testing on that.
    //Then if any collisions come up positive do a ray from the enemy to the player
    //to make sure its an actual line-of-sight.
    void DrawColoredRay(Color col)
    {
        Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * 20, col);
    }



    public void ShootRaycast()
    {
        Debug.Log("Shooting Raycast");
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, 20))
        {
            if (hit.transform.gameObject.tag == "Player")
            {
                DrawColoredRay(Color.red);
                if (killEnabled) OnDetected();
                Debug.Log("Found Player");
            }

            else { DrawColoredRay(Color.black); }


        }

        else
        {
            DrawColoredRay(Color.blue);
        }
    }



}
