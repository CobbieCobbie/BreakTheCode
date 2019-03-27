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
    public Transform door;

    private void Start()
    {
        spriteRenderer = gameObject.GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        if (animateGoal)
        {
            door.Translate(0.04f * Vector2.right);
        }
        if (animate && animateUntil < Time.time)
        {
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
        if (collision.name == "Controllable-Key")
        {
            animateGoal = true;
        }
        else
        {
            animateNotGoal = true;
        }
    }
}
