using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float _walkSpeed;

    private CharacterController _controller;

    protected void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }
    
    protected void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            _controller.SimpleMove(Vector3.left * _walkSpeed);
        }
        if (Input.GetKey(KeyCode.D))
        {
            _controller.SimpleMove(Vector3.right * _walkSpeed);
        }
        if (Input.GetKey(KeyCode.W))
        {
            _controller.SimpleMove(Vector3.forward * _walkSpeed);
        }
        if (Input.GetKey(KeyCode.S))
        {
            _controller.SimpleMove(Vector3.back * _walkSpeed);
        }
    }
}
