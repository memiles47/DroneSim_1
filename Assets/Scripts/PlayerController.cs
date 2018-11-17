using UnityEngine;

// ReSharper disable once UnusedMember.Global
// ReSharper disable once CheckNamespace
public class PlayerController : MonoBehaviour
{
    // Declare Public Variables
    private const float YawRate = 60.0f;
    private const float Speed = 18.0f;
    private const float TiltAngle = 25.0f;
    private float SmoothTilt = 10.0f;
    public bool enableTilt1;
    public bool enableTilt2 = true;

    // Declare Private Variables
    private Rigidbody _droneRBody;


    // ReSharper disable once UnusedMember.Local
    private void Start ()
    {

    }

    // ReSharper disable once UnusedMember.Local
    private void Awake()
    {
        // Initiate Variables
        _droneRBody = GetComponent<Rigidbody>();
        
    }
    
    // ReSharper disable once UnusedMember.Local
    private void Update ()
    {
        // Initiate Variables for checking within game
        var horizontalAxis = Input.GetAxis("Horizontal");
        var verticalAxis = Input.GetAxis("Vertical");
        var rStickX = Input.GetAxis("T1s_RStick-X");
        var rStickY = Input.GetAxis("T1s_RStick-Y");

        #region Tilt Switch

        if (!enableTilt1 & !enableTilt2)
        {
            enableTilt2 = true;
        }
        else if (enableTilt1)
        {
            enableTilt2 = false;
        }

        #endregion

        var movement = transform.TransformDirection(new Vector3(rStickX, verticalAxis, rStickY) * Speed * Time.deltaTime);
        _droneRBody.MovePosition(transform.position + movement);

        //Quaternion rotation = Quaternion.Euler(new Vector3(0, horizontalAxis, 0) * yawRate * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontalAxis, 0), YawRate * Time.deltaTime);

        #region Tilt1

        if (enableTilt1)
        {
            // Smoothly tilts a transform towards a target rotation.
            var tiltAroundZ = Input.GetAxis("T1s_RStick-X") * TiltAngle * -1;
            var tiltAroundX = Input.GetAxis("T1s_RStick-Y") * TiltAngle;

            var target = Quaternion.Euler(tiltAroundX, 0, tiltAroundZ);

            // Dampen towards the target rotation
            transform.rotation = Quaternion.Slerp(transform.rotation, target, Time.deltaTime * SmoothTilt);
        }

        #endregion

        #region Tilt2

        //Tilt with rotate option, tilt angle * X Axis input
        //Having doubts about this variation
        //transform.Rotate(new Vector3(0, horizontalAxis, TiltAngle * rStickX), YawRate * Time.deltaTime);

        #endregion
    }
}
