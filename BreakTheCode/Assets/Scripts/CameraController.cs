﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	//public GameObject player;
    private static float followSpeed = 4.0f;
    private Vector3 offset;

    void start(){
    	offset = transform.position - player.transform.position;
    }

    void LateUpdate()
    {
    	transform.position = player.transform.position + offset;
    }

}