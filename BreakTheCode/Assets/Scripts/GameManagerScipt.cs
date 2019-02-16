using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScipt : MonoBehaviour
{
    HashSet<Connection> connections = new HashSet<Connection>();
    CharacterController characterController;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        GameObject[] templates = new GameObject[GameObject.FindGameObjectsWithTag("Connection").Length];
        int index = 0;
        foreach (GameObject template in templates)
        {
            connections.Add(template.GetComponent<Connection>());
        }
    }

    private void Update()
    {
        characterController.Control();
    }
}
