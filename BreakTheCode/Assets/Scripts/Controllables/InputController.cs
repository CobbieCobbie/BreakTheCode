using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    //Movement
    private bool controlAll = true;
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
    private Transform transform;

    //GameModel.txt
    private GameObject[] controllables = new GameObject[0];
    private GameObject
        player,
        selectedNPC;
    private Transform aim;
    
    void Start()
    {
        aim = GameObject.FindGameObjectWithTag("Aim").transform;
        player = GameObject.FindGameObjectWithTag("Player");
        selectedNPC = null;
        moveDirection = Vector2.zero;
        mouseInput = Vector2.zero;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
    }

    void Update()
    {
        mouseInput = aim.position;
        HandleModes();
        HandleMovement();
        HandleAction();
    }

    public void HandleModes()
    {
        if (selectedNPC && Input.GetKey(KeyCode.Space))
        {
            controlAll = false;
        }
        else if (!Input.GetKey(KeyCode.Space))
        {
            controlAll = true;
            player.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    private void HandleMovement()
    {
        if(controlAll)
        {
            MoveAll();
            aim.up = player.transform.up;
        }
        else
        {
            ClearMovement();
            Move(selectedNPC);
            aim.up = selectedNPC.transform.up;
        }
    }

    private void HandleAction()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (controlAll)
            {
                player.GetComponent<Shoot>().web();
            }
            else
            {
                // Here goes optional NPC Actions
            }
        }

        if (!controlAll && Input.GetKeyDown(KeyCode.Mouse1))
        {
            for (int i = 0; i < controllables.Length; i++)
            {
                if (controllables[i].Equals(selectedNPC))
                {
                    Debug.Log("Right click " + selectedNPC);
                    selectedNPC = controllables[
                        (i+1)%controllables.Length
                    ];
                    break;
                }
            }
        }
    }

    private void ClearMovement()
    {
        if (!controlAll)
        {
            player.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
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

        controllable.GetComponent<Rigidbody2D>().velocity = Mathf.Abs(calcDirection().magnitude) < 0.1f
            ? Vector2.zero
            : moveDirection * speed;
    }

    private Vector2 walkingDirection(GameObject controllable)
    {
        if (!controlAll || controllable == player)
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
        return controlAll
            ? mouseInput - (Vector2) player.transform.position 
            : mouseInput - (Vector2) selectedNPC.transform.position;
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
        GameObject[] templates = new GameObject[controllables.Length + 1];
        templates[0] = controllable;
        int i = 1;
        foreach (GameObject piece in controllables)
        {
            templates[i] = piece;
            i++;
        }
        controllables = templates;
    }
    internal void unregister(GameObject unregisterControllable)
    {
        ClearMovement();
        unregisterControllable.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        GameObject[] templates = new GameObject[controllables.Length - 1];
        int i = 0;
        if (controllables.Length > 1) {
            foreach (GameObject controllable in controllables)
            {
                if (controllable != unregisterControllable)
                {
                    templates[i] = controllable;
                    i++;
                }
            }
        }
        controllables = templates;
        if (selectedNPC == unregisterControllable)
        {
            selectedNPC = null;
            if (!controlAll)
            {
                controlAll = true;
            }
        }
        if (controlAll)
        {
            player.GetComponent<Rigidbody2D>().isKinematic = false;
        }
    }

    public void selectNPC(GameObject controllable)
    {
        selectedNPC = controllable;
        controlAll = true;
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

    public Transform getSelectedControllable()
    {
        if (controlAll)
        {
            return player.transform;
        } else
        {
            return selectedNPC.transform;
        }
    }

    private void bindControllables(GameObject player, GameObject[] controllables)
    {
        this.controllables = GameObject.FindGameObjectsWithTag("Controllables");
        this.player = GameObject.FindGameObjectWithTag("Player");
    }
}
