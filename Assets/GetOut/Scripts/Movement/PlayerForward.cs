using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerForward : MonoBehaviour
{
    [SerializeField] private Transform _target;
    private PlayerInput _input;

    private void Awake()
    {
        _input = new PlayerInput();
        _input.Control.Movement.performed += _ => SetForward(_.ReadValue<Vector2>());
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

    private void SetForward(Vector2 value)
    {
        transform.forward = Convert(value);
    }

    private Vector3 Convert(Vector2 raw)
    {
        var rawV3 = new Vector3(raw.x, 0, raw.y);

        if (_target)
            return _target.right * rawV3.x + _target.up * rawV3.y + _target.forward * rawV3.z;

        return transform.forward;
    }
}