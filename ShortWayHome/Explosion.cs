using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
	Animator anim;

    void Start()
    {
		anim = GetComponent<Animator>();
	}

    void Update()
    {
		// destroy the game object if the explosion has finished its animation
		if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1)
        {
			Destroy(gameObject);
		}
	}
}
