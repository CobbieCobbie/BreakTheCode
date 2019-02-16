using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    //GameModel.txt
    private bool moveAll;
    private bool action;
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

	private Rigidbody2D rb;
	private new Transform transform;

    
    // Start is called before the first frame update
    void Start()
    {
    	moveDirection = Vector2.zero;
    	mouseInput = Vector2.zero;
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        transform = GetComponent<Transform>();
		rb = GetComponent<Rigidbody2D> ();
    }

    // Update is called once per frame
    void Update()
    {
    	mouseInput = getMouseInput();
        faceMouse();
        handleMovement();
    }

    void handleMovement () {
    	moveDirection = Vector2.zero;
    	Vector2 dir = calcDirection();
    	if (Input.GetKey(KeyCode.W)){
    		moveDirection += dir;
    	}
    	if (Input.GetKey(KeyCode.A)){
    		moveDirection += Left(dir);
    	}
    	if (Input.GetKey(KeyCode.S)){
    		moveDirection += Down(dir);
    	}
    	if (Input.GetKey(KeyCode.D)){
    		moveDirection += Right(dir);
    	}
    	if (Input.GetKey(KeyCode.Space)){
    		Action();
    	}
    	moveDirection.Normalize();
    	rb.velocity = moveDirection * speed;// = new Vector2(moveDirection.x * speed, moveDirection.y * speed);
    }

    private void Action(){
    	//TODO
    	Debug.Log("ok cool");
    }

    private Vector3 getMouseInput(){
    	return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    private void faceMouse(){
    	transform.up = calcDirection();
    }

    private Vector2 calcDirection(){
    	return new Vector2(mouseInput.x - transform.position.x, mouseInput.y - transform.position.y);
    }

    private Vector2 Left(Vector2 dir){
    	return new Vector2(-dir.y, dir.x);
    }

    private Vector2 Right(Vector2 dir){
    	return new Vector2(dir.y, -dir.x);
    }

    private Vector2 Down(Vector2 dir){
    	return new Vector2(dir.x, -dir.y);
    }
}
