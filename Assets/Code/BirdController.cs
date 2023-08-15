using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BirdController : MonoBehaviour
{
    [SerializeField] private float jumpVelocity;
    private Rigidbody2D _rigidbody2D;
    private PlayerInput _playerInput;

    public UnityEvent die = new ();
     
    private void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<PlayerInput>();
        _playerInput.actions["Jump"].performed += OnJump;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacles"))
        {
            die.Invoke();
        }
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        _rigidbody2D.velocity = Vector2.up * jumpVelocity;
    }
}
