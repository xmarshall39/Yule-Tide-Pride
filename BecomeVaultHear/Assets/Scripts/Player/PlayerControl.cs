using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    float oldPlayerDist;
    float oldStrafeDist;
    int strafePos;
    int strafingLimit;

    Animation anim;
    Animator animator;
    Transform playerTransform;
    AnimationCurve vaultCurve;
    AnimationClip testClip;

    // Start is called before the first frame update
    public void Start()
    {
        anim = GetComponent<Animation>();
        animator = GetComponent<Animator>();
        playerTransform = GetComponent<Transform>();
        oldPlayerDist = playerTransform.position.z;
        oldStrafeDist = playerTransform.position.x;
        strafePos = 0;
        strafingLimit = 1; //set to maximum places that can be strafed
    }

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space)) animator.SetTrigger("Vault");
        //if (Input.GetKeyDown(KeyCode.Space)) anim.SetBool("Vaulting", true);
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.W))
        {
            gameObject.GetComponentInParent<Mover>().ChangeParentLocation(oldPlayerDist);
            gameObject.GetComponentInParent<Mover>().StrafeParentLeft(oldStrafeDist); //new code for newer bug
            animator.Play("VaultFinal", -1, 0f);
        }
        if (Input.GetKeyDown(KeyCode.A) && strafePos != -strafingLimit)
        {
            gameObject.GetComponentInParent<Mover>().StrafeParentLeft(oldStrafeDist);
            gameObject.GetComponentInParent<Mover>().ChangeParentLocation(oldPlayerDist);
            animator.Play("StrafeLeft", -1, 0f);
            //gameObject.GetComponentInParent<Mover>().StrafeParentLeft();
            //animator.Play("StrafeLeft", -1, 0f); old location
            strafePos -= 1;
        }
        if (Input.GetKeyDown(KeyCode.D) && strafePos != strafingLimit)
        {
            gameObject.GetComponentInParent<Mover>().StrafeParentRight(oldStrafeDist);
            gameObject.GetComponentInParent<Mover>().ChangeParentLocation(oldPlayerDist);
            animator.Play("StrafeRight", -1, 0f);
            strafePos += 1;
        }
        
        
    }

    private void animationEnd()
    {
        //anim.SetBool("Vaulting", false);
        float newPlayerDist = oldPlayerDist + 7.0f;
        gameObject.GetComponentInParent<Mover>().ChangeParentLocation(oldPlayerDist);
        gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, newPlayerDist);
        //gameObject.GetComponentInParent<Transform>().position = new Vector3(transform.position.x, transform.position.y, newPlayerDist);
        //this.gameObject.transform.position = new Vector3(transform.position.x, transform.position.y, newPlayerDist);
        oldPlayerDist = newPlayerDist;
        print("Capsule's z position: " + gameObject.transform.position.z);
        print("Parent's z position: " + gameObject.GetComponentInParent<Transform>().transform.position.z);
    }

    private void LeftStrafeEnd()
    {
        float newStrafeDist = oldStrafeDist - 1f;
        gameObject.GetComponentInParent<Mover>().StrafeParentLeft(oldStrafeDist);
        //gameObject.transform.position = new Vector3(transform.position.x - 1, transform.position.y, transform.position.z);
        oldStrafeDist = newStrafeDist;
        print("Capsule's x position: " + gameObject.transform.position.x);
        print("Parent's x position: " + gameObject.GetComponentInParent<Transform>().transform.position.x);
    }

    private void RightStrafeEnd()
    {
        float newStrafeDist = oldStrafeDist + 1f;
        gameObject.GetComponentInParent<Mover>().StrafeParentRight(oldStrafeDist);
        oldStrafeDist = newStrafeDist;
        print("Capsule's x position: " + gameObject.transform.position.x);
        print("Parent's x position: " + gameObject.GetComponentInParent<Transform>().transform.position.x);
    }
}
