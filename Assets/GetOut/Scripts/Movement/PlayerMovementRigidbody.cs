using UnityEngine;

public class PlayerMovementRigidbody : MonoBehaviour
{
    [SerializeField] private Space _space;
    [SerializeField] private bool _isZAxis = true;
    [SerializeField] private Transform _target;

    [SerializeField] private float _speed = 1f;
    
    private PlayerInput _input;
    private Rigidbody _rb;
    private Vector3 _force;
    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = new PlayerInput();
        _input.Control.Movement.performed += _ => _force = Convert(_.ReadValue<Vector2>());
        _input.Control.Movement.canceled += _ => _force = Vector3.zero;
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

    private void FixedUpdate()
    {
        _rb.velocity = _speed * Time.deltaTime * _force;
    }


    private Vector3 Convert(Vector2 raw)
    {
        var rawV3 = _isZAxis ? new Vector3(raw.x, 0, raw.y) : new Vector3(raw.x, raw.y, 0);

        if (_target)
            return _target.right * rawV3.x + _target.up * rawV3.y + _target.forward * rawV3.z;
        if (_space == Space.Self)
            return transform.right * rawV3.x + transform.up * rawV3.y + transform.forward * rawV3.z;
        return rawV3;
    }
}
