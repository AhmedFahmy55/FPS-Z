using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class FPSController : MonoBehaviour
{
    [SerializeField] float walkSpeed = 4;
    [SerializeField] float runSpeed = 8;
    [SerializeField] float speedChangeRate = 10;


    [Header("Rotation settings")]
    [SerializeField] float rotationSpeed = 1;
    [SerializeField] Transform camRoot;
    [SerializeField, Range(-360, 360)] float maxLookAngel;
    [SerializeField, Range(-360, 360)] float minLookAngel;

    [Header("jumping")]
    [SerializeField] float jumbHeight;
    [SerializeField] float gravity = -9.8f;


    [Header("Ground check")]
    [SerializeField] float groundOfset;
    [SerializeField] float groundedRadius;
    [SerializeField] LayerMask groundLayers;





    InputManager inputManager;
    CharacterController controller;
    Animator _anim;

    public float _xRotaton;
    private float _yRotaton;
    private float _verticalVelocity;


    public bool _isGrounded = true;
    private float fallSpeed = 2;

    private int _speedID;
    private int _jumpID;

    

    private float _blendValue;

    private void Awake()
    {
        inputManager = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        _anim = GetComponentInChildren<Animator>();
    }

    private void Start() {
        
        SetupAnimationsIDs();
    }


        private void SetupAnimationsIDs()
        {
            _speedID = Animator.StringToHash("Speed");
            _jumpID =  Animator.StringToHash("Jumb");



    }

    private void Update()
    {
        HandleMovement();
        HandleJumpingAndGravity();
        GroundedCheck();

    }



    private void LateUpdate()
    {
        HandleRotation();
    }

    private void HandleJumpingAndGravity()
    {

        if (_isGrounded)
        {

            if (_verticalVelocity < 0.0f)
            {
                _verticalVelocity = -2f;
            }

            // Jump
            if (inputManager.Jumb)
            {
                _verticalVelocity = Mathf.Sqrt(jumbHeight * -2f * gravity);
                _anim.SetTrigger(_jumpID);
                inputManager.Jumb = false;
                
            }

        }
        else
        {
            _verticalVelocity += fallSpeed * gravity * Time.deltaTime;
        }

    }

    private void HandleRotation()
    {
        _xRotaton -= inputManager.Look.y * rotationSpeed * Time.smoothDeltaTime;
        _yRotaton += inputManager.Look.x * rotationSpeed * Time.smoothDeltaTime;
        _xRotaton = Mathf.Clamp(_xRotaton, minLookAngel, maxLookAngel);
        camRoot.localRotation = Quaternion.Euler(_xRotaton, 0, 0);
        //transform.Rotate(Vector3.up * _yRotaton);
        transform.rotation = Quaternion.Lerp(transform.rotation,Quaternion.Euler(0,_yRotaton,0),Time.smoothDeltaTime*rotationSpeed*10);
    }

    private void HandleMovement()
    {
        
        float speed = inputManager.Run? runSpeed : walkSpeed;
        if(inputManager.Move == Vector2.zero) speed = 0;
        Vector3 dir = transform.right * inputManager.Move.x + transform.forward * inputManager.Move.y;
        controller.Move(dir * speed * Time.smoothDeltaTime + new Vector3(0, _verticalVelocity, 0) * Time.smoothDeltaTime);

        _blendValue = Mathf.Lerp(_blendValue,speed,Time.deltaTime*speedChangeRate);
        if(_blendValue <0.01f) _blendValue = 0;
        _anim.SetFloat(_speedID,_blendValue);
    }

    private void GroundedCheck()
    {
        // set sphere position, with offset
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundOfset, transform.position.z);
        _isGrounded = Physics.CheckSphere(spherePosition, groundedRadius, groundLayers, QueryTriggerInteraction.Ignore);
    }
   
    private void OnDrawGizmos()
    {
        Gizmos.color = _isGrounded ? Color.green : Color.red;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y - groundOfset, transform.position.z), groundedRadius);
    }
}
