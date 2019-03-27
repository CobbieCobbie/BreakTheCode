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
        GameObject.FindGameObjectWithTag("GameController");
        transform.parent.parent.gameObject.GetComponent<GameController>().unregister(target);
        GameObject.Destroy(target.transform.GetChild(0).gameObject);
        GameObject.Destroy(joint.transform.parent.gameObject);
    }
}
