using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Player sript for turning invisible
public class InvisDriver : MonoBehaviour
{
    public float transparency;
    public float duration;

    //When recieve the turn invisible signal:
    //Activate inisible effect (of some kind)
    //Change Player tag to PlayerInvisible
    //This will prevent interactions with anything that relies on its tag
    //If we want tag based interaction regardless of visibilty
    //We check if that tag.Contains("Player") rather than == "Player"
    //This will only be relevant on things like the goal
    private Material playerMaterial;
    private Color originalColor;
    private Color transparentColor;

    private void Start()
    {
        playerMaterial = gameObject.GetComponent<Renderer>().material;
        originalColor = playerMaterial.GetColor("_Color");
        transparentColor = new Color(originalColor.r, originalColor.g, originalColor.b, transparency);
    }


    public void ToggleInvisibility()
    {
        StartCoroutine(InvisibilityDriver());
    }

    private void OnEnable() {InvisibilityPower.OnInvisibility += ToggleInvisibility;}
    private void OnDisable() { InvisibilityPower.OnInvisibility -= ToggleInvisibility;}

    private void InvisibleEffect()
    {
        playerMaterial.SetColor("_Color",transparentColor);
        Debug.Log(transparentColor);
    }

    private void RevertEffect()
    {
        playerMaterial.SetColor("_Color", originalColor);
        Debug.Log(originalColor);
    }

    private IEnumerator InvisibilityDriver()
    {
        gameObject.tag = "PlayerInvisible";
        InvisibleEffect();
        yield return new WaitForSeconds(duration);
        RevertEffect();
        gameObject.tag = "Player";
    }


}
