using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _walkSpeed;

    private CharacterController _controller;
    private Transform _body;

    protected void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _body = transform.GetChild(0);
    }
    
    protected void Update()
    {
        var moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        _body.LookAt(transform.position + moveDirection);

        moveDirection *= _walkSpeed;
        _controller.Move(moveDirection * Time.deltaTime);

    }
}
