using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    public Sprite card, arrow;
    InputController inputController;
    Transform active;
    private int radius = 8;
    SpriteRenderer cursorRenderer;
    bool cardControllable = false;

    private void Start()
    {
        inputController = GameObject.FindGameObjectWithTag("GameController").GetComponent<InputController>();
        cursorRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        active = inputController.getSelectedControllable();
        transform.position = getPosition();
        if (active.name == "Controllable-Key")
        {
            cursorRenderer.sprite = card;
        }
        else
        {
            cursorRenderer.sprite = arrow;
        }
    }

    private Vector2 getPosition()
    {
        Vector2 position = (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = position - (Vector2) active.position;
        if (direction.magnitude > radius)
        {
            direction = direction.normalized * radius;
        }
        position = (Vector2) active.position + direction;
        return position;
    }
}
