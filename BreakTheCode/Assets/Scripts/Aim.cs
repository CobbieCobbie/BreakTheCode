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
    private Vector3 getPosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
