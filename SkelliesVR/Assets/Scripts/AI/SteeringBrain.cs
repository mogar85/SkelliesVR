using System;
using System.Collections.Generic;
using UnityEngine;

public class SteeringBrain : MonoBehaviour
{
    [SerializeField]
    Rigidbody body;
    [SerializeField]
    float sightRangeShort, sightRangeLong, sightArchShort, sightArchLong, precisionShort, precisionLong, movSpeed;




    private void Start()
    {
    }

    private void FixedUpdate()
    {
        if (!body)
            return;



    }

    private Quaternion? GetTargetDir()
    {
        List<int> freeAngles = new List<int>();
        for (int i = 0; i < 360; i++)
        {
            if (Scan(sightRangeShort, 1, 1, Vector3.forward + Vector3.right * i).Length == 0)
            {
                freeAngles.Add(i);
            }
        }
        int targetAngle = freeAngles[freeAngles.Count / 2];
        return Quaternion.Euler(0, targetAngle, 0);
    }

    private GameObject[] Scan(float range, float arch, float angleInc, Vector3 dir)
    {
        List<GameObject> scannedObjects = new List<GameObject>();
        int scanTimes = Mathf.RoundToInt(arch / angleInc);
        float angle = -arch / 2;
        Vector3 direction = Quaternion.AngleAxis(angle, transform.up) * dir;
        RaycastHit hit;

        for (int i = 0; i < scanTimes; i++)
        {
            direction = Quaternion.AngleAxis(angle, transform.up) * dir;
            Debug.DrawRay(transform.position, direction * range, Color.white, 1, true);
            if (Physics.Raycast(transform.position, direction, out hit, range))
            {
                if (!scannedObjects.Contains(hit.collider.gameObject))
                {
                    scannedObjects.Add(hit.collider.gameObject);
                }
            }
            angle += angleInc;
        }

        scannedObjects.ForEach(x => print(x.name));
        return scannedObjects.ToArray();
    }

    private bool CheckWallForward()
    {
        return Scan(sightRangeShort, sightArchShort, precisionShort, transform.forward).Length != 0;
    }

    private bool CheckWallLeft()
    {
        return Scan(sightRangeShort, 2, 1, Vector3.Lerp(transform.forward, -transform.right, .5f)).Length != 0;
    }

    private bool CheckWallRight()
    {
        return Scan(sightRangeShort, 2, 1, Vector3.Lerp(transform.forward, transform.right, .5f)).Length != 0;
    }

    private void MoveForward()
    {
        body.velocity = (transform.forward * movSpeed * Time.deltaTime);
    }

    private void SlowDown()
    {
        body.velocity = Vector3.Lerp(body.velocity, Vector3.zero, .5f);
    }

    private void RotateBody(float angle)
    {
        Quaternion newRot = body.rotation * Quaternion.Euler(0, angle, 0);
        body.MoveRotation(newRot);
    }

}
