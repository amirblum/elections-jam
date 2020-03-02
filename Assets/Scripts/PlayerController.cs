using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _walkSpeed;

    private CharacterController _controller;
    private Transform _body;
    private Animator _animator;

    protected void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _body = transform.GetChild(0);
        _animator = GetComponentInChildren<Animator>();
    }

    protected void Update()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _body.LookAt(transform.position + moveDirection);
        _animator.SetFloat("Speed_f", moveDirection.magnitude);

        moveDirection *= _walkSpeed;
        _controller.Move(moveDirection * Time.deltaTime);
    }

    protected void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("Enemy"))
        {
            hit.gameObject.GetComponent<EnemyController>().NewRandomTalk();

        }
        else if (hit.gameObject.transform.parent.CompareTag("Goal"))
        {
            MainManager.Instance.WinGame();
        }
    }
}
