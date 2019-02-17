using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScript : MonoBehaviour
{
    InputController inputController;
    SpawnRope ropes;
    GameObject player;

    private void Start()
    {
        ropes = GetComponent<SpawnRope>();
        inputController = GetComponent<InputController>();
        player = GameObject.FindGameObjectWithTag("Player");
        Cursor.visible = false;
    }

    public void register(GameObject controllable)
    {
        if (!inputController.isRegistered(controllable))
        {
            inputController.register(controllable);
            ropes.setRope(player.transform, controllable.transform);
        }
        inputController.activate(controllable);
    }

    public void unregister(GameObject controllable)
    {
        inputController.unregister(controllable);
    }
}
