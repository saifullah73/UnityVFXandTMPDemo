using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerController : MonoBehaviour
{
    public float speed = 5.0f;
    private float forwardInput;
    private float horizontalInput;
    public float turnSmoothTime = 0.1f;
    private float turnSmoothVelocity;
    public CharacterController controller;
    public Joystick joystick;
    public Transform cam;
    public Animator anim;
    public CinemachineFreeLook camera;
    private float verticalSpeed = 0f;
    private float gravity = 9.8f;
    // Start is called before the first frame update

    private void Awake()
    {
#if UNITY_EDITOR
        Debug.unityLogger.logEnabled = true;
#else
        Debug.unityLogger.logEnabled = false;
#endif

        //Application.targetFrameRate = 90;
    }

    void Start()
    {
        //rb = GetComponent<Rigidbody>();
        //focalPoint = GameObject.Find("FocalPoint");
    }

    // Update is called once per frame
    void Update()
    {

#if UNITY_EDITOR
        horizontalInput = Input.GetAxis("Horizontal");
        forwardInput = Input.GetAxis("Vertical");
#else
         horizontalInput = joystick.Horizontal;
         forwardInput = joystick.Vertical;
#endif
        /*horizontalInput = joystick.Horizontal;
        forwardInput = joystick.Vertical;*/
        
        string animClipName = anim.GetCurrentAnimatorClipInfo(0)[0].clip.name;
        if (animClipName.Equals("Fox_Sit1") || animClipName.Equals("Fox_Sit_Idle_Break"))
        {
            return;
        }
        Vector3 direction = new Vector3(horizontalInput, 0f, forwardInput).normalized;
        anim.SetFloat("Horizontal", horizontalInput);
        anim.SetFloat("Speed", direction.magnitude);
        if (Mathf.Abs(horizontalInput) > 0.99)
        {
            anim.SetBool("Normal", true);
        }
        else
        {
            anim.SetBool("Normal", false);
        }
        if (direction.magnitude >= 0.1)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            Vector3 moveVelocity = moveDir.normalized * speed;
            if (controller.isGrounded)
            {
                verticalSpeed = 0;
            }
            verticalSpeed -= gravity * Time.deltaTime;
            moveVelocity.y = verticalSpeed;
            controller.Move(moveVelocity * Time.deltaTime);
        }
        

        //rb.AddForce(focalPoint.transform.forward * speed * forwardInput);
        //rb.AddForce(Vector3.forward*speed*forwardInput);
        //rb.AddTorque(Vector3.up*horizontalInput*turnSpeed*Time.deltaTime, ForceMode.Impulse);
        //transform.Translate(Vector3.forward * Time.deltaTime * speed * forwardInput);
        //transform.Rotate(Vector3.up, horizontalInput * turnSpeed * Time.deltaTime);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Step"))
        {
            other.gameObject.GetComponent<ToggleLights>().Trigger();
        }
        if (other.gameObject.CompareTag("Firework"))
        {
            other.gameObject.GetComponent<ToggleFireworks>().Toggle();
        }
    }
}
