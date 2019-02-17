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
    public GameObject[] controllables;
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
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput = aim.position;
        HandleModes();
        HandleMovement();
        HandleAction();
    }

    private void HandleAction()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (modePlayer())
            {
                player.GetComponent<Shoot>().web();
            }
        }
    }

    private void HandleMovement()
    {
        if(modeAll())
        {
            MoveAll();
            aim.up = player.transform.up;
        }
        else if (modeSelected())
        {
            ClearMovement();
            Move(selected);
            aim.up = selected.transform.up;
        }
        else
        {
            ClearMovement();
            Move(player);
            aim.up = player.transform.up;
        }
    }

    private void ClearMovement()
    {
        if (modeSelected())
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach (GameObject controllable in controllables)
            {
                controllable.GetComponent<Rigidbody2D>();
            }
        } else if (modePlayer())
        {
            if (selected)
            {
                selected.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            }
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

    public void register(GameObject controllable)
    {
        if(!isRegistered(controllable))
        {
            addToRegistry(controllable);
        }
        selected = controllable;
        mode = "SELECTED";
    }

    private void addToRegistry(GameObject controllable)
    {
        GameObject[] templates = new GameObject[controllables.Length + 1];
        int i = 0;
        templates[i] = controllable;
        foreach (GameObject piece in controllables)
        {
            i++;
            templates[i] = piece;
        }
        controllables = templates;
    }

    private bool isRegistered(GameObject controllable)
    {
        foreach (GameObject piece in controllables)
        {
            if (piece == controllable)
            {
                Debug.Log("Oh, I know this one!");
                return true;
            }
        }
        Debug.Log("Whos dat?");
        return false;
    }

    private void HandleModes()
    {
        float mouseWheelAxis = Input.GetAxis("Mouse ScrollWheel");
        if (modeAll() && selected && mouseWheelAxis < 0)
        {
            if (selected)
            {
                mode = "SELECTED";
            }
            else
            {
                mode = "PLAYER";
            }
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
            if (selected)
            {
                mode = "SELECTED";
            }
            else
            {
                mode = "PLAYER";
            }
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
}
