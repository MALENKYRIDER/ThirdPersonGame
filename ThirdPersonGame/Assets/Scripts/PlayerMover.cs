using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private Transform _camera;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _moveDirectionY;

    private void Awake()
    {
        _characterController = GetComponent<CharacterController>();
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