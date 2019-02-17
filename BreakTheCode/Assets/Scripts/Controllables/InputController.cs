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
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (isModePlayer())
            {
                player.GetComponent<Shoot>().web();
            }
        }
    }

    private void HandleMovement()
    {
        if(isModeAll())
        {
            MoveAll();
            aim.up = player.transform.up;
        }
        else if (isModeSelected())
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
        if (isModeSelected())
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            foreach (GameObject controllable in controllables)
            {
                controllable.GetComponent<Rigidbody2D>();
            }
        }
        else if (isModePlayer())
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

        controllable.GetComponent<Rigidbody2D>().velocity = Mathf.Abs(calcDirection().magnitude)< 0.1f
            ? Vector2.zero
            : moveDirection * speed;
    }

    private Vector2 walkingDirection(GameObject controllable)
    {
        if (isModeSelected() || controllable == player)
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
        return isModeSelected()
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
        addToRegistry(controllable);
    }

    public void activate(GameObject controllable)
    {
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

    public bool isRegistered(GameObject controllable)
    {
        foreach (GameObject piece in controllables)
        {
            if (piece == controllable)
            {
                return true;
            }
        }
        return false;
    }

    private void HandleModes()
    {
        float mouseWheelAxis = Input.GetAxis("Mouse ScrollWheel");
        if (isModeAll() && selected && mouseWheelAxis < 0)
        {
            if (selected)
            {
                mode = "SELECTED";
            }
            else
            {
                mode = "PLAYER";
                player.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
        else if (isModeSelected() && mouseWheelAxis > 0)
        {
            mode = "ALL";
            player.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else if (isModeSelected() && mouseWheelAxis < 0)
        {
            mode = "PLAYER";
            player.GetComponent<Rigidbody2D>().isKinematic = false;
        }
        else if (isModePlayer() && mouseWheelAxis > 0)
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

    private bool isModeSelected()
    {
        return mode == "SELECTED";
    }
    private bool isModeAll ()
    {
        return mode == "ALL";
    }
    private bool isModePlayer()
    {
        return mode == "PLAYER";
    }
}
