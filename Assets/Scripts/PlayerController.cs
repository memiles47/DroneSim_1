using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float yawRate;
    //public float assentRate;
    public float speed;
    private Rigidbody _droneRBody;

    // Use this for initialization
    private void Start ()
    {
        _droneRBody = GetComponent<Rigidbody>();
        yawRate = 60.0f;
        //assentRate = 9.0f;
        speed = 18.0f;
        
    }
    
    // Update is called once per frame
    private void Update ()
    {

        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        var rStickX = Input.GetAxis("T1s_RStick-X");
        var rStickY = Input.GetAxis("T1s_RStick-Y");

        Vector3 movement = transform.TransformDirection(new Vector3(rStickX, verticalAxis, rStickY) * speed * Time.deltaTime);
        _droneRBody.MovePosition(transform.position + movement);

        //Quaternion rotation = Quaternion.Euler(new Vector3(0, horizontalAxis, 0) * yawRate * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontalAxis, 0), yawRate * Time.deltaTime);
    }
}
