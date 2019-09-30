using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Space _space;
    [SerializeField] private bool _isZAxis = true;
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed = 1f;
    
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Control.Movement.started += _ => StartCoroutine(Move(_));
        _input.Control.Movement.canceled += _ => StopAllCoroutines();
    }

    private void OnEnable()
    {
        _input.Enable();
    }

    private void OnDisable()
    {
        _input.Disable();
    }

    private void OnDestroy()
    {
        _input.Disable();
        _input = null;
    }
    
    private IEnumerator Move(InputAction.CallbackContext callbackContext)
    {
    Debug.Log("ou");
        while (true)
        {
            if (_target)
            {
                transform.Translate(Convert(callbackContext.ReadValue<Vector2>()) * Time.deltaTime * _speed, _target);
            }
            else
                transform.Translate(Convert(callbackContext.ReadValue<Vector2>()) * Time.deltaTime * _speed, _space);
            yield return null;
        }
    }

    private Vector3 Convert(Vector2 raw)
    {
        if (_isZAxis)
            return new Vector3(raw.x, 0, raw.y);
        return new Vector3(raw.x, raw.y, 0);
        
    }
}
