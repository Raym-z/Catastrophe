using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animate : MonoBehaviour
{
    Animator animator;

    public float horizontal;
    public float vertical;


    private void Update()
    {
        animator.SetFloat("Horizontal", horizontal);
        // animator.SetFloat("Vertical", vertical);
        animator.SetBool("IsMoving", horizontal != 0 || vertical != 0);
    }

    internal void SetAnimate(GameObject animObject)
    {
        animator = animObject.GetComponent<Animator>();
    }

}
