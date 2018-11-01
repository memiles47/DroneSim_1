using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Public Variables
    public float yawRate;
    public float speed;
    public float tiltAngle;
    public float smoothTilt;

    // Private Variables
    private Rigidbody _droneRBody;


    private void Start ()
    {

    }

    private void Awake()
    {
        // Initiate Variables
        _droneRBody = GetComponent<Rigidbody>();

        yawRate = 60.0f;
        speed = 18.0f;
        tiltAngle = 25.0f;
        smoothTilt = 10.0f;
    }
    
    private void Update ()
    {
        // Initiate Variables for checking within game
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        var rStickX = Input.GetAxis("T1s_RStick-X");
        var rStickY = Input.GetAxis("T1s_RStick-Y");

        //xRotate = rStickY;
        //zRotate = rStickX;

        var movement = transform.TransformDirection(new Vector3(rStickX, verticalAxis, rStickY) * speed * Time.deltaTime);
        _droneRBody.MovePosition(transform.position + movement);

        //Quaternion rotation = Quaternion.Euler(new Vector3(0, horizontalAxis, 0) * yawRate * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontalAxis, 0), yawRate * Time.deltaTime);


        // Smoothly tilts a transform towards a target rotation.
        var tiltAroundZ = Input.GetAxis("T1s_RStick-X") * tiltAngle * -1;
        var tiltAroundX = Input.GetAxis("T1s_RStick-Y") * tiltAngle;

        var target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

        // Dampen towards the target rotation
        transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * smoothTilt);
    }
}
