using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnRope : MonoBehaviour
{
    public GameObject segmentTemplate;
    public GameObject fixedSegmentTemplate;
    public GameObject rope;
    public int segmentCount = 10;
    public bool trigger = true;
    

    public void setRope(Transform player, Transform target)
    {
        float adjustment = 0.5f;
        GameObject parent = Instantiate(rope, transform);
        Vector3 distance = target.position - player.position;
        float step = distance.magnitude/(float) segmentCount;
        distance.Normalize();

        Vector3 spawnPosition = player.position + distance * step;
        GameObject anchor = player.GetChild(1).gameObject;
        GameObject segment = Instantiate(
            segmentTemplate, 
            spawnPosition, 
            Quaternion.identity,
            parent.transform
        );
        segment.GetComponent<JointBreak>().SetTarget(target.gameObject);

        segment.GetComponentInChildren<SpringJoint2D>().distance = adjustment * step;
        segment.GetComponentInChildren<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
 
        for (int index = 2; index < segmentCount; index++)
        {
            spawnPosition = player.position + distance * step * (float)index;
            anchor = segment;
            segment = Instantiate(
                segmentTemplate, 
                spawnPosition, 
                Quaternion.identity,
                parent.transform
            );
            segment.GetComponent<JointBreak>().SetTarget(target.gameObject);
            segment.GetComponentInChildren<SpringJoint2D>().distance = adjustment * step;
            segment.GetComponentInChildren<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
            anchor = segment;
        }
        spawnPosition = target.position;
        anchor = segment;
        segment = Instantiate(
             fixedSegmentTemplate,
             spawnPosition,
             Quaternion.identity,
             target.transform
         );
        segment.GetComponentInChildren<SpringJoint2D>().distance = adjustment * step;
        segment.GetComponentInChildren<SpringJoint2D>().connectedBody = anchor.GetComponent<Rigidbody2D>();
    }
}
