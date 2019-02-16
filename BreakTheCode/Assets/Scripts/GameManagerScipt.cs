using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerScipt : MonoBehaviour
{
    HashSet<Connection> connections = new HashSet<Connection>();
    Controller inputController;

    void Start()
    {
        inputController = GetComponent<Controller>();
        GameObject[] templates = new GameObject[GameObject.FindGameObjectsWithTag("Connection").Length];
        int index = 0;
        foreach (GameObject template in templates)
        {
            connections.Add(template.GetComponent<Connection>());
        }
    }
}
