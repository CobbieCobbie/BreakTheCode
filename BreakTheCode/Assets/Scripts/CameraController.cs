using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    public float followSpeed = 4.0f;
    //private Vector3 direction;
    public Transform player;
    public Transform aim;
    private Transform transform;

    void start(){
        //direction = Vector3.zero;
        transform = GetComponent<Transform>();
    }

    void LateUpdate(){
        Vector3 direction = calcDirection();
        Vector3 move = new Vector3(direction.x * followSpeed, direction.y * followSpeed, 0);
        transform.Translate(move , Space.Self);
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
        return (aim.position - player.position);
    }

}