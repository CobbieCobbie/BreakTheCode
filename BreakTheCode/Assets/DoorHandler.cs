using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandler : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    public Sprite spriteNotGoal, spriteGoal;
    private bool animateNotGoal = false;
    private bool animateGoal = false, animate = false;
    private float animationTime = 2F, animateUntil = 0.0f;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        if (animate && animateUntil < Time.time)
        {
            Debug.Log("Something fishy");
            spriteRenderer.sprite = null;
            animateNotGoal = false;
            animateGoal = false;
            animate = false;
        }
        if (!animate && animateUntil < Time.time)
        {
            if (animateNotGoal)
            {
                animateUntil += Time.time + animationTime;
                animate = true;
                spriteRenderer.sprite = spriteNotGoal;
            }
            else if (animateGoal)
            {
                animateUntil += Time.time + animationTime;
                animate = true;
                spriteRenderer.sprite = spriteGoal;
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.name == "Card")
        {
            animateGoal = true;
        } else
        {
            animateNotGoal = true;
        }
    }
}
