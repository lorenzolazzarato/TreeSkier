using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class GameplayInputProvider : InputProvider
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

    private void OnEnable()
    {
        _Move.action.Enable();
        _IsTouching.action.Enable();

        _Move.action.performed += MovePerfomed;
        _IsTouching.action.started += StartTouch;
        _IsTouching.action.canceled += EndTouch;
    }

    private void OnDisable()
    {
        _Move.action.Disable();
        _IsTouching.action.Disable();
        

        _Move.action.performed -= MovePerfomed;

        _IsTouching.action.started -= StartTouch;
        _IsTouching.action.canceled -= EndTouch;
    }

    private void MovePerfomed(InputAction.CallbackContext obj)
    {
        if (!IsPointerOverGameObject()) {
            return;
        }

        Vector2 value = obj.action.ReadValue<Vector2>();
        _positionTouch = value;

        OnMove?.Invoke(value);
    }


    private void StartTouch(InputAction.CallbackContext obj)
    {
        //Debug.Log("Started touch");
        if (!IsPointerOverGameObject()) {
            return;
        }
        //Vector2 value = obj.action.ReadValue<Vector2>();
        OnStartTouch?.Invoke(_positionTouch);
        
    }

    public void EndTouch(InputAction.CallbackContext obj)
    {
        //Debug.Log("Ended touch");
        if (!IsPointerOverGameObject()) {
            return;
        }
        //Vector2 value = obj.action.ReadValue<Vector2>();
        OnEndTouch?.Invoke(_positionTouch);
    }

    public static bool IsPointerOverGameObject() {
        //check mouse
        if (EventSystem.current.IsPointerOverGameObject())
            return true;

        //check touch
        if (Input.touchCount > 0 && Input.touches[0].phase == UnityEngine.TouchPhase.Began) {
            if (EventSystem.current.IsPointerOverGameObject(Input.touches[0].fingerId))
                return true;
        }

        return false;
    }
}
