using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Joystick joystickController;
    public GameObject player;

    public float speed = 6f;

    public void Update()
    {
        float horizontal = joystickController.Horizontal;
        float vertical = joystickController.Vertical;
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if(direction.magnitude >= 0.1f)
        {
            controller.Move(direction * speed * Time.deltaTime);
        }
    }
}
