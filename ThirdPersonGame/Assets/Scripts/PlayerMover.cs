using System;
using DG.Tweening;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;
    [SerializeField] private Transform _body;
    [SerializeField] private Transform _animationTarget;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _moveDirectionY;
    private Animator _animator;

    private void OnFlyStart()
    {
        Debug.Log("OnFlyStart");
    }

    private void OnFlyEnd()
    {
        Debug.Log("OnFlyEnd");
    }
    
    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        _body.DOMoveY(_animationTarget.position.y, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnEnable()
    {
        _inputService.Jump += OnJump;
    }

    private void OnDisable()
    {
        _inputService.Jump -= OnJump;
    }

    private void Update()
    {
        Vector2 input = _inputService.MoveDirection;
        Vector3 cameraForward = _camera.forward;
        Vector3 cameraRigth = _camera.right;
        
        cameraForward.y = 0;
        cameraRigth.y = 0;
        
        Vector3 move = cameraForward * input.y + cameraRigth * input.x;

        _characterController.Move(move * _moveSpeed * Time.deltaTime);

        if (move != Vector3.zero) 
            transform.forward = move;
        
        _moveDirectionY = Mathf.Max(_gravity, _moveDirectionY + _gravity * Time.deltaTime);
        
        Vector3 velocity = move * _moveSpeed;
        velocity.y = _moveDirectionY;

        _characterController.Move(velocity * Time.deltaTime);
        
        _animator.SetBool(AnimatortParameters.IsFlying, !_characterController.isGrounded);
    }

    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDirectionY = 10;
            Debug.Log("OnJump");
        }
    }
}