using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
	private Transform transform;
    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = getMouseInput();
    }

    private Vector3 getMouseInput(){
    	return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
}
