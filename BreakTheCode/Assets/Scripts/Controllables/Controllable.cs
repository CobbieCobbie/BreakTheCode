using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controllable : MonoBehaviour
{

    public static int count = 0;
    public int id = 0;
    public string type = "";

    void Action() {
        switch (type) {
            case "shooter":
                ShooterAction();
                break;
            default:
                Debug.Log("DoNothing");
                break;
        }
    }

    void ShooterAction()
    {
        Debug.Log("SHoot that motherfather");
    }
}
