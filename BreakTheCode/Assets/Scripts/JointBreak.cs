using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour
{
    GameObject target;
    public void SetTarget(GameObject controllable)
    {
        target = controllable;
    }

    private void OnJointBreak2D(Joint2D joint)
    {
        Debug.Log("Something broke in " + joint);
        GameObject.FindGameObjectWithTag("GameController");
        transform.parent.parent.gameObject.GetComponent<GameManagerScript>().unregister(target);
        GameObject.Destroy(joint.transform.parent.gameObject);
    }
}
