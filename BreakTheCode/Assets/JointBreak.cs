using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JointBreak : MonoBehaviour
{
    private void OnJointBreak2D(Joint2D joint)
    {
        Debug.Log("Something broke in " + joint);
    }
}
