using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamController : MonoBehaviour
{

    [SerializeField]
    float sensivity = 5;
    [SerializeField]
    float smoothing = 2;

    Vector2 mouseLook;
    Vector2 smoothV;

    GameObject character;

    // Use this for initialization
    void Start()
    {
        character = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mV = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
        mV = Vector2.Scale(mV, new Vector2(sensivity * smoothing, sensivity * smoothing));
        smoothV.x = Mathf.Lerp(smoothV.x, mV.x, 1 / smoothing);
        smoothV.y = Mathf.Lerp(smoothV.y, mV.y, 1 / smoothing);
        mouseLook += smoothV;

        transform.localRotation = Quaternion.AngleAxis(-mouseLook.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(mouseLook.x, character.transform.up);
    }
}
