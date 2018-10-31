﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Variables
    public float yawRate;
    public float speed;
    public float forwardRotate;
    public float sideSlipRotate;
    public float forwardTilt;
    public float sideTilt;
    public float zRotate;
    public float xRotate;

    // Private Variables
    private Rigidbody _droneRBody;
    private Quaternion _droneRotation;


    private void Start ()
    {
        // Initiate Variables
        //_droneRBody = GetComponent<Rigidbody>();

        //yawRate = 60.0f;
        //speed = 18.0f;
        //forwardRotate = 12.0f;
        //sideSlipRotate = 12.0f;
    }

    private void Awake()
    {
        // Initiate Variables
        _droneRBody = GetComponent<Rigidbody>();

        yawRate = 60.0f;
        speed = 18.0f;
        forwardRotate = 12.0f;
        sideSlipRotate = 12.0f;
    }
    
    private void Update ()
    {
        // Initiate Variables for checking within game
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        var rStickX = Input.GetAxis("T1s_RStick-X");
        var rStickY = Input.GetAxis("T1s_RStick-Y");

        xRotate = rStickY;
        zRotate = rStickX;

        Vector3 movement = transform.TransformDirection(new Vector3(rStickX, verticalAxis, rStickY) * speed * Time.deltaTime);
        _droneRBody.MovePosition(transform.position + movement);

        //Quaternion rotation = Quaternion.Euler(new Vector3(0, horizontalAxis, 0) * yawRate * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontalAxis, 0), yawRate * Time.deltaTime);

        _droneRotation.x = forwardRotate * rStickY;
        _droneRotation.z = sideSlipRotate * rStickX;
    }
}
