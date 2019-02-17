using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        transform.position = getPosition();
    }
    private Vector2 getPosition()
    {
        return (Vector2) Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
