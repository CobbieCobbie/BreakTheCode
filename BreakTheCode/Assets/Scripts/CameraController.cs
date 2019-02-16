using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    //Screen constraints
    private float screenWidth, screenHeight;
    private Vector3 screenCenter;
    private GameObject sceneCam;

    public float followSpeed = 1.0f;
    //private Vector3 direction;
    public Transform player;
    public Transform aim;
    private Transform transform;
    Vector2 screenBounds, screenOrigo;
    

    void Start(){
        //direction = Vector3.zero;

        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenOrigo = Camera.main.ScreenToWorldPoint(Vector2.zero);
        screenWidth = Screen.width;
        screenHeight = Screen.height;
        screenCenter = new Vector3((screenBounds.x - screenOrigo.x) / 2 , (screenBounds.y - screenOrigo.y) / 2 , -10);
        transform = GetComponent<Transform>();
        Debug.Log("Height: " + screenHeight  + ", Width: " + screenWidth);
    }

    void Update(){
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
        screenOrigo = Camera.main.ScreenToWorldPoint(Vector2.zero);
        screenCenter = new Vector3((screenBounds.x - screenOrigo.x) / 2 , (screenBounds.y - screenOrigo.y) / 2 , -10);

        if (Vector3.Distance(transform.position,screenCenter) < ((screenBounds.y - screenOrigo.y) / 2))
        {
            Vector3 direction = calcDirection();
            Vector3 move = new Vector3(direction.x * followSpeed, direction.y * followSpeed, -10);
            move.Normalize();
            transform.Translate(move * Time.deltaTime * followSpeed);
        }
        //TODO mal anschauen --> transform.position = Vector3.SmoothDamp(transform.position,)
    }

    /*void LateUpdate(){
        Vector3 newPosition = Vector3.zero;
        calcMean();
        newPosition.x = transform.position.x + mean.x;
        newPosition.y = transform.position.y + mean.y;
        this.transform.position = newPosition;
    }*/

    private Vector3 calcDirection(){
        return new Vector3(aim.position.x - player.position.x,aim.position.y - player.position.y,0);
    }
}