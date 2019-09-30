using UnityEngine;

//ends the level and brings player to victory screen
public class Goal : MonoBehaviour
{
    public delegate void ClearLevel();
    public static event ClearLevel OnClearLevel;
    [SerializeField]
    private bool clearable = true; //DEBUG ONLY

    private void Update()
    {
        RaycastHit goalCheck;

        if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.back), out goalCheck))
        {
            Debug.Log("Goal Reached");
            gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
            if (clearable) OnClearLevel?.Invoke();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject.name);
        if(other.tag == "Player")
        {
            Debug.Log("Goal Collision");
            if (clearable) OnClearLevel?.Invoke();
            else Debug.Log("Level Set to unclearable for testing in \"Goal\" script");
        }
    }
}
