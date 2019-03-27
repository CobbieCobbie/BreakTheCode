using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
    public float interpolation;
    private GameObject aim;
	private GameController gameController;
    private static float followSpeed = 4.0f;
    private Vector3 offset;

    void Start(){
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameController>();
        aim = GameObject.FindGameObjectWithTag("Aim");
    }

    void LateUpdate()
    {
        Transform active = gameController.getSelectedControllable();
        Vector2 direction = aim.transform.position - active.position;
        Vector2 position = (Vector2) active.position + 0.2f * direction;
    	transform.position = Vector3.Lerp(transform.position, new Vector3(position.x, position.y, transform.position.z), interpolation);
    }
}