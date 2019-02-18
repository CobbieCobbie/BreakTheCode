using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Sprite card, arrow;
    SpriteRenderer spriteRenderer;
    bool cardControllable = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = getPosition();
        if (cardControllable)
        {
            spriteRenderer.sprite = card;
        } else
        {
            spriteRenderer.sprite = arrow;
        }
    }
    private Vector2 getPosition()
    {
        return (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void setCardTo(bool card)
    {
        cardControllable = card;
    }
}
