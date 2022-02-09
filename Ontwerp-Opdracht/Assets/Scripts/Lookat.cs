using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lookat : MonoBehaviour
{
    public Transform LookatObject;

    public void Update()
    {
        transform.LookAt(LookatObject);
    }
}
