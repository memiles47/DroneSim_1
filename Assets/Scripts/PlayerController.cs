using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Declare Public Variables
    private const float YawRate = 60.0f;
    private const  float Speed = 18.0f;
    private const float TiltAngle = 25.0f;
    private const float SmoothTilt = 10.0f;
    public bool enableTilt;

    // Declare Private Variables
    private Rigidbody _droneRBody;


    private void Start ()
    {

    }

    private void Awake()
    {
        // Initiate Variables
        _droneRBody = GetComponent<Rigidbody>();

        enableTilt = false;
    }
    
    private void Update ()
    {
        // Initiate Variables for checking within game
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        var rStickX = Input.GetAxis("T1s_RStick-X");
        var rStickY = Input.GetAxis("T1s_RStick-Y");

        var movement = transform.TransformDirection(new Vector3(rStickX, verticalAxis, rStickY) * Speed * Time.deltaTime);
        _droneRBody.MovePosition(transform.position + movement);

        //Quaternion rotation = Quaternion.Euler(new Vector3(0, horizontalAxis, 0) * yawRate * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontalAxis, 0), YawRate * Time.deltaTime);

        if (enableTilt)
        {
            // Smoothly tilts a transform towards a target rotation.
            var tiltAroundZ = Input.GetAxis("T1s_RStick-X") * TiltAngle * -1;
            var tiltAroundX = Input.GetAxis("T1s_RStick-Y") * TiltAngle;

            var target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * SmoothTilt);
        }
    }
}
