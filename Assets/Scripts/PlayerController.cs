using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _walkSpeed;
    [SerializeField] float _gravity = 20f;

    
    private bool _isSprinting;
    private Vector3 _moveDirection = Vector3.zero;

    private CharacterController _controller;
    private Transform _body;
    private Animator _animator;
    public bool CanMove = true;

    protected void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _body = transform.GetChild(0);
        _animator = GetComponentInChildren<Animator>();
    }

    protected void Update()
    {
        if (_controller.isGrounded)
        _isSprinting = Input.GetKey(KeyCode.LeftShift);

        if (_controller.isGrounded) 
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _body.LookAt(transform.position + _moveDirection);
            _animator.SetFloat("Speed_f", _moveDirection.magnitude);

            _moveDirection *= _walkSpeed;

        }

        _moveDirection.y -= _gravity * Time.deltaTime;
        if (!CanMove) return;
            var moveSpeed = _isSprinting ? _walkSpeed * 2f : _walkSpeed;
            _moveDirection *= moveSpeed;
         }

         _moveDirection.y -= _gravity * Time.deltaTime;
        _controller.Move(_moveDirection * Time.deltaTime);
    }

    protected void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            hit.gameObject.GetComponent<EnemyController>().StartTalking();
            CanMove = false;

        }
        else if (hit.gameObject.transform.parent.CompareTag("Goal"))
        {
            MainManager.Instance.WinGame();
        }
    }


}
