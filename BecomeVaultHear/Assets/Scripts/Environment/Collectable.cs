using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Collectables
{
    First,
    Second,
    Third
}

public class Collectable : MonoBehaviour
{
    //Can I just have one event with a parameter
    public delegate void Collection();
    public static event Collection OnFirstCollection;
    public static event Collection OnSecondCollection;
    public static event Collection OnThirdCollection;


    public Collectables Type;
    public Material baseMaterial;
    public Material ghostMaterial;

    [HideInInspector] public SaveData data;

    private bool alreadyCollected;
    private string currentScene;

    void Start()
    {
        data = new SaveMangement().data;
        currentScene = new SceneManagement().CurrentScene().ToString();


        if (!data.obtainedCollectables.ContainsKey(currentScene))
        {
            alreadyCollected = false;
        }
        else
        {
            alreadyCollected = data.obtainedCollectables[currentScene].Contains(Type);
        }

        //Has the player already collected this and cleared the level? [Check current data class]
        //In other words, is the collectable saved in the binary file?
        //If so, spawn a ghost prefab of the ornament
        if (alreadyCollected) GetComponent<Renderer>().material = ghostMaterial;

        else GetComponent<Renderer>().material = baseMaterial;
        //The only difference will be in the loaded material
        //This ghost will trigger collection effects, but not do much else
        Debug.Log(GetComponent<Renderer>().material.name);
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {

            switch (Type)
            {
                case Collectables.First:
                    OnFirstCollection?.Invoke();
                    Destroy(this.gameObject);
                    return;
                case Collectables.Second:
                    OnSecondCollection?.Invoke();
                    Destroy(this.gameObject);
                    return;
                case Collectables.Third:
                    OnThirdCollection?.Invoke();
                    Destroy(this.gameObject);
                    return;
            }

            //Trigger some despawn effect here. Instantiate some prefab//

        }

    }

    //What exists in the save binary?

}
