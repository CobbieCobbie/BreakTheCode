using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StayUnrotaed : MonoBehaviour
{
    Quaternion rotation;
    void Awake()
    {
        rotation = transform.rotation;
    }
    void LateUpdate()
    {
        // transform.rotation = rotation;
        transform.position = transform.parent.position;
    }
}
