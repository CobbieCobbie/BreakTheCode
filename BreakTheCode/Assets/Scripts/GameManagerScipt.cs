using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScipt : MonoBehaviour
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
        inputController.register(controllable);
        ropes.setRope(player.transform, controllable.transform);
    }
}
