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
        active;
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
        switch(mode)
        {
            case "ALL":
                MoveAll();
                break;
            case "SELECTED":
                SelectBody(active);
                MoveActive(active.transform);
                break;
            case "PLAYER":
                SelectBody(player);
                MoveActive(player.transform);
                break;
            default:
                break;
        }
    }
    

    private void MoveAll()
    {
        SelectBody(player);
        mouseInput = getMouseInput();
        faceToMouse();

        SelectBody(player);
        MoveActive(player.transform);
        foreach (GameObject controllable in controllables)
        {
            SelectBody(controllable);
            controllable.transform.rotation = player.transform.rotation;
            MoveActive(controllable.transform);
        }
    }

    void MoveActive(Transform active)
    {
        mouseInput = getMouseInput();
        faceToMouse();
        moveDirection = Vector2.zero;
        Vector2 dir =  walkingDirection();
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
            moveDirection += Down(dir);
        }
        if (Input.GetKey(KeyCode.D))
        {
            moveDirection += Right(dir);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            Action();
        }
        moveDirection.Normalize();
        rigidbody.velocity = moveDirection * speed;
    }

    private void Action()
    {
        //TODO
        Debug.Log("ok cool");
    }

    public void SelectBody(GameObject controllable)
    {
        if (controllable)
        {
            rigidbody = controllable.GetComponent<Rigidbody2D>();
            transform = controllable.GetComponent<Transform>();
        }
    }

    public void bindControllables(GameObject player, GameObject[] controllables)
    {
        this.controllables = GameObject.FindGameObjectsWithTag("Controllables");
        this.player = GameObject.FindGameObjectWithTag("Player");
    }

    private Vector3 getMouseInput()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void faceToMouse()
    {
        transform.up = calcDirection();
    }

    private Vector2 walkingDirection()
    {
        if (mode == "SELECTED" || active.Equals(player)) {
            return calcDirection();
        }
        else
        {
            return transform.up;
        }
    }

    private Vector2 calcDirection()
    {     
        return new Vector2(mouseInput.x - transform.position.x, mouseInput.y - transform.position.y);
    }

    private Vector2 Left(Vector2 dir)
    {
        return new Vector2(-dir.y, dir.x);
    }

    private Vector2 Right(Vector2 dir)
    {
        return new Vector2(dir.y, -dir.x);
    }

    private Vector2 Down(Vector2 dir)
    {
        return -dir;
    }
}
