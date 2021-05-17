using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimation : MonoBehaviour
{
    Animator anim;
    
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
    }

    void FixedUpdate()
    {
        if (Input.GetAxis("Horizontal") > 0||(Input.GetAxis("Vertical")>0&&Input.GetAxis("Horizontal")==0))
        {
            anim.SetBool("isRunningRight", true);
            anim.SetBool("isRunningLeft", false);
        }
        else if (Input.GetAxis("Horizontal") < 0 || (Input.GetAxis("Vertical") < 0 && Input.GetAxis("Horizontal") == 0))
        {
            anim.SetBool("isRunningRight", false);
            anim.SetBool("isRunningLeft", true);
        }
        else if (Input.GetAxis("Horizontal")==0&&Input.GetAxis("Vertical")==0)
        {
            anim.SetBool("isRunningRight", false);
            anim.SetBool("isRunningLeft", false);
        }
    }
}
