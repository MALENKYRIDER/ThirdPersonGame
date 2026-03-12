using System;
using UnityEngine;

public class PlayerMover : MonoBehaviour
{
    [SerializeField] private InputService _inputService;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _gravity;

    private CharacterController _characterController;
    private Vector3 _moveDirection;
    private float _moveDiractionY;

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
        _moveDiractionY = Mathf.Max(_gravity, _moveDiractionY + _gravity * Time.deltaTime);
        var moveDirection = new Vector3(_inputService.MoveDirection.x, _moveDiractionY, _inputService.MoveDirection.y);
        _characterController.Move(moveDirection * (Time.deltaTime * _moveSpeed));
    }

    private void OnJump()
    {
        if (_characterController.isGrounded)
        {
            _moveDiractionY = 1;
            Debug.Log("OnJump");
        }
    }
}