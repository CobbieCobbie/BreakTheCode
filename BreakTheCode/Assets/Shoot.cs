using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public GameObject webShot;
    public Transform spawnWeb;

    public void web()
    {
        Instantiate(webShot, spawnWeb.position, transform.rotation, null);
    }
}
