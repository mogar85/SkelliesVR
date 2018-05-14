using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NodeBrain : MonoBehaviour
{
    [SerializeField]
    Node currentNode;
    [SerializeField]
    float nodeDistanceThreshold = .2f;
    [SerializeField]
    Rigidbody body;
    [SerializeField]
    float speed = 10;
    [SerializeField]
    float rotSpeed = 10;

    float tempNodeDistanceThreshold;
    float currentSpeed;
    Vector3 prevPos;

    private void Start()
    {
        tempNodeDistanceThreshold = nodeDistanceThreshold;
    }

    // Update is called once per frame
    void Update()
    {
        if (currentNode == null || currentNode.Connections == null || currentNode.Connections.Length == 0)
            return;

        if (DistanceToNode() < tempNodeDistanceThreshold)
            GetNewTargetNode();


        MoveTowardsNode(currentNode.transform.position);
        CalcCurrentSpeed();
        tempNodeDistanceThreshold += Time.deltaTime;

    }

    private void CalcCurrentSpeed()
    {
        currentSpeed = Vector3.Distance(body.transform.position, prevPos);
        prevPos = body.transform.position;
        print(currentSpeed * speed);
    }

    private void MoveTowardsNode(Vector3 point)
    {

        if (RotateTowards(new Vector3(point.x, 0, point.z)))
            body.velocity = transform.forward.normalized * speed;
    }

    private bool RotateTowards(Vector3 point)
    {
        Vector3 pointB = new Vector3(body.transform.position.x, 0, body.transform.position.z);

        Quaternion targetRot = Quaternion.LookRotation((point - pointB).normalized);
        body.MoveRotation(Quaternion.Lerp(body.rotation, targetRot, .5f));

        if (Mathf.Abs(body.rotation.eulerAngles.y - targetRot.eulerAngles.y) < 10)
        {
            return true;
        }
        return false;
    }

    private float DistanceToNode()
    {
        return Vector3.Distance(body.transform.position, currentNode.transform.position);
    }

    private void GetNewTargetNode()
    {
        currentNode = currentNode.Connections[UnityEngine.Random.Range(0, currentNode.Connections.Length)];
        tempNodeDistanceThreshold = nodeDistanceThreshold;
    }
}
