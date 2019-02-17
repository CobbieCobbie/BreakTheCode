using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    public bool walking = true;

    private void Start()
    {
        animator = GetComponent<Animator>();    
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walking", walking);
    }

    public void letsWalk()
    {
        walking = true;
    }

    public void letsIdle()
    {
        walking = false;
    }
}
