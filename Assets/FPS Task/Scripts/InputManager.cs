using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.InputSystem.InputAction;

public class InputManager : MonoBehaviour
{
    


    public Vector2 Move {get; private set;}

    public Vector2 Look {get; private set;}

    public bool Jumb {get ;  set;}
    public bool Run {get ;  set;}
    public bool Shoot {get ;  set;}
    public bool Reload { get;  set; }






    Playerinput playerInput;
    InputAction movementAction;
    InputAction lookAction;
    InputAction jumbAction;
    InputAction runAction;
    InputAction shootAction;
    InputAction reloadAction;








    private void Awake() 
    {
        playerInput = new Playerinput();
    }

    private void OnEnable() 
    {
        playerInput.Player.Enable();
        movementAction = playerInput.Player.Move;
        lookAction = playerInput.Player.Look;
        jumbAction = playerInput.Player.Jumb;
        runAction = playerInput.Player.Run;
        shootAction = playerInput.Player.Fire;
        reloadAction = playerInput.Player.Reload;

        movementAction.performed += HadleMoveInput;
        movementAction.canceled += HadleMoveInput;

        lookAction.performed += HandleLookInput;
        lookAction.canceled += HandleLookInput;

        jumbAction.performed += HandleJumbInput;
        jumbAction.canceled += HandleJumbInput;

        runAction.performed += HandleRunInput;
        runAction.canceled += HandleRunInput;

        shootAction.performed += HandleShootAction;
        shootAction.canceled += HandleShootAction;

        reloadAction.performed += HandleReloadInput;



    }

    private void HandleReloadInput(CallbackContext obj)
    {
        Reload = true;
    }

    private void OnDisable() 
    {
        playerInput.Player.Disable();

    }

    
    private void HandleShootAction(CallbackContext obj)
    {
        Shoot = obj.ReadValueAsButton();
    }


    private void HandleRunInput(CallbackContext obj)
    {
        Run = obj.ReadValueAsButton();  
    }
    private void HandleJumbInput(CallbackContext obj)
    {
        Jumb = obj.ReadValueAsButton();        
    }

    private void HandleLookInput(CallbackContext obj)
    {
        Look = obj.ReadValue<Vector2>();
    }

    private void HadleMoveInput(CallbackContext obj)
    {
        Move = obj.ReadValue<Vector2>();
    }

   

    
}
