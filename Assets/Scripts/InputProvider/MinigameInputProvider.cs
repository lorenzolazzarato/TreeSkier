using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MinigameInputProvider : InputProvider
{
    #region Delegate
    public OnVector2Delegate OnMove;
    public OnVector2Delegate OnStartTouch;
    public OnVector2Delegate OnEndTouch;
    #endregion

    [Header("Gameplay")]
    [SerializeField]
    private InputActionReference _Move;


    [SerializeField]
    private InputActionReference _IsTouching;


    private Vector2 _positionTouch;

    private void Start()
    {
        _Move.action.Enable();
        _IsTouching.action.Enable();
    }

    private void OnEnable()
    {
        //_Move.action.Enable();
        //_IsTouching.action.Enable();

        _Move.action.performed += MovePerfomed;
        _IsTouching.action.started += StartTouch;
        _IsTouching.action.canceled += EndTouch;
    }

    private void OnDisable()
    {
        //_Move.action.Disable();
        //_IsTouching.action.Disable();


        _Move.action.performed -= MovePerfomed;

        _IsTouching.action.started -= StartTouch;
        _IsTouching.action.canceled -= EndTouch;

        //InputReset();
    }

    private void MovePerfomed(InputAction.CallbackContext obj)
    {
        //Debug.Log("Move performed minigame");


        Vector2 value = obj.action.ReadValue<Vector2>();
        _positionTouch = value;

        OnMove?.Invoke(value);
    }


    private void StartTouch(InputAction.CallbackContext obj)
    {
        //Debug.Log("Started touch minigame" + _positionTouch);

        //Vector2 value = obj.action.ReadValue<Vector2>();
        OnStartTouch?.Invoke(_positionTouch);
    }

    private void EndTouch(InputAction.CallbackContext obj)
    {
        //Debug.Log("Ended touch minigame " + _positionTouch);

        //Vector2 value = obj.action.ReadValue<Vector2>();
        OnEndTouch?.Invoke(_positionTouch);
    }

    
}
