using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterController : MonoBehaviour
{
    Rigidbody2D rigidbody;
    public float speed = 0.0f;
    HashSet<Controllable> controllables = new HashSet<Controllable>();
    Controllable active;

    bool moveAll = false;

    void Start()
    {
        GameObject[] templates = GameObject.FindGameObjectsWithTag("Controllable");
        int index = 0;
        controllables = new HashSet<Controllable>();
        foreach (GameObject template in templates)
        {
            controllables.Add(template.GetComponent<Controllable>());
            Debug.Log(template.name);
            if(template.name == "Player")
            {
                active = template.GetComponent<Controllable>();
                rigidbody = template.GetComponent<Rigidbody2D>();
            }
        }
    }

    public void Control()
    {
        float horizontal = Input.GetAxis("Horizontal"),
            vertical = Input.GetAxis("Vertical");

        rigidbody.velocity = speed * new Vector2(horizontal, vertical);
    }
}
