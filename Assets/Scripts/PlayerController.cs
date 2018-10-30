using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float yawRate;
    //public float assentRate;
    public float speed;
    public float forwardRotate;
    public float sideSlipRotate;
    public float forwardTilt;
    public float sideTilt;
    private Rigidbody _droneRBody;
    private Quaternion _droneRotation;

    public float zRotate;
    public float xRotate;

    // Use this for initialization
    private void Start ()
    {
        _droneRBody = GetComponent<Rigidbody>();
        _droneRotation = _droneRBody.rotation;

        yawRate = 60.0f;
        //assentRate = 9.0f;
        speed = 18.0f;
        forwardRotate = 12.0f;
        sideSlipRotate = 12.0f;
    }
    
    // Update is called once per frame
    private void Update ()
    {
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
