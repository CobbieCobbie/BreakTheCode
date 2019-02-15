using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
	//Input
	private bool mouseInputReceived;

    //Movement

    public float speed = 0.25f;
    public float x = 0.0f, y = 0.0f;
    public Transform aim;

    //States
    public bool player_active = true, npc_active = false;

    //Screen
    private float screenWidth;


    //Random Movement, copied
	private float nextDecision = 0.0f, coolDownDecisionFor = 1.0f;

	private Rigidbody2D rb;
	private new Transform transform;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void handleMovement () {

    }

    private bool CheckMouseInput()
        {
            //return false;
            mouseInputReceived = false;

            //Mouse Input
            if (Input.GetMouseButtonDown(0))
            {
                mouseInputReceived = true;
                Hit(Input.mousePosition);
                Click(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0))
            {
                mouseInputReceived = true;
                OnRelease(Input.mousePosition);
                isDragging = false;
                //Destroy(_pointer);
                if (Input.mousePosition.x < _screenWidth / 2)
                {
                    this.InputReleaseEvent.Invoke();
                }
            }
            else if (isDragging)
            {
                Dragging(Input.mousePosition);
            }
            return mouseInputReceived;
        }
}
