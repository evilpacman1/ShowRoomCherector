using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CherectorMovment : MonoBehaviour
{
    // Start is called before the first frame update
    public float speedMove;
    public float turnSmoothTime = 0.1f;
    float turnSmootVelosity;
    private float  Horizontal;
    private float Vertical;
    public float jumpPower= 5;
    public float ySpeed = 0;
    private Vector3 moveVector;
    public Transform cam;

    private CharacterController _characterController;
    private Animation animationHero;
    void Start()
    {
      _characterController = GetComponent<CharacterController>();
        animationHero = GetComponent<Animation>();
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveHero(); 
    }

    private void MoveHero()
    {
       
        Horizontal = Input.GetAxis("Horizontal") * speedMove;
        Vertical = Input.GetAxis("Vertical") * speedMove;
        ySpeed += Physics.gravity.y * Time.deltaTime;   
        if (_characterController.isGrounded)
        {
            ySpeed = -10;
            if (Input.GetButtonDown("Jump"))
            {
                ySpeed = jumpPower;
            }
        }
        
        
        Vector3 diraction = new Vector3(Horizontal, 0f, Vertical).normalized;
        Vector3 Velocity = diraction;
        Velocity.y = ySpeed;
        _characterController.Move(Velocity * Time.deltaTime);
        if (diraction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(diraction.x, diraction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float Angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmootVelosity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, Angle, 0f);
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            _characterController.Move(moveDir.normalized * speedMove * Time.deltaTime);

        }

    }
}
