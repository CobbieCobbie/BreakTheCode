using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRope : MonoBehaviour
{
    public GameObject fixedSegmentFixture,
        segmentFixture;
    public Transform player, target;
    public int segmentCount = 10;
    public bool trigger = true;
    private void Start()
    {
        
    }

    private void Update()
    {
        if (trigger)
        {
            setRope(player, target);
            trigger = false;
        }
        Debug.DrawLine(player.position, target.position);
    }

    public void setRope(Transform player, Transform target)
    {
        Vector3 distance = target.position - player.position;
        float step = distance.magnitude/(float) segmentCount;
        distance.Normalize();
        Debug.Log("Step" + step + " of " + distance.magnitude);
        Vector3 spawnPosition = player.position;
        GameObject segment = null;
        GameObject anchor = player.gameObject;

        for (int index = 1; index < segmentCount; index++) {
            spawnPosition = player.position + distance * step * index;
            segment = Instantiate(segmentFixture, spawnPosition, Quaternion.identity, GetComponent<Transform>());
            segment.GetComponent<Transform>().up = distance;
            anchor.GetComponent<Joint2D>().connectedBody = segment.GetComponent<Rigidbody2D>();
            anchor = segment;
        }
        spawnPosition = player.position + distance * step * segmentCount;
        segment = target.gameObject;
        anchor.GetComponent<Joint2D>().connectedBody = segment.GetComponent<Rigidbody2D>();
    }
}
