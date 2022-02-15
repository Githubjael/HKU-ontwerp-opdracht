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
        var Xposition = gameObject.transform.position.x + OffsetX;
        var Yposition = gameObject.transform.position.y + OffsetX;
        var Zposition = gameObject.transform.position.z + OffsetX;
        gameObject.transform.position = new Vector3(Xposition, Yposition, Zposition);
    }
}
