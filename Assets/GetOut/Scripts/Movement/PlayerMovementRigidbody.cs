using System.Collections;
using Unity.Collections;
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

    private bool _canControl = true;

    [SerializeField] private float _nockBackForce = 2000;
    [SerializeField] private float _timeToMoveWithForce = 0.1f;
    [SerializeField] private float _stunTime = 0.2f;

    [SerializeField] private Animator PlayerAnim;
    private static readonly int MoveSpeed = Animator.StringToHash("moveSpeed");

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _input = new PlayerInput();
        _input.Control.Movement.performed += _ => _force = Convert(_.ReadValue<Vector2>());
        _input.Control.Movement.canceled += _ => _force = Vector3.zero;

        GameLoop.onStageOver += OnGameOver;
    }


    private void OnGameOver(bool value)
    {
        this.enabled = false;
        _rb.velocity = Vector3.zero;
        PlayerAnim.SetFloat(MoveSpeed, 0f);
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
        GameLoop.onStageOver -= OnGameOver;
        _input.Disable();
        _input = null;
    }

    private void FixedUpdate()
    {
        if (_canControl)
        {
            _rb.velocity = _speed * Time.deltaTime * _force;
            PlayerAnim.SetFloat(MoveSpeed, _rb.velocity.magnitude);
        }
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

    public void Knockback(Vector3 from)
    {
        StopAllCoroutines();
        StartCoroutine(KnockingBack(from));

    }

    private IEnumerator KnockingBack(Vector3 from)
    {
        _canControl = false;
        yield return null;
        _rb.AddExplosionForce(_nockBackForce, from,2);
        yield return  new WaitForSeconds(_timeToMoveWithForce);
        _rb.velocity = Vector3.zero;
        yield return new WaitForSeconds(_stunTime);
        _canControl = true;
    }
}
