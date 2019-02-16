using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    private static float followSpeed = 4.0f;
    public Vector3 offset;
    private Vector3 cameraPosition;
    private Transform player;
    private Transform aim;

    void start(){
        offset = transform.position - player.transform.position;
        cameraPosition = new Vector3(0,0,-10.0f);
    }

    /*void Update()
    {
        cameraPosition.x = player.transform.position.x;// + offset.x;
        cameraPosition.y = player.transform.position.y;// + offset.y;
        this.transform.position = cameraPosition;

        //TODO mal anschauen --> transform.position = Vector3.SmoothDamp(transform.position,)
    }*/

    void LateUpdate(){
        transform.position = player.transform.position + offset;
    }

}