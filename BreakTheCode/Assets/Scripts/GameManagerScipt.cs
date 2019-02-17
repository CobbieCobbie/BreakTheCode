using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScipt : MonoBehaviour
{
    InputController inputController;
    SpawnRope ropes;

    private void Start()
    {
        ropes = GetComponent<SpawnRope>();
        inputController = GetComponent<InputController>();
        Cursor.visible = false;
    }

    public void register(GameObject controllable)
    {
        inputController.register(controllable);
    }
}
