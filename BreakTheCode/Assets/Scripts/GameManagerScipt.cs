using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScipt : MonoBehaviour
{
    InputController inputController;
    private void Start()
    {
        inputController = GetComponent<InputController>();
        Cursor.visible = false;
    }

    public void register(GameObject controllable)
    {
        inputController.register(controllable);
    }
}
