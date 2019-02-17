using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{


    //private Controllable active;
    //private Controllable[] controllables;

    //Movement
    public float speed = 0.5f;
    public float x = 0.0f, y = 0.0f;

    //Mouse input
    public Transform aim;
    public Vector2 mouseInput;
    public Vector2 direction;
    public Vector2 moveDirection;
    
    //Screen
    private float screenWidth, screenHeight;


    //Random Movement, copied
    //private float nextDecision = 0.0f, coolDownDecisionFor = 1.0f;

    private Rigidbody2D rigidbody;
    private new Transform transform;

    //GameModel.txt
    private GameObject[] controllables;
    public GameObject
        player,
        selected;
    public string mode = "PLAYER";

    // Start is called before the first frame update
    void Start()
    {
        moveDirection = Vector2.zero;
        mouseInput = Vector2.zero;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        controllables = GameObject.FindGameObjectsWithTag("Controllable");
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput = aim.position;
        ChooseModes();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if(modeAll())
        {
            MoveAll();
        }
        else if (modeSelected())
        {
            ClearMovement();
            Move(selected);
        }
        else
        {
            ClearMovement();
            Move(player);

        }
    }

    private void ClearMovement()
    {
        if (modeSelected())
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach (GameObject controllable in controllables)
            {
                if(selected != controllable)
                {
                    controllable.GetComponent<Rigidbody2D>();
                }
            }
        } else if (modePlayer())
        {
            selected.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach (GameObject controllable in controllables)
            {
                if (selected != controllable)
                {
                    controllable.GetComponent<Rigidbody2D>();
                }
            }
        }
    }

    private void MoveAll()
    {
        Move(player);
        foreach (GameObject controllable in controllables)
        {
            controllable.transform.rotation = player.transform.rotation;
            Move(controllable);
        }
    }

    void Move(GameObject controllable)
    {
        moveDirection = Vector2.zero;
        Vector2 dir =  walkingDirection(controllable);
        if (Input.GetKey(KeyCode.W))
        {
            moveDirection += dir;
        }
        if (Input.GetKey(KeyCode.A))
        {
            moveDirection += Left(dir);
        }
        if (Input.GetKey(KeyCode.S))
        {
            moveDirection += -dir;
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Right(dir);
        }
        if (player == controllable && Input.GetKey(KeyCode.Space))
        {
            // Action();
        }
        moveDirection.Normalize();
        controllable.transform.up = walkingDirection(controllable);
        controllable.GetComponent<Rigidbody2D>().velocity = moveDirection * speed;
    }

    private Vector2 walkingDirection(GameObject controllable)
    {
        if (modeSelected() || controllable == player)
        {
            return calcDirection();
        }
        else
        {
            return player.transform.up;
        }
    }

    private void ChooseModes()
    {
        float mouseWheelAxis = Input.GetAxis("Mouse ScrollWheel");
        if (modeAll() && mouseWheelAxis < 0)
        {
            mode = "SELECTED";
        }
        else if (modeSelected() && mouseWheelAxis > 0)
        {
            mode = "ALL";
        }
        else if (modeSelected() && mouseWheelAxis < 0)
        {
            mode = "PLAYER";
        }
        else if (modePlayer() && mouseWheelAxis > 0)
        {
            mode = "SELECTED";
        }
    }

    private void bindControllables(GameObject player, GameObject[] controllables)
    {
        this.controllables = GameObject.FindGameObjectsWithTag("Controllables");
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    private bool modeSelected()
    {
        return mode == "SELECTED";
    }
    private bool modeAll()
    {
        return mode == "ALL";
    }
    private bool modePlayer()
    {
        return mode == "PLAYER";
    }
    
    private Vector2 calcDirection()
    {     
        return modeSelected()
            ? mouseInput - (Vector2) selected.transform.position
            : mouseInput - (Vector2) player.transform.position;
    }

    private Vector2 Left(Vector2 dir)
    {
        return new Vector2(-dir.y, dir.x);
    }

    private Vector2 Right(Vector2 dir)
    {
        return new Vector2(dir.y, -dir.x);
    }
}
