using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shot : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddForce(speed * transform.up, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D target = collision.collider;
        if (target.tag == "Controllable")
        {
            GameObject
                .FindGameObjectWithTag("GameController")
                .GetComponent<GameManagerScipt>()
                .register(target.gameObject);
        }
        GameObject.Destroy(gameObject);
    }
}
