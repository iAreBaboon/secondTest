﻿using UnityEngine;
using System.Collections;


[RequireComponent (typeof(CharacterController))]
public class FirstPersonController : MonoBehaviour {

    public float movementSpeed;
    public float mouseSensitivity = 2.0f;
    public float upDownRange = 60.0f;
    public float jumpSpeed = 20.0f;

    float verticalRotation = 0;

    float verticalVelocity = 0;

    CharacterController characterController;

	// Use this for initialization
	void Start () {
        Cursor.visible = false;
        characterController = GetComponent<CharacterController>();
	}
	
	// Update is called once per frame
	void Update () {

        // Rotation
        

        float rotLeftRight = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);

        verticalRotation -= Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation = Mathf.Clamp(verticalRotation, -upDownRange, upDownRange);
        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0, 0);

        // Movement

        float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;

        verticalVelocity += Physics.gravity.y * Time.deltaTime;

        if( characterController.isGrounded && Input.GetButtonDown("Jump"))
        {
            verticalVelocity = jumpSpeed;
        }

        Vector3 speed = new Vector3(sideSpeed, verticalVelocity, forwardSpeed);

        speed = transform.rotation * speed;

        

        characterController.Move(speed * Time.deltaTime);
	}
}
