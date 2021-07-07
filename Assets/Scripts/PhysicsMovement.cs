using System;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsMovement : MonoBehaviour
{
    [SerializeField] private float _minimumGroundNormalY = 0.65f;
    [SerializeField] private float _gravityModifier = 1f;
    [SerializeField] private Vector2 _velocity;
    [SerializeField] private LayerMask _layerMask;

    [SerializeField] private PlayerInput _playerInput;
    
    [SerializeField] private float _moveSpeed = 2f;
    [SerializeField] private float _jumpForse = 6.5f;
    [SerializeField] private int _maxJumpCount = 2;
    
    protected bool _grounded = false;
    private int _jumpCount = 0;

    #region Rigidbody Components
    protected Vector2 _targetVelocity;
    protected Vector2 _groundNormal;
    protected Rigidbody2D _rigidbody;
    protected ContactFilter2D _contactFilter;
    protected RaycastHit2D[] _hitBuffer = new RaycastHit2D[16];
    protected List<RaycastHit2D> _hitBufferList = new List<RaycastHit2D>(16);

    protected const float _minimumMoveDistance = 0.001f;
    protected const float _shellRadius = 0.01f;
    #endregion

    public event Action FirstJump;
    public event Action SecondJump;
    
    public Vector2 Velocity => _velocity;
    public bool IsGrounded => _grounded;
    
    private void OnEnable()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput.OnJumped += Jump;
    }

    private void Start()
    {
        _contactFilter.useTriggers = false;
        _contactFilter.SetLayerMask(_layerMask);
        _contactFilter.useLayerMask = true;
    }

    private void Update()
    {
        _targetVelocity = new Vector2(_playerInput.MoveX, 0);
    }

    private void FixedUpdate()
    {
        _velocity += _gravityModifier * Physics2D.gravity * Time.deltaTime;
        _velocity.x = _targetVelocity.x;

        _grounded = false;

        Vector2 deltaPosition = _velocity * Time.deltaTime;
        Vector2 moveAlongGround = new Vector2(_groundNormal.y, -_groundNormal.x);
        Vector2 move = moveAlongGround * _moveSpeed * deltaPosition.x;

        Movement(move, false);

        move = Vector2.up * deltaPosition.y;

        Movement(move, true);
    }
    
    private void Movement(Vector2 move, bool yMovement)
    {
        float distance = move.magnitude;

        if (distance > _minimumMoveDistance)
        {
            int count = _rigidbody.Cast(move, _contactFilter, _hitBuffer, distance + _shellRadius);

            _hitBufferList.Clear();

            for (int i = 0; i < count; i++)
            {
                _hitBufferList.Add(_hitBuffer[i]);
            }

            for (int i = 0; i < _hitBufferList.Count; i++)
            {
                Vector2 currentNormal = _hitBufferList[i].normal;
                if (currentNormal.y > _minimumGroundNormalY)
                {
                    _grounded = true;
                    if (yMovement == true)
                    {
                        _groundNormal = currentNormal;
                        currentNormal.x = 0;
                    }
                }

                float projection = Vector2.Dot(_velocity, currentNormal);
                if (projection < 0)
                {
                    _velocity = _velocity - projection * currentNormal;
                }

                float modifiedDistance = _hitBufferList[i].distance - _shellRadius;
                distance = modifiedDistance < distance ? modifiedDistance : distance;
            }
        }

        _rigidbody.position = _rigidbody.position + move.normalized * distance;
    }
    
    private void Jump()
    {
        if (_grounded == true)
        {
            _velocity.y = _jumpForse;
            FirstJump?.Invoke();
        }
        else if (++_jumpCount < _maxJumpCount)
        {
            _velocity.y = _jumpForse;
            SecondJump?.Invoke();
        }

        if (_grounded == true)
        {
            _jumpCount = 0;
        }
    }
}
