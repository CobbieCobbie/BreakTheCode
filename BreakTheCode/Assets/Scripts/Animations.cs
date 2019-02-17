using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    Animator animator;
    private Rigidbody2D rigidbody;
    public bool walking = true;

    private void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody2D>();
    }
    // Update is called once per frame
    void Update()
    {
        animator.SetBool("walking", rigidbody.velocity.magnitude >= 0.01f);
    }
    

    public void letsIdle()
    {
        walking = false;
    }
}
