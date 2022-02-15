using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    public Transform LookatObject;
    public float OffsetX;
    public float OffsetY;
    public float OffsetZ;

    public void Update()
    {
        Follow();
        gameObject.transform.LookAt(LookatObject);
    }

    public void Follow()
    {
        gameObject.transform.position = new Vector3(LookatObject.position.x + OffsetX, LookatObject.position.y
            + OffsetY, LookatObject.position.z + OffsetZ);
    }
}
